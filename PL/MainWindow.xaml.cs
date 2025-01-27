﻿using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /*<Button Content =" drones list" Background="AliceBlue"
				Click="Button_Click" Margin="322,178,322,178"/>*/
    public partial class MainWindow : Window
    {
        BlApi.IBL Bl;
        public MainWindow(BlApi.IBL bl, EmpolyeeBL employee)
        {
            Bl = bl;
            InitializeComponent();
        }

        private void droneList_Click(object sender, RoutedEventArgs e)
        {
            new DroneList(Bl).Show();
        }

        private void parcelList_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(Bl).Show();
        }

        private void customerList_Click(object sender, RoutedEventArgs e)
        {
            new DisplayCustomerList(Bl).Show();
        }
        
        private void stationList_Click(object sender, RoutedEventArgs e)
        {
            new DisplayStationList(Bl).Show();
        }
    }
}
