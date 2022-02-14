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

namespace PL
{
    /// <summary>
    /// The class is responsible for displaying a list of base stations
    /// and managing calls for a specific station display.
    /// </summary>
    public partial class DisplayStationList : Window
    {
        BlApi.IBL Bl;

        /// <summary>
        /// CTOR - gets a IBL object and restarts a view
        /// </summary>
        public DisplayStationList(BlApi.IBL bl)
        {
            InitializeComponent();
            Bl = bl;
            stationDisplay.ItemsSource = Bl.GetStationList();
        }

        /// <summary>
        /// Sends a call to the constructor of a specific station view
        /// </summary>
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BO.StationToList station = (sender as ListView).SelectedValue as BO.StationToList;
            if (station != null)
            {
                new DisplayStation(Bl, Bl.GetToStationByID(station.Id)).Show();
            }
        }

        private void ButtonAddStation_Click(object sender, RoutedEventArgs e)
        {
            new DisplayStation(Bl).Show();
        }


        /// <summary>
        /// Sorted station list view
        /// </summary>
        private void ButtonSortByEmptySlots_Click(object sender, RoutedEventArgs e)
        {
            stationDisplay.ItemsSource = Bl.GetStationListSortedByEmptySlots();
        }

        private void buttonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            stationDisplay.ItemsSource = Bl.GetStationList();
        }
    }
}
