﻿using asztali_vizsgaremek.Velemenyekk;
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
    /// Interaction logic for Velemenyek.xaml
    /// </summary>
    public partial class Velemenyek : Page
    {
        VelemenyekServices service=new VelemenyekServices();
        public Velemenyek()
        {
            InitializeComponent();
           // VelemenyTable.ItemsSource = service.GetAll();
        }
    }
}
