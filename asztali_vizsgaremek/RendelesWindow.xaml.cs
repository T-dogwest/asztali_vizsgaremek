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
    /// Interaction logic for RendelesWindow.xaml
    /// </summary>
    public partial class RendelesWindow : Window
    {
        AttekintesItem selected ;
        
        public RendelesWindow(AttekintesItem selectedItem)
        {
            
            InitializeComponent();
            selected = selectedItem;
           
            Loadbasket();

           
        }

        private void Loadbasket()
        {
            if (selected != null && selected.UserData != null && selected.UserData.Basket != null)
            {
                // Clear the existing content of BasketContainer
                BasketContainer.Children.Clear();

                // Find the basket related to the selected reservation
                var selectedBasket = selected.UserData.Basket.FirstOrDefault(basket => basket.Id == selected.Id);

                if (selectedBasket != null)
                {
                    var basketLabel = new Label();
                 

                    // Add menu items to the label
                    foreach (var menu in selectedBasket.Menu)
                    {
                        basketLabel.Content += $"\nMenu Name: {menu.NameKosar}, Price: {menu.Price}";
                    }

                    // Add some spacing
                    basketLabel.Content += "\n";

                    // Add the label to BasketContainer
                    BasketContainer.Children.Add(basketLabel);
                }
                else
                {
                    // Ha a kiválasztott foglaláshoz nem tartozik kosár, jeleníts meg egy üzenetet
                    var noBasketLabel = new Label();
                    noBasketLabel.Content = "Nincs elérhető kosár az adott foglaláshoz.";
                    BasketContainer.Children.Add(noBasketLabel);
                }
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
