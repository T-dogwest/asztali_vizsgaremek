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

namespace asztali_vizsgaremek.Velemenyekk
{
    /// <summary>
    /// A vélemények megjelenítéséért és kezeléséért felelős felületi elemeket tartalmazó osztály.
    /// </summary>
    public partial class VelemenyekPage : Page
    {
        VelemenyekServices services = new VelemenyekServices();
        List<VelemenyekItem> velemenyekList;

        /// <summary>
        /// A VelemenyekPage osztály konstruktora.
        /// Frissíti az adatokat az oldal megjelenítésekor.
        /// </summary>
        public VelemenyekPage()
        {
            InitializeComponent();
            RefreshData();
        }
        /// <summary>
        /// Az adatok frissítését végző metódus.
        /// Lekéri az összes véleményt a szerverről és betölti azokat a táblázatba.
        /// </summary>
        private void RefreshData()
        {
            velemenyekList = services.GetAll();
            VelemenyTable.ItemsSource = velemenyekList;
        }
        /// <summary>
        /// A vélemények táblázatának kiválasztásának eseménykezelője.
        /// Betölti az adott kiválasztott vélemény részleteit a megfelelő szövegmezőkbe.
        /// </summary>
        private void VelemenyTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VelemenyTable.SelectedItem != null)
            {
                VelemenyekItem selectedItem = (VelemenyekItem)VelemenyTable.SelectedItem;
                ertekelesTB.Text = selectedItem.Rate.ToString();
                velemenyTB.Text = selectedItem.Content;
            }
        }

        /// <summary>
        /// Az elem törlésének gombjának kattintásának eseménykezelője.
        /// Törli a kiválasztott véleményt a táblázatból és a szerverről.
        /// </summary>
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (VelemenyTable.SelectedItem != null)
            {
                VelemenyekItem selectedVelemeny = (VelemenyekItem)VelemenyTable.SelectedItem;

                MessageBoxResult result = MessageBox.Show("Biztosan törölni szeretné ezt az elemet?",
                                                            "Törlés megerősítése",
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool success = services.Delete(selectedVelemeny);
                    if (success)
                    {
                        MessageBox.Show("A törlés sikeres volt.", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshData();
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("A törlés sikertelen volt.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válassza ki a törlendő elemet!", "Közlés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// A szövegmezők kiürítését végző metódus.
        /// </summary>
        private void ClearTextBoxes()
        {
            ertekelesTB.Text = string.Empty;
            velemenyTB.Text = string.Empty;
        }


    }
}