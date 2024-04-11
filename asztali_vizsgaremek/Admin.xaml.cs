using asztali_vizsgaremek.Attekintes;
using asztali_vizsgaremek.Menu;
using asztali_vizsgaremek.Nyitvatartas;
using asztali_vizsgaremek.Profile;
using asztali_vizsgaremek.User;
using asztali_vizsgaremek.Velemenyek;
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
    /// Az Admin felhasználói felület ablaka.
    /// </summary>
    public partial class AdminWindow : Window
    {

        /// <summary>
        /// Admin ablak létrehozása.
        /// </summary>
        public AdminWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Az AttekintesPage megnyitását végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Attekintes(object sender, RoutedEventArgs e)
        {
            Main.Content = new AttekintesPage();
        }
        /// <summary>
        /// A MenuPage megnyitását végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Menu(object sender, RoutedEventArgs e)
        {
            Main.Content = new MenuPage();
        }


        /// <summary>
        /// A FelhasznalokPage megnyitását végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Felhasznalok(object sender, RoutedEventArgs e)
        {
            Main.Content = new FelhasznalokPage();
        }

        /// <summary>
        /// Az OpeningPage megnyitását végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Opening(object sender, RoutedEventArgs e)
        {
            Main.Content = new OpeningPage();
        }
        /// <summary>
        /// A VelemenyekPage megnyitását végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Velemenyek(object sender, RoutedEventArgs e)
        {
            Main.Content = new VelemenyekPage();
        }
        /// <summary>
        /// A felhasználó kijelentkezését végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
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
        /// <summary>
        /// A ProfilePage megnyitását végző eseménykezelő.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_profil(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfilePage();
        }
    }
}
