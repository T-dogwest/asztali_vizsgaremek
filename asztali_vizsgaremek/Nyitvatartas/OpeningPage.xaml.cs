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
    /// Az üzlet nyitvatartását megjelenítő és módosító felület logikáját tartalmazó osztály.
    /// </summary>
    public partial class OpeningPage : Page
    {
        OpeningServices services = new OpeningServices();

        /// <summary>
        /// Az OpeningPage osztály konstruktora.
        /// </summary>
        public OpeningPage()
        {
            InitializeComponent();
            LoadData();
        }
        /// <summary>
        /// Az üzlet nyitvatartási adatainak betöltése és megjelenítése az UI-ban.
        /// </summary>
        private void LoadData()
        {
            var openings = services.GetAll();
            if (openings.Count > 0)
            {
                // Az első elem a lista első eleme lesz
                var opening = openings[0];
                tbMonday.Text = opening.Monday;
                tbTuesday.Text = opening.Tuesday;
                tbWednesday.Text = opening.Wednesday;
                tbThursday.Text = opening.Thursday;
                tbFriday.Text = opening.Friday;
                Sasturday.Text = opening.Sasturday;
                tbSunday.Text = opening.Sunday;
            }
        }
        /// <summary>
        /// Ellenőrzi az időformátum helyességét.
        /// </summary>
        /// <param name="time">Az ellenőrizendő idő</param>
        /// <returns>True, ha az időformátum helyes, egyébként false</returns>
        private bool ValidateTimeFormat(string time)
        {
            if (time.ToLower() == "closed")
            {
                return true;
            }

            string[] parts = time.Split('-');

            if (parts.Length != 2)
            { return false; }

            foreach (string part in parts)
            {
                string[] timeParts = part.Split(':');

                if (timeParts.Length != 2)
                { return false; }

                if (!int.TryParse(timeParts[0], out int hour) || !int.TryParse(timeParts[1], out int minute))
                { return false; }

                if (hour < 0 || hour > 23 || minute < 0 || minute > 59|| timeParts[1].Length !=2)
                {   return false; }
            }

            return true;
        }
        /// <summary>
        /// Az üzlet nyitvatartási adatainak módosítása.
        /// </summary>
        ///  <param name="sender">Az eseményt kiváltó objektum.</param>
        /// <param name="e">Az esemény argumentumai.</param>
        private void Button_Modify(object sender, RoutedEventArgs e)
        {

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
                    Monday = tbMonday.Text.ToLower() == "closed" ? "Closed" : tbMonday.Text,
                    Tuesday = tbTuesday.Text.ToLower() == "closed" ? "Closed" : tbTuesday.Text,
                    Wednesday = tbWednesday.Text.ToLower() == "closed" ? "Closed" : tbWednesday.Text,
                    Thursday = tbThursday.Text.ToLower() == "closed" ? "Closed" : tbThursday.Text,
                    Friday = tbFriday.Text.ToLower() == "closed" ? "Closed" : tbFriday.Text,
                    Sasturday = Sasturday.Text.ToLower() == "closed" ? "Closed" : Sasturday.Text,
                    Sunday = tbSunday.Text.ToLower() == "closed" ? "Closed" : tbSunday.Text,
                };

                OpeningItem selectedOpening = services.GetAll().FirstOrDefault(); // Csak az első elemet módosítjuk

                OpeningItem updatedItem = services.Update(selectedOpening.Id, modifiedOpening);

                if (updatedItem != null)
                {
                    MessageBox.Show("A módosítás sikeres volt!", "Közlés", MessageBoxButton.OK, MessageBoxImage.Information);

                 
                }
                else
                {
                    MessageBox.Show("A módosítás sikertelen volt!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Az időtartamok formátuma helytelen!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
