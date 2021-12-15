using IBL.BO;
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
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Window
    {
        IBL.IBL Bl;

        int maxWeight = 1;
        public DisplayDrone(IBL.IBL bl, DroneBL drone)
        {
            Bl = bl;
            InitializeComponent();
            ADD_BUTTON.Visibility = Visibility.Hidden;
            UPDATE_MENU.Visibility = Visibility.Visible;
            IDTextBox.Text = drone.getIdBL().ToString();
            IDTextBox.IsEnabled = false;
            ModelTextBox.Text = drone.ModelBL;
            light.IsChecked = drone.MaxWeight == EnumBL.WeightCategoriesBL.light ? true : false;
            medium.IsChecked = drone.MaxWeight == EnumBL.WeightCategoriesBL.medium ? true : false;
            heavy.IsChecked = drone.MaxWeight == EnumBL.WeightCategoriesBL.heavy ? true : false;
            light.IsEnabled = medium.IsEnabled = heavy.IsEnabled = false;
            batteryStatus.Value = drone.BatteryStatus;
            DroneStatusTextBox.Text = drone.DroneStatus.ToString();
            statioIdLabel.Visibility = StationIdTextBox.Visibility = Visibility.Hidden;
        }
        public DisplayDrone(IBL.IBL bl)
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
            if (string.IsNullOrWhiteSpace(IDTextBox.Text) || string.IsNullOrWhiteSpace(ModelTextBox.Text) || string.IsNullOrWhiteSpace(StationIdTextBox.Text))
            {
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddDrone(Int32.Parse(IDTextBox.Text), ModelTextBox.Text, (EnumBL.WeightCategoriesBL)maxWeight, Int32.Parse(StationIdTextBox.Text));
                    this.Close();
                }
                catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                /*                new DroneList(Bl).Show();
                                Close();*/
            }
        }
        private void UpdateModelClick(object sender, RoutedEventArgs e)
        {
            Bl.UpDateDroneName(Int32.Parse(IDTextBox.Text), ModelTextBox.Text);
            this.Close();
        }
        private void SendDroneToChargeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.SendDroneToCharge(Int32.Parse(IDTextBox.Text));
                this.Close();
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void ReleaseDroneFromChargingClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double hour = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("enter time in charge"));
                Bl.ReleaseDroneFromCharging(Int32.Parse(IDTextBox.Text), hour);
                this.Close();
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
                this.Close();
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
                this.Close();
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
                this.Close();
            }
            catch (FormatException) { MessageBox.Show("data reciving error ~", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error ~", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
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
    }
}
