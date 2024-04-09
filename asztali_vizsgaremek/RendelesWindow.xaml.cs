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
                
                BasketContainer.Children.Clear();

           
                var selectedBasket = selected.UserData.Basket.FirstOrDefault(basket => basket.Id == selected.Id);

                if (selectedBasket != null)
                {
                    var basketLabel = new Label();
                 

                  
                    foreach (var menu in selectedBasket.Menu)
                    {
                        basketLabel.Content += $"\nMenu Name: {menu.NameKosar}, Price: {menu.Price}";
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
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
