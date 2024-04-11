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
    /// Az AttekintesPage osztály az áttekintő oldalt reprezentálja, ahol a foglalásokat megjelenítik és kezelik.
    /// </summary>
    public partial class AttekintesPage : Page
    {   AttekintesService service=new AttekintesService();
        private List<AttekintesItem> allItems;
        /// <summary>
        /// Az AttekintesPage osztály konstruktora, inicializálja az oldalt és betölti az adatokat.
        /// </summary>
        public AttekintesPage()
        {
            InitializeComponent();
            InitializeComboBox();
            LoadData();
        }
        /// <summary>
        /// Betölti az adatokat az adatforrásból és megjeleníti azokat a DataGrid-ben.
        /// </summary>
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
                    MessageBox.Show("Az adatok betöltése nem sikerült: A kapott adatok null értéket adnak vissza.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt az adatok betöltése közben: {ex.Message}", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Szűri a DataGrid-et a kiválasztott foglalásállapot alapján.
        /// </summary>
        /// <param name="selectedState">A kiválasztott foglalásállapot.</param>
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
                  
                    AttekintesDG.ItemsSource = null;
                }
                else
                {
                   
                    AttekintesDG.ItemsSource = view;
                }
            }
            else
            {
                
                MessageBox.Show("Az allItems változó null értéket tartalmaz.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Inicializálja a ComboBox-ot a foglalásállapotokkal.
        /// </summary>
        private void InitializeComboBox()
        {
            stateComboBox.ItemsSource = Enum.GetValues(typeof(ReservationState));

        }
        /// <summary>
        /// A ComboBox kiválasztott elemének megváltozásakor meghívódó eseménykezelő.
        /// </summary>
        /// <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stateComboBox.SelectedItem != null)
            {
                ReservationState selectedState = (ReservationState)stateComboBox.SelectedItem;
                FilterDataGrid(selectedState);
            }
        }

        /// <summary>
        /// A "Done" gombra kattintáskor meghívódó eseménykezelő.
        /// Állapotátállítja a kiválasztott foglalást "Done" állapotra.
        /// </summary>
        /// <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
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
                        
                         ReservationState selectedState = (ReservationState)stateComboBox.SelectedItem;
                        FilterDataGrid(selectedState);
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("A kiválasztott foglalás már 'Cancelled' állapotban van, ezért nem lehet 'Done' állapotba állítani.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Válassz egy elemet előbb.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        /// <summary>
        /// A "Cancelled" gombra kattintáskor meghívódó eseménykezelő.
        /// Állapotátállítja a kiválasztott foglalást "Cancelled" állapotra.
        /// </summary>
        /// <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
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

                    ReservationState selectedState = (ReservationState)stateComboBox.SelectedItem;
                    FilterDataGrid(selectedState);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Válassz egy elemet elöbb.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// A "Basket" gombra kattintáskor meghívódó eseménykezelő.
        /// Megnyitja a kiválasztott elem kosarát tartalmazó ablakot.
        /// </summary>
        /// <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
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
                MessageBox.Show("Válassz ki egy elemet a listából, mielőtt megnyomnád a Kosár gombot.", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
