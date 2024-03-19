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
using static asztali_vizsgaremek.MenuItem;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for Felhasznalok.xaml
    /// </summary>
    public partial class Felhasznalok : Page
    {
        FelhasznaloService services = new FelhasznaloService();
        public Felhasznalok()
        {
            InitializeComponent();
            LoadData();


        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                FelhasznalokDTO admin = CreateAdminFromInputFields();
                FelhasznmalokItem newadmin = services.Add(admin);
                if (newadmin.Id != 0)
                {
                    MessageBox.Show("Sikeres felvétel");
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Button_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {

        }
        private async void LoadData()
        {
            List<FelhasznmalokItem> felhasznalok = services.GetAll();
            List<FelhasznmalokItem> filteredFelhasznalok = felhasznalok.Select(item =>
                new FelhasznmalokItem
                {
                    First_name = item.First_name,
                    Last_name = item.Last_name,
                    Email = item.Email,
                    RoleType = item.RoleType,
                }).ToList();

            UserTable.ItemsSource = filteredFelhasznalok;
        }
        private FelhasznalokDTO CreateAdminFromInputFields()
        {
            string FelName = tbFelhnev.Text.Trim();
            string Email = tbEmail.Text.Trim();
            string Password = tbJelszo.Text.Trim();
            string FirstName = tbkeresztnev.Text.Trim();
            string LastName = tbVezeteknev.Text.Trim();
            
           


            if (string.IsNullOrWhiteSpace(FelName))
            {
                // Hibakezelés: Felhasználónév üres
                MessageBox.Show("Felhasználónév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Validáció: Email üres-e és helyes formátumban van-e?
            if (string.IsNullOrWhiteSpace(Email))
            {
                // Hibakezelés: Email üres
                MessageBox.Show("E-mail cím megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else if (!IsValidEmail(Email))
            {
                // Hibakezelés: Helytelen e-mail formátum
                MessageBox.Show("Helytelen e-mail formátum!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Validáció: Jelszó üres-e?
            if (string.IsNullOrWhiteSpace(Password))
            {
                // Hibakezelés: Jelszó üres
                MessageBox.Show("Jelszó megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Validáció: Keresztnév üres-e?
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                // Hibakezelés: Keresztnév üres
                MessageBox.Show("Keresztnév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Validáció: Vezetéknév üres-e?
            if (string.IsNullOrWhiteSpace(LastName))
            {
                // Hibakezelés: Vezetéknév üres
                MessageBox.Show("Vezetéknév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

           FelhasznalokDTO felh = new FelhasznalokDTO();
            felh.Username=FelName;
            felh.Email=Email;
            felh.Password=Password;
            felh.First_name=FirstName;
            felh.Last_name=LastName;
            felh.roleType = RoleType.Admin;
           
            return felh;



        }

        // Segédfüggvény az e-mail formátum ellenőrzésére
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
       

        private void ClearInputFields()
        {
            tbFelhnev.Text = "";
            tbEmail.Text = "";
            tbJelszo.Text = "";
            tbkeresztnev.Text = "";
            tbVezeteknev.Text = "";
            
        }
        private void MenuTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserTable.SelectedItem != null)
            {
                add.Visibility = Visibility.Collapsed;
                modify.Visibility = Visibility.Visible;
                // Itt ellenőrizheted, hogy melyik elem van kiválasztva a DataGrid-ben
                FelhasznmalokItem selecteduserItem = (FelhasznmalokItem)UserTable.SelectedItem;
                tbFelhnev.Text = selecteduserItem.Username;
                tbEmail.Text = selecteduserItem.Email;
                tbJelszo.Text=selecteduserItem.Password;
                tbkeresztnev.Text=selecteduserItem.Password;
                tbVezeteknev.Text = selecteduserItem.Email;
                CreateAdminFromInputFields();
            }
        }
    }
}
