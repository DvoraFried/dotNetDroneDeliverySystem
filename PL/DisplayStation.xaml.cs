﻿using IBL.BO;
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
        IBL.IBL Bl;
        public DisplayStation(IBL.IBL bl, StationBL station)
        {
            Bl = bl;
            InitializeComponent();
            IDTextBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeText.IsEnabled = false;
            IDTextBox.Text = station.GetIdBL().ToString();
            NameTextBox.Text = station.NameBL;
            ChargesLotsTextBox.Text = station.ChargeSlotsBL.ToString();
            LongitudeTextBox.Text = station.Position.Longitude.ToString();
            LatitudeText.Text = station.Position.Latitude.ToString();
            dronesInCharge.Content = returnList(station.DronesInCharging);
            ADD_BUTTON.Visibility = Visibility.Hidden;
            UPDATE_STATION.Visibility = dronesInCharge.Visibility = Visibility.Visible;
        }
        private string returnList(List<DroneInChargeBL> drones)
        {
            if(drones.Count == 0) { return "No drones in charge"; }
            string myString = "";
            foreach(DroneInChargeBL drone in drones) { myString += drone.ToString(); }
            return myString;
        }
        public DisplayStation(IBL.IBL bl)
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
    }
}