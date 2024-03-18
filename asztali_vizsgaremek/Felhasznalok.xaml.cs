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

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Interaction logic for Felhasznalok.xaml
    /// </summary>
    public partial class Felhasznalok : Page
    {
        FelhasznaloService services = new FelhasznaloService();
        public Felhasznalok()
        {
            InitializeComponent();
            LoadData();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {

        }
        private async void LoadData()
        {
            List<FelhasznmalokItem> felhasznalok = services.GetAll();
            List<FelhasznmalokItem> filteredFelhasznalok = felhasznalok.Select(item =>
                new FelhasznmalokItem
                {
                    First_name = item.First_name,
                    Last_name = item.Last_name,
                    Email = item.Email,
                    roleType = item.roleType,
                }).ToList();

            UserTable.ItemsSource = filteredFelhasznalok;
        }
    }
}
