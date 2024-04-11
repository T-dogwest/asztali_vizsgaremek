using asztali_vizsgaremek.Attekintes;
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
using System.Windows.Shapes;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// A RendelesWindow.xaml logikája
    /// </summary>
    public partial class RendelesWindow : Window
    {
        AttekintesItem selected ;
       
        /// <summary>
        /// A RendelesWindow osztály konstruktora.
        /// </summary>
        /// <param name="selectedItem">Az a kiválasztott tétel, amelyhez a rendelésablak tartozik.</param>

        public RendelesWindow(AttekintesItem selectedItem)
        {
            
            InitializeComponent();
            selected = selectedItem;
            Loadbasket();

           
        }
        /// <summary>
        /// Betölti a kosár tartalmát a megfelelő foglaláshoz.
        /// </summary>
        private void Loadbasket()
        {
            if (selected != null && selected.Basket != null && selected.Basket.Menu != null && selected.Basket.Menu != null && selected.Basket.Menu.Count > 0)
            {
                BasketContainer.Children.Clear();

                var basketLabel = new Label();

                foreach (var menu in selected.Basket.Menu)
                {
                    basketLabel.Content += $"\nTermék neve: {menu.NameKosar}\nÁr: {menu.Price}ft\n";
                }

                basketLabel.Content += "\n";

                BasketContainer.Children.Add(basketLabel);
            }
            else
            {
                var noBasketLabel = new Label();
                noBasketLabel.Content = "Nincs elérhető kosár az adott foglaláshoz.";
                BasketContainer.Children.Add(noBasketLabel);
            }
        }
        /// <summary>
        /// A rendelésablak bezárása.
        /// </summary>
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
