using System.Windows;
using ApiHttpClient;
using Models;
using Common;
using System;
using System.Threading.Tasks;


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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                await Login();
                User.GetInstance();
                MainWindow main = new MainWindow();
                main.Show();

                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong with the Sign in!");
            }
            
        }

        private async Task Login()
        {
            var loginData = new { email = userName.Text, password = userPassword.Password };
            User u = (User)await ApiHttpClient.Dispatcher.GetInstance().LoginUser(loginData);
            User.Create(u);
        }
    }
}
