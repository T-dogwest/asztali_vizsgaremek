using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        MenuServices services=new MenuServices();
        Menucs menu;
        public Menu()
        {

            InitializeComponent();
            MenuTable.ItemsSource = services.GetAll();
        }

        private void Button_MenuHozzaad(object sender, RoutedEventArgs e)
        {
            try
            {
                Menucs menu = CreateMenuFromInputFields();
                Menucs newMenu = services.Add(menu);
                if (newMenu.Id != 0)
                {
                    MessageBox.Show("Sikeres felvétel");
                    tbNameDrink.Text = "";
                    tbPriceDrink.Text = "";
                    tlTipus.SelectedItem = null;
                    
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Menucs CreateMenuFromInputFields()
        {
            string NameDrink = tbNameDrink.Text.Trim();
            string Price = tbPriceDrink.Text.Trim();
            string Tipus = listBoxTipus.SelectedItem?.ToString(); // ListBox-ból kiválasztott elem lekérése


            if (string.IsNullOrEmpty(Fullname))
            {
                throw new Exception("Név kitöltése kötelező!");
            }

            if (string.IsNullOrEmpty(Email) || new System.Net.Mail.MailAddress(Email).Address != Email)
            {
                throw new Exception("Nem hagyhatod üresen és érvényes emailt kell megadnod!");
            }

            if (!Regex.IsMatch(Phone, @"^\(\d{3}\) \d{3}-\d{4}$"))
            {
                throw new Exception("Érvénytelen telefonszám ");
            }
            if (string.IsNullOrEmpty(Phone))
            {
                throw new Exception("Nem hagyhatod üresen");
            }
            if (string.IsNullOrEmpty(Holidayss))
            {
                throw new Exception("Nem hagyhatod üresen");
            }
            if (!int.TryParse(Holidayss, out int Holidays))
            {
                throw new Exception("A napok számát add meg");
            }
            if (Holidays > 20 || Holidays < 0)
            {
                throw new Exception("Maximum 20 szabadság lehet");
            }


            Employees employees = new Employees();
            employees.Fullname = Fullname;
            employees.Email = Email;
            employees.Phone = Phone;
            employees.Holidays = Holidays;
            return employees;
        }
    }
}
