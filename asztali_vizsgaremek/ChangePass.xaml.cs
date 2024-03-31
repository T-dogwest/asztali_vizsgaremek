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
    /// Interaction logic for ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {

        private FelhasznmalokItem loggedInUser;
        FelhasznaloService service = new FelhasznaloService();
        
        public ChangePass(FelhasznmalokItem user)
        {
            InitializeComponent();
            loggedInUser = user;
        }


        private void Button_Change(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(oldpw.Password) || string.IsNullOrWhiteSpace(newpw.Password))
                {
                    MessageBox.Show("Kérjük, töltse ki mindkét jelszómezőt.");
                    return;
                }

                if (loggedInUser == null)
                {
                    MessageBox.Show("Nincs bejelentkezett felhasználó.");
                    return;
                }

                ChangePasswordDTO changePasswordDto = new ChangePasswordDTO
                {
                    OldPassword = oldpw.Password,
                    NewPassword = newpw.Password
                };

                int userId = loggedInUser.Id;

                service.ChangePassword(userId, changePasswordDto);
                MessageBox.Show("Jelszó sikeresen megváltoztatva!");

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

        private void CloseAdminWindow()
        {
             //Admin ablak bezárása
            var adminWindow = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            if (adminWindow != null)
                adminWindow.Close();
        }
        private void OpenLoginWindow()
        {
            // Login ablak megnyitása
            var loginWindow = new Login();
            loginWindow.Show();
        }

    }
}