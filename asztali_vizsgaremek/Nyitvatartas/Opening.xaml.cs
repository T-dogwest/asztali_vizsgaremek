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
    public partial class Opening : Page
    {
        OpeningServices services = new OpeningServices();
        public Opening()
        {
            InitializeComponent();
            openinTable.ItemsSource = services.GetAll();
        }
        private bool ValidateTimeFormat(string time)
        {
            if (time.ToLower() == "closed")
            {
                return true; // Ha a "closed" szót kapjuk, akkor elfogadjuk az inputot
            }

            // Az időtartam formátuma: "hh:mm-hh:mm"
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
                    // Az input mezőkből át kell másolni az adatokat egy DTO objektumba
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

                    // Ellenőrizzük, hogy az adott nap zárva van-e
                    if (tbMonday.Text.ToLower() == "closed")
                    {
                        modifiedOpening.Monday = "Closed"; // Ha zárva van, az időintervallum helyett az "Closed" szöveget használjuk
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

                    // Majd meghívjuk az Update metódust a kiválasztott elem azonosítójával és a módosított adatokkal
                    OpeningItem updatedItem = services.Update(selectedOpening.Id, modifiedOpening);

                    if (updatedItem != null)
                    {
                        MessageBox.Show("A módosítás sikeres volt!");
                        // Frissítjük a táblát
                        openinTable.ItemsSource = services.GetAll();

                        // Töröljük az input mezők tartalmát
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
