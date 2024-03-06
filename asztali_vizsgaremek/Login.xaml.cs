using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {

            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbFelhnev.Text;
            string password = tbJelszo.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            HttpClient client = new HttpClient();
            var content = new StringContent($"{{ \"username\": \"{username}\", \"password\": \"{password}\" }}", Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:3000/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();

                    // Assume token is stored or processed here, for simplicity we're ignoring it
                    MessageBox.Show("A bejelentkezés sikeres");

                    // Open AdminWindow
                    Admin adminWindow = new Admin();
                    adminWindow.Show();

                    // Close LoginWindow
                    Close();
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your credentials.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                client.Dispose();
            }
        }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}


 
    

