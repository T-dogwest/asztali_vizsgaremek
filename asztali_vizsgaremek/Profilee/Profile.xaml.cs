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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private FelhasznmalokItem loggedInUser;

        public Profile()
        {
            InitializeComponent();
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
                MessageBox.Show($"Hiba: {ex.Message}");
            }
        }

        private void FillUserData(FelhasznmalokItem loggedInUser)
        {
            felhasznaloNevtb.Text = loggedInUser.Username;
            firstNametb.Text = loggedInUser.First_name;
            lastNametb.Text = loggedInUser.Last_name;
            profEmailtb.Text = loggedInUser.Email;
        }


        private void Button_Modify(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(felhasznaloNevtb.Text) &&
                !string.IsNullOrWhiteSpace(firstNametb.Text) &&
                !string.IsNullOrWhiteSpace(lastNametb.Text) &&
                !string.IsNullOrWhiteSpace(profEmailtb.Text))
            {
                MessageBoxResult result = MessageBox.Show("Biztosan szeretné frissíteni a felhasználó adatait?", "Megerősítés", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    UpdateFelhasznaloDTO updateUserDto = new UpdateFelhasznaloDTO
                    {
                        Email = profEmailtb.Text,
                        FirstName = firstNametb.Text,
                        LastName = lastNametb.Text,
                        Username = felhasznaloNevtb.Text
                    };

                    int userId = loggedInUser.Id;

                    FelhasznaloService userService = new FelhasznaloService();
                    try
                    {
                        userService.UpdateUser(userId, updateUserDto);
                        MessageBox.Show("Felhasználó adatai frissítve.");
                        DisableEdit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérlek tölts ki minden mezőt a frissítéshez.");
            }
        }
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            EnableEdit();
        }

        private void DisableEdit()
        {
            felhasznaloNevtb.IsEnabled = false;
            firstNametb.IsEnabled = false;
            lastNametb.IsEnabled = false;
            profEmailtb.IsEnabled = false;
            felhPW.IsEnabled = false;

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

            Editbt.Visibility = Visibility.Collapsed;
            Modify.Visibility = Visibility.Visible;
        }

        private void felhPW_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}