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
using static asztali_vizsgaremek.Menucs;

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
            InitializeComboBox();
            MenuTable.ItemsSource = services.GetAll();
        }
        private void InitializeComboBox()
        {
            // Az enum értékek lekérése
            Array types = Enum.GetValues(typeof(MenuType));

            // ComboBox feltöltése az enum értékekkel
            foreach (MenuType type in types)
            {
                cbTipus.Items.Add(type);
            }
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
                    cbTipus.SelectedItem = null;
                    
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
            string Name = tbNameDrink.Text.Trim();
            string Price = tbPriceDrink.Text.Trim();
            int Type =cbTipus.SelectedIndex;

            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Név kitöltése kötelező!");
            }

            if (string.IsNullOrEmpty(Price))
            {
                throw new Exception("Ár kitöltése kötelező!");
            }

            // Ár ellenőrzése
            if (!int.TryParse(Price, out int priceValue))
            {
                throw new Exception("Az árnak numerikus értéknek kell lennie!");
            }

            // Menü objektum inicializálása és beállítása
            Menucs menue = new Menucs();
            menue.Name = Name;
            menue.Price = priceValue;
            menue.ItemType = (Menucs.MenuType)Type;
            return menue;
        }
    }
}
