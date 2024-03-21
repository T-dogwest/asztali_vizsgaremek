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

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Button_Attekintes(object sender, RoutedEventArgs e)
        { 
            Main.Content = new Attekintes();
        }

        private void Button_Menu(object sender, RoutedEventArgs e)
        {
            Main.Content = new Menu();
        }

       

        private void Button_Felhasznalok(object sender, RoutedEventArgs e)
        {
            Main.Content = new Felhasznalok();
        }

       

        private void Button_cegadatok(object sender, RoutedEventArgs e)
        {
            Main.Content = new Opening();
        }

        private void Button_Velemenyek(object sender, RoutedEventArgs e)
        {
            Main.Content = new Velemenyek();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztos ki szeretne jelentkezni?", "Kijelentkezés", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Töröljük a tárolt tokent
                    TokenM.DeleteToken();
                    // Itt lehet még további logout műveleteket végezni, például a felhasználót visszairányítani a bejelentkező oldalra vagy bezárni az alkalmazást.
                    MessageBox.Show("Sikeres kijelentkezés");
                    Login loginw = new Login();
                    loginw.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a kijelentkezés során: " + ex.Message);
                }
            }
        }
    }
}
