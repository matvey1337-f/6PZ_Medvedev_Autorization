using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using _2_wpf_medvedev_.Models;
using _2_wpf_medvedev_.Services;
using Hash = _2_wpf_medvedev_.Services.Hash;

namespace _2_wpf_medvedev_.Pages
{
    public partial class Autho : Page
    {
        int click = 0;

        public Autho()
        {
            InitializeComponent();
        }
        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            NavigationService.Navigate(new Client(null, "Гость"));
        }
        private void GenerateCapctcha()
        {
            tblCaptcha.Visibility = Visibility.Visible;
            tbCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
            tbCaptcha.Clear();
        }
        private void ClearFields()
        {
            txtLogin.Clear();
            txtPassword.Clear();
            tbCaptcha.Clear();
            tblCaptcha.Text = "Капча:";
            tblCaptcha.Visibility = Visibility.Collapsed;
            tbCaptcha.Visibility = Visibility.Collapsed;
            click = 0;
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password.Trim();
            string passwordHash = Hash.HashPassword(password);
            var user = RullEntities.Users.FirstOrDefault(x => x.UserLogin == login && x.UserPassword == passwordHash);

            if (click == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.RoleName);
                    ClearFields();
                    LoadPage(user.RoleName, user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha();
                    txtPassword.Clear();
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.RoleName);
                    ClearFields();
                    LoadPage(user.RoleName, user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                    GenerateCapctcha();
                    txtPassword.Clear();
                    tbCaptcha.Clear();
                }
            }
        }
        private void LoadPage(string _role, User user)
        {
            if (_role == "Клиент")
            {
                NavigationService.Navigate(new Client(user, _role));
            }
        }
    }
}