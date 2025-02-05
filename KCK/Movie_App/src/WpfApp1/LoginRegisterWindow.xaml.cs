using ConsoleApp1.Controllers;
using ConsoleApp1.Data;
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

namespace WpfApp1
{
    public partial class LoginRegisterWindow : Window
    {
        private UserController userController;


        public LoginRegisterWindow()
        {
            InitializeComponent();
            userController = new UserController(new FilmDbContext());
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var success = userController.LoginUser(username, password);
                if (success)
                {
                    MessageBox.Show("Login successful!");
                    this.Close();
                    OpenUserMenu(username);
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            else
            {
                MessageBox.Show("Please enter both username and password.");
            }
        }



        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var success = userController.RegisterUser(username, password);
                if (success)
                {
                    MessageBox.Show("Registration successful!");
                    this.Close();
                    OpenUserMenu(username);
                }
                else
                {
                    MessageBox.Show("Username already exists.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }
        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "Password")
            {
                PasswordBox.Password = "";
                PasswordBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Password = "Password";
                PasswordBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void OpenUserMenu(string username)
        {
            UserMenuWindow userMenuWindow = new UserMenuWindow(username);
            userMenuWindow.Show();
            this.Close();
        }
    }
}
