using System;
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
using System.Windows.Shapes;

namespace ShadrinaHotel
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly int _userId;
        
        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = txtCurrentPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmNewPassword = txtCurrentPassword.Password;

            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmNewPassword))
            {
                MessageBox.Show("Все поля обязательны к заполнению", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Пароль не совпадают", "Ошибка" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using(var context)
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
