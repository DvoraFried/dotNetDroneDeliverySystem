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
        public DisplayDrone(IBL.IBL bl,DroneBL drone)
        {
            Bl = bl;
            InitializeComponent();
            ADD_BUTTON.Visibility = Visibility.Hidden;
            UPDATE_MENU.Visibility = Visibility.Visible;
            IDTextBox.Text = drone.getIdBL().ToString();
            IDTextBox.IsEnabled = false;
            ModelTextBox.Text = drone.ModelBL;
            light.IsChecked = drone.MaxWeight == EnumBL.WeightCategoriesBL.light ? true : false;
            light.IsEnabled = false;
            medium.IsChecked = drone.MaxWeight == EnumBL.WeightCategoriesBL.medium ? true : false;
            medium.IsEnabled = false;
            heavy.IsChecked = drone.MaxWeight == EnumBL.WeightCategoriesBL.heavy ? true : false;
            heavy.IsEnabled = false;
            batteryStatus.Value = drone.BatteryStatus;
            DroneStatusTextBox.Text = drone.DroneStatus.ToString();
            statioIdLabel.Visibility = Visibility.Hidden;
            StationIdTextBox.Visibility = Visibility.Hidden;
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

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (0==0)
            {
                IDTextBox.Foreground = Brushes.Red;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDTextBox.Text) || string.IsNullOrWhiteSpace(ModelTextBox.Text) || string.IsNullOrWhiteSpace(StationIdTextBox.Text)){
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                Bl.AddDrone(Int32.Parse(IDTextBox.Text), ModelTextBox.Text, (EnumBL.WeightCategoriesBL)maxWeight, Int32.Parse(StationIdTextBox.Text));
            }
        }
        private void UpdateModelClick()
        {
            if (ModelTextBox.Text == IDTextBox.Text) { MessageBox.Show("No value updated"); }
            else { Bl.UpDateDroneName(Int32.Parse(IDTextBox.Text), ModelTextBox.Text); }
        }
        private void SendDroneToCharge()
        {
            Bl.SendDroneToCharge(Int32.Parse(IDTextBox.Text));
        }
        private void ReleaseDroneFromChargingClick()
        {
            MessageBox.Show("Enter Time In Charging:");
        }
        public void AssigningPackageToDroneClick()
        {
            Bl.AssigningPackageToDrone(Int32.Parse(IDTextBox.Text));
        }
        public void CollectionOfAParcelByDroneClick()
        {
            Bl.CollectionOfAParcelByDrone(Int32.Parse(IDTextBox.Text));
        }
        public void DeliveryAParcelByDroneClick()
        {
            Bl.DeliveryOfAParcelByDrone(Int32.Parse(IDTextBox.Text));

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

        private void StationIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AssigningPackageToDrone(object sender, RoutedEventArgs e)
        {

        }
    }
}
