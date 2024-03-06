using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbFelhnev.Text;
            string password = tbJelszo.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Sikeres bejelentkezés!");
                Admin admin = new Admin();
                admin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sikertelen bejelentkezés. Kérjük, ellenőrizze a felhasználónevet és a jelszót.");
            }


        }
        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = "Server=localhost;Port=3306;Database=beercycle;Uid=root;Pwd=;";

            string query = "SELECT role FROM user WHERE username = @username AND password = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["role"].ToString();
                                if (role == "Admin")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba történt az adatbázis kapcsolódás közben: " + ex.Message);
                        return false;
                    }
                }
            }
            return false;
        }

        private void tbJelszo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbFelhnev_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
