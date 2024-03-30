using Microsoft.VisualBasic.ApplicationServices;
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


namespace asztali_vizsgaremek.User
{
    /// <summary>
    /// Interaction logic for Felhasznalok.xaml
    /// </summary>
    public partial class FelhasznalokPage : Page
    {

        FelhasznaloService services = new FelhasznaloService();
        public FelhasznalokPage()
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
                    LoadData();
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
            
            FelhasznmalokItem selected = UserTable.SelectedItem as FelhasznmalokItem;
            if (selected == null)
            {
                MessageBox.Show("Válasszon ki egy elemet a törléshez!");
                return;
            }

          
            MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné ezt az elemet?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
               
                try
                {
                 
                    bool deleted = services.Delete(selected);
                    if (deleted)
                    {
                        MessageBox.Show("Sikeres törlés");
                        LoadData(); 
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt a törlés során");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a törlés során: " + ex.Message);
                }
            }
        }

        private async void LoadData()
        {
            List<FelhasznmalokItem> felhasznalok = services.GetAll();
            List<FelhasznmalokItem> filteredFelhasznalok = felhasznalok.Select(item =>
                new FelhasznmalokItem
                {
                    Id = item.Id,
                    Username = item.Username,
                    Email = item.Email,
                    Password = item.Password,
                    First_name = item.First_name,
                    Last_name = item.Last_name,
                    Role = item.Role,
                }).ToList();

            UserTable.ItemsSource = filteredFelhasznalok;
        }
        private FelhasznalokDTO CreateAdminFromInputFields()
        {
            string FelName = tbFelhnev.Text.Trim();
            string Email = tbEmail.Text.Trim();
            string Password = tbJelszo.Password.Trim();
            string FirstName = tbkeresztnev.Text.Trim();
            string LastName = tbVezeteknev.Text.Trim();





            if (string.IsNullOrWhiteSpace(FelName))
            {
              
                MessageBox.Show("Felhasználónév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        
            if (string.IsNullOrWhiteSpace(Email))
            {

                MessageBox.Show("E-mail cím megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else if (!IsValidEmail(Email))
            {
               
                MessageBox.Show("Helytelen e-mail formátum!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

          
            if (string.IsNullOrWhiteSpace(Password))
            {
             
                MessageBox.Show("Jelszó megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

     
            if (string.IsNullOrWhiteSpace(FirstName))
            {
        
                MessageBox.Show("Keresztnév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

      
            if (string.IsNullOrWhiteSpace(LastName))
            {
                
                MessageBox.Show("Vezetéknév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            FelhasznalokDTO felh = new FelhasznalokDTO();
            felh.Username = FelName;
            felh.Email = Email;
            felh.Password = Password;
            felh.First_name = FirstName;
            felh.Last_name = LastName;



            return felh;



        }

      
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
            tbJelszo.Password = "";
            tbkeresztnev.Text = "";
            tbVezeteknev.Text = "";

        }

    }
}
