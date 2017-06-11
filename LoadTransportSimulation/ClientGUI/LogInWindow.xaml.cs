using System.Windows;
using ApiHttpClient;
using Models;
using Common;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Controllers;

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
 
            // Initial creation of dispacher on application start 
            ApiHttpClient.Dispatcher.Create("");

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
            ApiHttpClient.Dispatcher.Create(User.GetInstance().Token);
            await GetCompany();
        }

        private async Task GetCompany()
        {
            Company c = (Company)await ApiHttpClient.Dispatcher.GetInstance().Get<Company>("companies", User.GetInstance().CompanyId);
            User.GetInstance().Company = c;
            CompanyController.Create(c);
            
        }
    }
}
