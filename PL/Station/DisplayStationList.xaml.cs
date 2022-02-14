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
    /// Interaction logic for DisplayStationList.xaml
    /// </summary>
    public partial class DisplayStationList : Window
    {
        BlApi.IBL Bl;
        public DisplayStationList(BlApi.IBL bl)
        {
            InitializeComponent();
            Bl = bl;
            stationDisplay.ItemsSource = Bl.GetStationList();
        }
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
            this.Close();
        }

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
