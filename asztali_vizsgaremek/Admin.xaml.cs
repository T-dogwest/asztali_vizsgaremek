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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Button_Attekintes(object sender, RoutedEventArgs e)
        { 
            Main.Content = new Attekintes();
        }

        private void Button_Etelek(object sender, RoutedEventArgs e)
        {
            Main.Content = new Etelek();
        }

        private void Button_Italok(object sender, RoutedEventArgs e)
        {
            Main.Content = new Italok();
        }

        private void Button_Felhasznalok(object sender, RoutedEventArgs e)
        {
            Main.Content = new Felhasznalok();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Adminok();
        }
        
    }
}
