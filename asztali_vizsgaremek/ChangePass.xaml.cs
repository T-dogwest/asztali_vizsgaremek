using asztali_vizsgaremek.Admin;
using asztali_vizsgaremek.Profilee;
using asztali_vizsgaremek.User;
using Microsoft.Identity.Client.NativeInterop;
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
    /// Az ablak, amely lehetővé teszi a felhasználó jelszavának megváltoztatását.
    /// </summary>
    public partial class ChangePass : Window
    {

        private FelhasznmalokItem loggedInUser;
        FelhasznaloService service = new FelhasznaloService();

        /// <summary>
        /// ChangePass ablak létrehozása a megadott felhasználóval.
        /// </summary>
        /// <param name="user">A bejelentkezett felhasználó adatai.</param>

        public ChangePass(FelhasznmalokItem user)
        {
            InitializeComponent();
            loggedInUser = user;
        }

        /// <summary>
        /// A jelszó megváltoztatását kezelő eseménykezelő.
        /// </summary>
        private void Button_Change(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(oldpw.Password) || string.IsNullOrWhiteSpace(newpw.Password))
                {
                    MessageBox.Show("Kérjük, töltse ki mindkét jelszómezőt.", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (loggedInUser == null)
                {
                    MessageBox.Show("Nincs bejelentkezett felhasználó.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ChangePasswordDTO changePasswordDto = new ChangePasswordDTO
                {
                    OldPassword = oldpw.Password,
                    NewPassword = newpw.Password
                };

                int userId = loggedInUser.Id;

                service.ChangePassword(userId, changePasswordDto);
                MessageBox.Show("Jelszó sikeresen megváltoztatva!", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);

                // Bezárjuk a ChangePass ablakot
                Close();

                OpenLoginWindow();
                // Bezárjuk az AdminWindow-t
                CloseAdminWindow();

                // Megnyitjuk a Login ablakot
              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a jelszó megváltoztatása során: {ex.Message}");
            }
        }
        /// <summary>
        /// Az Admin ablak bezárását végző metódus.
        /// </summary>
        private void CloseAdminWindow()
        {
             //Admin ablak bezárása
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
                adminWindow.Close();
        }  /// <summary>
           /// A bejelentkező ablak megnyitását végző metódus.
           /// </summary>
        private void OpenLoginWindow()
        {
            // Login ablak megnyitása
            var loginWindow = new Login();
            loginWindow.Show();
        }

    }
}