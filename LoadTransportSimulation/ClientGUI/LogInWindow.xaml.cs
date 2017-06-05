using System.Windows;
using ApiHttpClient;
using Models;
using Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFLoadSimulation
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender,e);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
                await Login();
                User.GetInstance();
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();    
        }

        private async Task Login()
        {
            var loginData = new { email = userName.Text, password = userPassword.Password };
            User u = (User)await ApiHttpClient.Dispatcher.GetInstance().LoginUser(loginData);
            User.Create(u);
        }
    }
}
