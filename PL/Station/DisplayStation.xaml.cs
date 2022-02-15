using BO;
using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// The class is responsible for displaying a specific base stations -
    /// and display options (add, overview)
    /// </summary>
    public partial class DisplayStation : Window
    {
        BlApi.IBL Bl;
        BO.Station stationBO;
        Station_pl stationPO;

        public DisplayStation(BlApi.IBL bl, Station station)
        {
            stationBO = station;
            stationPO = new Station_pl(station);
            this.Bl = bl;
            InitializeComponent();
            DataContext = stationPO;
            IDTextBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeText.IsEnabled = false;
            UPDATE_STATION.Visibility = dronesInCharge.Visibility = expender.Visibility = Visibility.Visible;
            foreach (DroneInCharge drone in station.DronesInCharging) {dronesInCharge.Items.Add(drone);}
            ADD_BUTTON.Visibility = Visibility.Hidden;
        }
        public DisplayStation(BlApi.IBL bl)
        {
            Bl = bl;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// AddStation by IBL interface calling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDTextBox.Text) || string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(LongitudeTextBox.Text)||string.IsNullOrWhiteSpace(LatitudeText.Text) || string.IsNullOrWhiteSpace(ChargesLotsTextBox.Text))
            {
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddStation(Int32.Parse(IDTextBox.Text), NameTextBox.Text, double.Parse(LongitudeTextBox.Text), double.Parse(LatitudeText.Text), Int32.Parse(ChargesLotsTextBox.Text));
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void showDrone(object sender, RoutedEventArgs e)
        {
            BO.DroneInCharge drone = (sender as ListView).SelectedValue as BO.DroneInCharge;
            if (drone != null)
            {
                new DisplayDrone(Bl, Bl.ConvertDroneInChargeToDrone(drone)).Show();
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.UpDateStationData(Int32.Parse(IDTextBox.Text), NameTextBox.Text,Int32.Parse(ChargesLotsTextBox.Text));
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }


        private void expanderHasExpanded(object sender, RoutedEventArgs args)
        {
            dronesInCharge.Background = Brushes.DimGray;
        }

        /// <summary>
        /// set style of background of dronesInCharge to null,
        /// so it wont hide anything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void expanderHasClose(object sender, RoutedEventArgs args)
        {
            dronesInCharge.Background = null;
        }

        /// <summary>
        /// close window
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
