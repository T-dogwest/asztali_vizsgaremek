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


namespace asztali_vizsgaremek.Menu
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        MenuServices services = new MenuServices();


        public MenuPage()
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
                    MessageBox.Show("Sikeres felvétel", "közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputFields();
                    RefreshMenuTable();
                }
                else
                {
                    MessageBox.Show("Hiba történt a felvétel során: ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void Button_Torles(object sender, RoutedEventArgs e)
        {
            if (MenuTable.SelectedItem != null)
            {
                try
                {

                    MenuItem selectedMenu = (MenuItem)MenuTable.SelectedItem;
                    bool deleteSuccess = services.Delete(selectedMenu);

                    if (deleteSuccess)
                    {
                        RefreshMenuTable();
                        MessageBox.Show("Elem sikeresen törölve.", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("A törlés nem sikerült.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt a törlés során: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy elemet a törléshez.", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Vissza(object sender, RoutedEventArgs e)
        {
            MenuTable.SelectedItem= null;
            add.Visibility = Visibility.Visible;

            modify.Visibility = Visibility.Collapsed;

            ClearInputFields();
        }

        private void Button_Modositas(object sender, RoutedEventArgs e)
        {
            MenuItem selected = MenuTable.SelectedItem as MenuItem;
            if (selected == null)
            {
                MessageBox.Show("Módosításhoz előbb válasszon ki elemet!", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MenuDTO menu = CreateMenuFromInputFields();
                MenuItem update = services.Update(selected.Id, menu);
                if (update != null)
                {
                    MessageBox.Show("Sikeres módosítás", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshMenuTable();
                    ClearInputFields();
                    add.Visibility = Visibility.Visible;

                    modify.Visibility = Visibility.Collapsed;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a módosítás során: " + ex.Message);
            }

        }
        private MenuDTO CreateMenuFromInputFields()
        {
            string Name = tbMenuName.Text.Trim();
            string Price = tbMenuPrice.Text.Trim();
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
        private void RefreshMenuTable()
        {
            MenuTable.ItemsSource = services.GetAll();
        }

        private void ClearInputFields()
        {
            tbMenuName.Text = "";
            tbMenuPrice.Text = "";
            cbTipus.Text = null;
        }
        private void MenuTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuTable.SelectedItem != null)
            {
                add.Visibility = Visibility.Collapsed;
                modify.Visibility = Visibility.Visible;
                
                MenuItem selectedMenuItem = (MenuItem)MenuTable.SelectedItem;
                tbMenuName.Text = selectedMenuItem.Name;
                tbMenuPrice.Text = selectedMenuItem.Price.ToString();
                cbTipus.SelectedItem = selectedMenuItem.ItemType;
                CreateMenuFromInputFields();
            }
        }




    }


}
