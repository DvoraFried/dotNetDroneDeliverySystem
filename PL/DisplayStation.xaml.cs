using BO;
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
    /// Interaction logic for DisplayStation.xaml
    /// </summary>
    public partial class DisplayStation : Window
    {
        public DisplayStation()
        {
            InitializeComponent();
        }
        BlApi.IBL Bl;
        public DisplayStation(BlApi.IBL bl, StationBL station)
        {
            Bl = bl;
            InitializeComponent();
            IDTextBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeText.IsEnabled = false;
            IDTextBox.Text = station.GetIdBL().ToString();
            NameTextBox.Text = station.NameBL;
            expender.Visibility = Visibility.Visible;
            ChargesLotsTextBox.Text = station.ChargeSlotsBL.ToString();
            LongitudeTextBox.Text = station.Position.Longitude.ToString();
            LatitudeText.Text = station.Position.Latitude.ToString();
            foreach (DroneInChargeBL drone in station.DronesInCharging)
            {
                dronesInCharge.Items.Add(drone);
            }
            ADD_BUTTON.Visibility = Visibility.Hidden;
            UPDATE_STATION.Visibility = dronesInCharge.Visibility = Visibility.Visible;
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
                catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void showDrone(object sender, RoutedEventArgs e)
        {
            BO.DroneInChargeBL drone = (sender as ListView).SelectedValue as BO.DroneInChargeBL;
            this.Close();
            new DisplayDrone(Bl, Bl.convertDroneInChargeBLToDroneBl(drone) ).ShowDialog();
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.UpDateStationData(Int32.Parse(IDTextBox.Text), NameTextBox.Text,Int32.Parse(ChargesLotsTextBox.Text));
                this.Close();
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void expanderHasExpanded(object sender, RoutedEventArgs args)
        {
            dronesInCharge.Background = Brushes.DimGray;
        }
        private void expanderHasClose(object sender, RoutedEventArgs args)
        {
            dronesInCharge.Background = null;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
