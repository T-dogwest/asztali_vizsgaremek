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

        /// <summary>
        /// A MenuPage osztály konstruktora.
        /// Inicializálja a felhasználói felületet és betölti az összes elemet a menüből.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();
            InitializeComboBox();
            MenuTable.ItemsSource = services.GetAll();


        }
        /// <summary>
        /// Inicializálja a típusok legördülő listáját a menü oldalon.
        /// </summary>
        private void InitializeComboBox()
        {

            cbTipus.ItemsSource = Enum.GetValues(typeof(MenuType));
        }
        /// <summary>
        /// Az "Hozzáadás" gomb eseménykezelője.
        /// Létrehozza az új menüelemet a felhasználói bemenetek alapján, majd hozzáadja a menühöz.
        /// </summary>
        private void Button_Add(object sender, RoutedEventArgs e)
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
        /// <summary>
        /// Az "Törlés" gomb eseménykezelője.
        /// Az kiválasztott elem törlése a menüből.
        /// </summary>
        private void Button_Delete(object sender, RoutedEventArgs e)
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
        /// <summary>
        /// A "Vissza" gomb eseménykezelője.
        /// Törli a kijelölt elemet és törli a beviteli mezőket.
        /// </summary>
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MenuTable.SelectedItem= null;
            add.Visibility = Visibility.Visible;

            modify.Visibility = Visibility.Collapsed;

            ClearInputFields();
        }
        /// <summary>
        /// A "Módosítás" gomb eseménykezelője.
        /// Módosítja a kijelölt elemet a beviteli mezők értékeivel.
        /// </summary>
        private void Button_Modify(object sender, RoutedEventArgs e)
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
        /// <summary>
        /// Az input mezőkből létrehozza a menüt.
        /// </summary>
        /// <returns>A létrehozott menü objektum.</returns>
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
        /// <summary>
          /// Frissíti a menü táblázatot az aktuális menüelemekkel.
          /// </summary>
        private void RefreshMenuTable()
        {
            MenuTable.ItemsSource = services.GetAll();
        }
        /// <summary>
        /// Törli a beviteli mezők tartalmát.
        /// </summary>
        private void ClearInputFields()
        {
            tbMenuName.Text = "";
            tbMenuPrice.Text = "";
            cbTipus.Text = null;
        }
        /// <summary>
        /// A menü táblázat kijelölésének eseménykezelője.
        /// Betölti a kiválasztott menüelem adatait a beviteli mezőkbe a módosításhoz.
        /// </summary>
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
