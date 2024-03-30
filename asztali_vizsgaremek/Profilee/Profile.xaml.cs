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
using asztali_vizsgaremek.User;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private FelhasznmalokItem loggedInUser;
        public Profile(FelhasznmalokItem user)
        {
            InitializeComponent();
            loggedInUser = user;
            FillUserData(loggedInUser); // Átadjuk a bejelentkezett felhasználó adatait a FillUserData metódusnak
            LoadLoggedInUserData();
        }
        public void LoadLoggedInUserData()
        {
            FelhasznaloService userService = new FelhasznaloService();
            try
            {
                loggedInUser = userService.GetLoggedInUserData();
                if (loggedInUser != null)
                {
                    FillUserData(loggedInUser);
                }
                else
                {
                    MessageBox.Show("Nem sikerült betölteni a bejelentkezett felhasználó adatait.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a bejelentkezett felhasználó adatainak betöltésekor: " + ex.Message);
            }
        }


        private void FillUserData(FelhasznmalokItem loggedInUser)
        {
            felhasznaloNevtb.Text = loggedInUser.Username;
            firstNametb.Text = loggedInUser.First_name;
            lastNametb.Text = loggedInUser.Last_name;
            profEmailtb.Text = loggedInUser.Email;
            // A jelszót nem töltjük be alapértelmezésben
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            EnableEdit();
        }

        private void Button_Modify(object sender, RoutedEventArgs e)
        {
            // Ellenőrizd, hogy a felhasználó beírt-e új adatokat a felhasználói felületen
            if (!string.IsNullOrWhiteSpace(felhasznaloNevtb.Text) &&
                !string.IsNullOrWhiteSpace(firstNametb.Text) &&
                !string.IsNullOrWhiteSpace(lastNametb.Text) &&
                !string.IsNullOrWhiteSpace(profEmailtb.Text))
            {
                // Megerősítő párbeszédablak megjelenítése
                MessageBoxResult result = MessageBox.Show("Biztosan szeretné frissíteni a felhasználó adatait?", "Megerősítés", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // UpdateUserDto létrehozása az új adatokkal
                    UpdateFelhasznaloDTO updateUserDto = new UpdateFelhasznaloDTO
                    {
                        Email = profEmailtb.Text,
                        FirstName = firstNametb.Text,
                        LastName = lastNametb.Text,
                        Username = felhasznaloNevtb.Text
                    };

                    // Felhasználó azonosítójának lekérése (feltételezem, hogy a loggedInUser.Id tartalmazza az azonosítót)
                    int userId = loggedInUser.Id;

                    // Felhasználószolgáltatás példányosítása és felhasználó frissítése
                    FelhasznaloService userService = new FelhasznaloService();
                    try
                    {
                        userService.UpdateUser(userId, updateUserDto);
                        // Sikeres frissítés esetén üzenet megjelenítése
                        MessageBox.Show("Felhasználó adatai frissítve.");
                        DisableEdit();
                    }
                    catch (Exception ex)
                    {
                        // Hiba esetén hibaüzenet megjelenítése
                        MessageBox.Show("Hiba történt a felhasználó adatainak frissítésekor: " + ex.Message);
                    }
                }
            }
            else
            {
                // Ha a felhasználó nem töltötte ki az összes mezőt, akkor erről tájékoztatjuk
                MessageBox.Show("Kérlek tölts ki minden mezőt a frissítéshez.");
            }
        }

        private void DisableEdit()
        {
            felhasznaloNevtb.IsEnabled = false;
            firstNametb.IsEnabled = false;
            lastNametb.IsEnabled = false;
            profEmailtb.IsEnabled = false;
            felhPW.IsEnabled = false;

            // Szerkesztés gomb megjelenítése, módosítás gomb elrejtése
            Editbt.Visibility = Visibility.Visible;
            Modify.Visibility = Visibility.Collapsed;
        }

        private void EnableEdit()
        {
            felhasznaloNevtb.IsEnabled = true;
            firstNametb.IsEnabled = true;
            lastNametb.IsEnabled = true;
            profEmailtb.IsEnabled = true;
            felhPW.IsEnabled = true;

            // Módosítás gomb megjelenítése, szerkesztés gomb elrejtése
            Editbt.Visibility = Visibility.Collapsed;
            Modify.Visibility = Visibility.Visible;
        }

      
    }
}