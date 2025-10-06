using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShadrinaHotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль");
                return;
            }

            using (var context = new ShadrinaHotel_bdEntities())
            {
                var user = await context.Users
                    .Where(u => u.username == username)
                    .FirstOrDefaultAsync();
            }
            using (var context = new ShadrinaHotel_bdEntities())
            {
                var user = await context.Users
                    .Where(u => u.userName == username)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    MessageBox.Show("неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (user.Islocked.HasValue && user.Islocked.Value)
                {
                    MessageBox.Show("Вы заблокированы, обратитесь к админу", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (user.LastLoginDate.HasValue && (DateTime.Now - user.LastLoginDate.Value).TotalDays > 30 && user.role != "Admin")
                {
                    user.Islocked = true;
                    await context.SaveChangesAsync();
                    MessageBox.Show("Вы заблокированы, обратитесь в поддержку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (user.password == password)
                {
                    user.LastLoginDate = DateTime.Now;
                    user.IsFirstLogin = 0;
                    await context.SaveChangesAsync();
                    MessageBox.Show("Успешная авторизация", "Ураааа", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (user.IsFirstLogin.HasValue && user.IsFirstLogin.Value)
                    {
                        ChangePassword changePasswordWindow = new ChangePassword(user.id);
                        ChangePasswordWindow.Owner = this;
                        ChangePasswordWindow.ShowDialog();
                    }
                    else
                    {
                        if (user.role == "Admin")
                        {
                            Admin adminWindow = new Admin();
                            adminWindow.Showg();
                        }
                        else
                        {
                            MainWindow userWindow = new MainWindow();
                            userWindow.Show();
                        }
                        this.Close();
                    }

                }
                else
                {
                    user.IsFirstLogin++;
                    if (user.IsFirstLogin == 3)
                    {
                        user.Islocked = true;
                        MessageBox.Show("Вы заблокированы, обратитесь в поддержку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        this attemptLeft = 3 - (user.IsFirstLogin ?? 0);
                        MessageBox.Show($"неправильный логин или пароль. Осталось попыток: {attemprsLeft}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }
    

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
