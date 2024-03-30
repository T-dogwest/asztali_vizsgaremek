using asztali_vizsgaremek.Attekintes;
using asztali_vizsgaremek.Menu;
using asztali_vizsgaremek.Nyitvatartas;
using asztali_vizsgaremek.Profilee;
using asztali_vizsgaremek.User;
using asztali_vizsgaremek.Velemenyekk;
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

namespace asztali_vizsgaremek.Admin
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
   

        public AdminWindow()
        {
            InitializeComponent();

        }
      

        private void Button_Attekintes(object sender, RoutedEventArgs e)
        {
            Main.Content = new AttekintesPage();
        }

        private void Button_Menu(object sender, RoutedEventArgs e)
        {
            Main.Content = new MenuPage();
        }

       

        private void Button_Felhasznalok(object sender, RoutedEventArgs e)
        {
            Main.Content = new FelhasznalokPage();
        }

       

        private void Button_cegadatok(object sender, RoutedEventArgs e)
        {
            Main.Content = new OpeningPage();
        }

        private void Button_Velemenyek(object sender, RoutedEventArgs e)
        {
            Main.Content = new VelemenyekPage();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztos ki szeretne jelentkezni?", "Kijelentkezés", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                  
                    TokenM.DeleteToken();
                    
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
        private void Button_profil(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfilePage();
        }
    }
}
