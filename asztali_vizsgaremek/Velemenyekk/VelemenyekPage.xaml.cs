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
    /// Interaction logic for Velemenyek.xaml
    /// </summary>
    public partial class VelemenyekPage : Page
    {
        VelemenyekServices services = new VelemenyekServices();
        List<VelemenyekItem> velemenyekList;


        public VelemenyekPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            velemenyekList = services.GetAll();
            VelemenyTable.ItemsSource = velemenyekList;
        }

        private void VelemenyTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VelemenyTable.SelectedItem != null)
            {
                VelemenyekItem selectedItem = (VelemenyekItem)VelemenyTable.SelectedItem;
                ertekelesTB.Text = selectedItem.Rate.ToString();
                velemenyTB.Text = selectedItem.Content;
            }
        }

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
                        MessageBox.Show("A törlés sikeres volt.", "Sikeres törlés", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshData();
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("A törlés sikertelen volt.", "Sikertelen törlés", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem válassza ki a törlendő elemet!", "Nincs kiválasztva elem", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearTextBoxes()
        {
            ertekelesTB.Text = string.Empty;
            velemenyTB.Text = string.Empty;
        }


    }
}