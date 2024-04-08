using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace asztali_vizsgaremek.Attekintes
{
    /// <summary>
    /// Interaction logic for Attekintes.xaml
    /// </summary>
    public partial class AttekintesPage : Page
    {   AttekintesService service=new AttekintesService();
        private List<AttekintesItem> allItems;
        public AttekintesPage()
        {
            InitializeComponent();
            InitializeComboBox();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                allItems = service.GetAll();
                
                if (allItems != null)
                {
                    AttekintesDG.ItemsSource = allItems;
                }
                else
                {
                    MessageBox.Show("Az adatok betöltése nem sikerült: A kapott adatok null értéket adnak vissza.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt az adatok betöltése közben: {ex.Message}");
            }
        }
        private void FilterDataGrid(ReservationState selectedState)
        {
            if (allItems != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(allItems);
                view.Filter = item =>
                {
                    AttekintesItem attekintesItem = item as AttekintesItem;
                    return attekintesItem.State == selectedState;
                };

                if (view.IsEmpty)
                {
                    // Ha a szűrt lista üres, ürítsd ki a DataGrid-et
                    AttekintesDG.ItemsSource = null;
                }
                else
                {
                    // Ellenkező esetben frissítsd a DataGrid-et a szűrt elemekkel
                    AttekintesDG.ItemsSource = view;
                }
            }
            else
            {
                // Ha az allItems null, jelezd ezt vagy kezeld le a hibát
                MessageBox.Show("Az allItems változó null értéket tartalmaz.");
            }
        }

        private void InitializeComboBox()
        {
            stateComboBox.ItemsSource = Enum.GetValues(typeof(ReservationState));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stateComboBox.SelectedItem != null)
            {
                ReservationState selectedState = (ReservationState)stateComboBox.SelectedItem;
                FilterDataGrid(selectedState);
            }
        }


        private void Button_Done(object sender, RoutedEventArgs e)
        {
            if (AttekintesDG.SelectedItem != null )
            {
                AttekintesItem selectedItem = (AttekintesItem)AttekintesDG.SelectedItem;

                if (selectedItem.State != ReservationState.Cancelled)
                {
                    try
                    {
                        int id = selectedItem.Id;
                        UpdateStateDto dto = new UpdateStateDto { State = ReservationState.Done };
                        service.Update(id, dto);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("A kiválasztott foglalás már 'Cancelled' állapotban van, ezért nem lehet 'Done' állapotba állítani.");
                }
            }
            else
            {
                MessageBox.Show("Válassz egy elemet előbb.");
            }
        }

        private void Button_Cancelled(object sender, RoutedEventArgs e)
        {
            if (AttekintesDG.SelectedItem != null)
            {
                try
                {
                    AttekintesItem selectedItem = (AttekintesItem)AttekintesDG.SelectedItem;
                    int id = selectedItem.Id;
                    UpdateStateDto dto = new UpdateStateDto { State = ReservationState.Cancelled };
                    service.Update(id, dto);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Válassz egy elemet elöbb.");
            }
        }

        private void Button_Basket(object sender, RoutedEventArgs e)
        {
            if (AttekintesDG.SelectedItem != null)
            {
                AttekintesItem selected = AttekintesDG.SelectedItem as AttekintesItem;
                RendelesWindow rendeles = new RendelesWindow(selected);
              
                rendeles.Closed += (_, _) =>
                {
                    LoadData();
                };
                rendeles.ShowDialog();
            }
            else
            {
                MessageBox.Show("Válassz ki egy elemet a listából, mielőtt megnyomnád a Kosár gombot.");
            }
        }

    }
}
