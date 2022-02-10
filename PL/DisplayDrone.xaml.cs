using BO;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        BlApi.IBL BL;
        drone_pl dronePO;
        Drone droneBO;
        int maxWeight = 1;
        public DisplayDrone(BlApi.IBL BL,Drone drone)
        {
            droneBO = drone;
            dronePO = new drone_pl(BL,drone);
            this.BL = BL;
            InitializeComponent();
            DataContext = dronePO;
            light.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.light ? true : false;
            medium.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.medium ? true : false;
            heavy.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.heavy ? true : false;
            light.IsEnabled = medium.IsEnabled = heavy.IsEnabled = false;
            UPDATE_MENU.Visibility = hidddenInfroUpDate.Visibility = Visibility.Visible;
            ADD_BUTTON.Visibility = statioIdLabel.Visibility = StationIdTextBox.Visibility = Visibility.Hidden;
        }
        public DisplayDrone(BlApi.IBL bl)
        {
            BL = bl;
            InitializeComponent();
        }
        private void showParcel(object sender, RoutedEventArgs e)
        {
            if (dronePO.Delivery.Id != 0)
            {
                new DisplayParcel(BL, BL.returnParcel(dronePO.Delivery.Id)).ShowDialog();
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
                    BL.AddDrone(int.Parse(IDTextBox.Text), ModelTextBox.Text, (BO.Enum.WeightCategoriesBL)maxWeight, int.Parse(StationIdTextBox.Text));
                    this.Close();
                }
                catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void UpdateModelClick(object sender, RoutedEventArgs e)
        {
            BL.UpDateDroneName(Int32.Parse(IDTextBox.Text), ModelTextBox.Text);

        }
        private void SendDroneToChargeClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.SendDroneToCharge(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void ReleaseDroneFromChargingClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.ReleaseDroneFromCharging(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public void AssigningPackageToDroneClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.AssigningPackageToDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        
        public void CollectionOfAParcelByDroneClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.CollectionOfAParcelByDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
        public void DeliveryAParcelByDroneClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.DeliveryOfAParcelByDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error ~", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error ~", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        
        private void light_Checked(object sender, RoutedEventArgs e) { maxWeight = 0; }

        private void medium_Checked(object sender, RoutedEventArgs e) { maxWeight = 1; }

        private void heavy_Checked(object sender, RoutedEventArgs e) { maxWeight = 2; }

        private void Button_Click_1(object sender, RoutedEventArgs e) { this.Close(); }


        BackgroundWorker worker = new BackgroundWorker();
        private void simulationButton_Click(object sender, RoutedEventArgs e)
        {
            Drone updateDrone = null;
            worker.DoWork += (object? sender, DoWorkEventArgs e) =>
            {
                 BL.StartSimulation(
                   BL,
                   droneBO.getIdBL(),
                   (droneBO) => { updateDrone = droneBO; worker.ReportProgress(0); },
                   () => worker.CancellationPending);
            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) =>
            {
                dronePO.UpdatePlDrone(droneBO);
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }
    }
}
