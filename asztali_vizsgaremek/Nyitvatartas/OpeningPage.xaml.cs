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

namespace asztali_vizsgaremek.Nyitvatartas
{
    /// <summary>
    /// Interaction logic for Opening.xaml
    /// </summary>
    public partial class OpeningPage : Page
    {
        OpeningServices services = new OpeningServices();
        public OpeningPage()
        {
            InitializeComponent();
            openinTable.ItemsSource = services.GetAll();
        }
        private bool ValidateTimeFormat(string time)
        {
            if (time.ToLower() == "closed")
            {
                return true;
            }

            string[] parts = time.Split('-');

            if (parts.Length != 2)
                return false;

            foreach (string part in parts)
            {
                string[] timeParts = part.Split(':');

                if (timeParts.Length != 2)
                    return false;

                if (!int.TryParse(timeParts[0], out int hour) || !int.TryParse(timeParts[1], out int minute))
                    return false;

                if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                    return false;
            }

            return true;
        }

        private void openinTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (openinTable.SelectedItem != null)
            {
                OpeningItem selectedOpening = (OpeningItem)openinTable.SelectedItem;
                // Betöltjük az adatokat az input mezőkbe
                tbMonday.Text = selectedOpening.Monday;
                tbTuesday.Text = selectedOpening.Tuesday;
                tbWednesday.Text = selectedOpening.Wednesday;
                tbThursday.Text = selectedOpening.Thursday;
                tbFriday.Text = selectedOpening.Friday;
                Sasturday.Text = selectedOpening.Sasturday;
                tbSunday.Text = selectedOpening.Sunday;
            }
        }


        private void Button_Modify(object sender, RoutedEventArgs e)
        {
            if (openinTable.SelectedItem != null)
            {
                OpeningItem selectedOpening = (OpeningItem)openinTable.SelectedItem;

                if (ValidateTimeFormat(tbMonday.Text) &&
                    ValidateTimeFormat(tbTuesday.Text) &&
                    ValidateTimeFormat(tbWednesday.Text) &&
                    ValidateTimeFormat(tbThursday.Text) &&
                    ValidateTimeFormat(tbFriday.Text) &&
                    ValidateTimeFormat(Sasturday.Text) &&
                    ValidateTimeFormat(tbSunday.Text))
                {
                    
                    OpeningDTO modifiedOpening = new OpeningDTO
                    {
                        Monday = tbMonday.Text,
                        Tuesday = tbTuesday.Text,
                        Wednesday = tbWednesday.Text,
                        Thursday = tbThursday.Text,
                        Friday = tbFriday.Text,
                        Sasturday = Sasturday.Text,
                        Sunday = tbSunday.Text,
                    };

                   
                    if (tbMonday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Monday = "Closed"; 
                    }
                    if (tbTuesday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Tuesday = "Closed";
                    }
                    if (tbWednesday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Wednesday = "Closed";
                    }
                    if (tbThursday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Thursday = "Closed";
                    }
                    if (tbFriday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Friday = "Closed";
                    }
                    if (Sasturday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Sasturday = "Closed";
                    }
                    if (tbSunday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Sunday = "Closed";
                    }

                    
                    OpeningItem updatedItem = services.Update(selectedOpening.Id, modifiedOpening);

                    if (updatedItem != null)
                    {
                        MessageBox.Show("A módosítás sikeres volt!");
              
                        openinTable.ItemsSource = services.GetAll();

                      
                        tbMonday.Text = "";
                        tbTuesday.Text = "";
                        tbWednesday.Text = "";
                        tbThursday.Text = "";
                        tbFriday.Text = "";
                        Sasturday.Text = "";
                        tbSunday.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("A módosítás sikertelen volt!");
                    }
                }
                else
                {
                    MessageBox.Show("Az időtartamok formátuma helytelen!");
                }
            }
        }

    }
}
