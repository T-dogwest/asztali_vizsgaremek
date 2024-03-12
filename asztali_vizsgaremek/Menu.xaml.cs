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
using static asztali_vizsgaremek.MenuItem;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        MenuServices services = new MenuServices();
        MenuItem menu;

        public Menu()
        {
            InitializeComponent();
            InitializeComboBox();
            MenuTable.ItemsSource = services.GetAll();
        }

        private void InitializeComboBox()
        {
            
            cbTipus.ItemsSource = Enum.GetValues(typeof(MenuType));
        }

        private void Button_MenuHozzaad(object sender, RoutedEventArgs e)
        {

            try
            {
                MenuDTO menu = CreateMenuFromInputFields();
                MenuItem newMenu = services.Add(menu);
                if (newMenu.Id != 0)
                {
                    MessageBox.Show("Sikeres felvétel");
                    tbNameDrink.Text = "";
                    tbPriceDrink.Text = "";
                    cbTipus.SelectedItem = null;
                    MenuTable.ItemsSource = services.GetAll(); 
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során: ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private MenuDTO CreateMenuFromInputFields()
        {
            string Name = tbNameDrink.Text.Trim();
            string Price = tbPriceDrink.Text.Trim();
            MenuType? Type = cbTipus.SelectedItem as MenuType?;

            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Név kitöltése kötelező!");
            }

            if (string.IsNullOrEmpty(Price))
            {
                throw new Exception("Ár kitöltése kötelező!");
            }

           
            if (!int.TryParse(Price, out int priceValue))
            {
                throw new Exception("Az árnak numerikus értéknek kell lennie!");
            }

            
            if (Type == null)
            {
                throw new Exception("Típus kiválasztása kötelező!");
            }


            MenuDTO menu = new MenuDTO();
            menu.Name = Name;
            menu.Price = priceValue;
            menu.ItemType = Type.Value;
            return menu;
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (MenuTable.SelectedItem != null)
            {
                try
                {
                    // Kiválasztott elem törlése
                    MenuItem selectedMenu = (MenuItem)MenuTable.SelectedItem;
                    bool deleteSuccess = services.Delete(selectedMenu); // Módosítás itt

                    if (deleteSuccess)
                    {
                        MenuTable.ItemsSource = services.GetAll();
                        MessageBox.Show("Elem sikeresen törölve.");
                    }
                    else
                    {
                        MessageBox.Show("A törlés nem sikerült.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a törlés során: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy elemet a törléshez.");
            }
        }

        private void Button_Modify(object sender, RoutedEventArgs e)
        {
            try
            {
                // Módosítás végrehajtása
                MenuItem selectedMenu = (MenuItem)MenuTable.SelectedItem;
                MenuDTO menu = CreateMenuFromInputFields();

                bool success = services.Update(selectedMenu.Id, menu);

                if (success)
                {
                    MessageBox.Show("Sikeres módosítás");
                    tbNameDrink.Text = "";
                    tbPriceDrink.Text = "";

                    // Módosítás gomb elrejtése és a hozzáadás gomb megjelenítése
                    modify.Visibility = Visibility.Visible;
                    add.Visibility = Visibility.Collapsed;

                    MenuTable.ItemsSource = services.GetAll();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }
}
