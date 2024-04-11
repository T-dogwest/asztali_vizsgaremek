using asztali_vizsgaremek.Admin;
using asztali_vizsgaremek.User;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace asztali_vizsgaremek.Profilee
{
    /// <summary>
    /// A felhasználói profil oldal logikáját tartalmazó osztály.
    /// </summary>
    public partial class ProfilePage : Page
    {
        private FelhasznaloService userService = new FelhasznaloService();
        private FelhasznmalokItem loggedInUser;
        /// <summary>
        /// A ProfilePage osztály konstruktora.
        /// </summary>
        public ProfilePage()
        {
            InitializeComponent();
            LoadLoggedInUserData();
            DisableEdit();
        }
        /// <summary>
        /// Bejelentkezett felhasználó adatainak betöltése.
        /// </summary>
        public void LoadLoggedInUserData()
        {
            try
            {
                if (loggedInUser == null) 
                {
                    loggedInUser = userService.GetLoggedInUserData();
                    if (loggedInUser != null)
                    {
                        FillUserData(loggedInUser);
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült betölteni a bejelentkezett felhasználó adatait.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a felhasználó adatainak betöltésekor: {ex.Message}");
            }
        }
        /// <summary>
        /// A bejelentkezett felhasználó adatainak kitöltése a megfelelő mezőkbe.
        /// </summary>
        /// <param name="loggedInUser">Bejelentkezett felhasználó adatai</param>
        private void FillUserData(FelhasznmalokItem loggedInUser)
        {
            felhasznaloNevtb.Text = loggedInUser.Username;
            firstNametb.Text = loggedInUser.First_name;
            lastNametb.Text = loggedInUser.Last_name;
            profEmailtb.Text = loggedInUser.Email;
        }
        /// <summary>
        /// Módosítás gomb eseménykezelője.
        /// </summary>
        private void Button_Modify(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ellenőrizzük, hogy a szükséges mezők ki vannak-e töltve
                if (string.IsNullOrWhiteSpace(felhasznaloNevtb.Text) ||
                    string.IsNullOrWhiteSpace(firstNametb.Text) ||
                    string.IsNullOrWhiteSpace(lastNametb.Text) ||
                    string.IsNullOrWhiteSpace(profEmailtb.Text))
                {
                    MessageBox.Show("Kérlek tölts ki minden mezőt a frissítéshez.", "Figyekmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Megkérdezzük a felhasználót, hogy valóban szeretné-e frissíteni a felhasználó adatait
                MessageBoxResult result = MessageBox.Show("Biztosan szeretné frissíteni a felhasználó adatait?", "Megerősítés", MessageBoxButton.YesNo,MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Elkészítjük a frissítendő felhasználó objektumot
                    UpdateFelhasznaloDTO updateUserDto = new UpdateFelhasznaloDTO
                    {
                        Id = loggedInUser.Id,
                        Email = profEmailtb.Text,
                        FirstName = firstNametb.Text,
                        LastName = lastNametb.Text,
                        Username = felhasznaloNevtb.Text
                    };

                    // Felhasználó frissítése
                    userService.UpdateUser(loggedInUser.Id, updateUserDto);
                    MessageBox.Show("Felhasználó adatai frissítve.", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    DisableEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a felhasználó adatainak frissítésekor: {ex.Message}");
            }
        }
        /// <summary>
        /// Szerkesztés gomb eseménykezelője.
        /// </summary>
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            EnableEdit();
        }
        /// <summary>
        /// Szerkesztés tiltása.
        /// </summary>
        private void DisableEdit()
        {
            felhasznaloNevtb.IsEnabled = false;
            firstNametb.IsEnabled = false;
            lastNametb.IsEnabled = false;
            profEmailtb.IsEnabled = false;
            felhPW.IsEnabled = true;

            Editbt.Visibility = Visibility.Visible;
            Modify.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Szerkesztés engedélyezése.
        /// </summary>
        private void EnableEdit()
        {
            felhasznaloNevtb.IsEnabled = true;
            firstNametb.IsEnabled = true;
            lastNametb.IsEnabled = true;
            profEmailtb.IsEnabled = true;
            felhPW.IsEnabled = false;

            Editbt.Visibility = Visibility.Collapsed;
            Modify.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Jelszóváltoztatás gomb eseménykezelője.
        /// </summary>
        private void FelhPW_Click(object sender, RoutedEventArgs e)
        {
            DisableEdit();
            ChangePass passwin = new ChangePass(loggedInUser);
            passwin.Closed += (s, ev) => {
             

            };
            passwin.ShowDialog();
        }
    }
}