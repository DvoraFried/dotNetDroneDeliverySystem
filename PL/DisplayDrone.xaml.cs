using BO;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Window
    {
        BlApi.IBL Bl;
        Drone_pl dronePO;
        int maxWeight = 1;
        public DisplayDrone(BlApi.IBL bl,Drone drone)
        {
            dronePO = new Drone_pl(bl,drone);
            Bl = bl;
            InitializeComponent();
            DataContext = dronePO;
            //parcelInDrone.Items.Add(dronePO.Delivery == null ? "No Parcel in Drone" : dronePO.Delivery);
            light.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.light ? true : false;
            medium.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.medium ? true : false;
            heavy.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.heavy ? true : false;
            light.IsEnabled = medium.IsEnabled = heavy.IsEnabled = false;
            //batteryStatus.Value = drone.BatteryStatus;
            UPDATE_MENU.Visibility = hidddenInfroUpDate.Visibility = Visibility.Visible;
            ADD_BUTTON.Visibility = statioIdLabel.Visibility = StationIdTextBox.Visibility = Visibility.Hidden;
        }
        public DisplayDrone(BlApi.IBL bl)
        {
            Bl = bl;
            InitializeComponent();
        }
        private void showParcel(object sender, RoutedEventArgs e)
        {
            BO.ParcelByTransfer parcel = (sender as ListView).SelectedValue as BO.ParcelByTransfer;
            if (parcel != null)
            {
                this.Close();
                new DisplayParcel(Bl, Bl.convertParcelToParcelBl(parcel.Id)).ShowDialog();
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDTextBox.Text) || string.IsNullOrWhiteSpace(ModelTextBox.Text) || string.IsNullOrWhiteSpace(StationIdTextBox.Text)){
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddDrone(int.Parse(IDTextBox.Text), ModelTextBox.Text, (BO.Enum.WeightCategoriesBL)maxWeight, int.Parse(StationIdTextBox.Text));
                    this.Close();
                }
                catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void UpdateModelClick(object sender, RoutedEventArgs e)
        {
            Bl.UpDateDroneName(Int32.Parse(IDTextBox.Text), ModelTextBox.Text);

        }
        private void SendDroneToChargeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.SendDroneToCharge(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void ReleaseDroneFromChargingClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.ReleaseDroneFromCharging(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public void AssigningPackageToDroneClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.AssigningPackageToDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        
        public void CollectionOfAParcelByDroneClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.CollectionOfAParcelByDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
        public void DeliveryAParcelByDroneClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.DeliveryOfAParcelByDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error ~", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error ~", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void expanderHasExpanded(object sender, RoutedEventArgs args)
        {
            parcelInDrone.Background = Brushes.DimGray;
        }
        private void expanderHasClose(object sender, RoutedEventArgs args)
        {
            parcelInDrone.Background = null;
        }
        
        private void light_Checked(object sender, RoutedEventArgs e)
        {
            maxWeight = 0;
        }

        private void medium_Checked(object sender, RoutedEventArgs e)
        {
            maxWeight = 1;
        }

        private void heavy_Checked(object sender, RoutedEventArgs e)
        {
            maxWeight = 2;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
