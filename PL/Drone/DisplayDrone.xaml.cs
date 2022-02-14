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
        Drone_pl dronePO;
        Drone droneBO;
        int maxWeight = 1;
        public DisplayDrone(BlApi.IBL BL,Drone drone)
        {
            droneBO = drone;
            dronePO = new Drone_pl(BL,drone);
            this.BL = BL;
            InitializeComponent();
            DataContext = dronePO;
            light.IsEnabled = medium.IsEnabled = heavy.IsEnabled = false;
            UPDATE_MENU.Visibility = hidddenInfroUpDate.Visibility = DELETE_BUTTON.Visibility = Simulation.Visibility = Visibility.Visible;
            ADD_BUTTON.Visibility = statioIdLabel.Visibility = StationIdTextBox.Visibility = Visibility.Hidden;
        }
        public DisplayDrone(BlApi.IBL bl)
        {
            BL = bl;
            InitializeComponent();
            hidddenInfroUpDate.Visibility = Visibility.Hidden;
            IDTextBox.IsEnabled = true;
        }
        private void showParcel(object sender, RoutedEventArgs e)
        {
            if (dronePO.Delivery.Id != 0)
            {
                new DisplayParcel(BL, BL.GetParcel(dronePO.Delivery.Id)).ShowDialog();
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// the function adds drone to dal list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        /// <summary>
        /// function delet drone drom dal list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DELETE_Button_Click(object sender, RoutedEventArgs e)
        {
            BL.DeleteDrone(int.Parse(IDTextBox.Text));
        }
        /// <summary>
        /// the function update drone in dal list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateModelClick(object sender, RoutedEventArgs e)
        {
            BL.UpDateDroneName(Int32.Parse(IDTextBox.Text), ModelTextBox.Text);

        }
        /// <summary>
        /// the function send drone to charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendDroneToChargeClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.SendDroneToCharge(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        /// <summary>
        /// the function release drone frome charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseDroneFromChargingClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.ReleaseDroneFromCharging(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        /// <summary>
        /// the function assin packeg to drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AssigningPackageToDroneClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.AssigningPackageToDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        /// <summary>
        /// the function collet parcel with the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CollectionOfAParcelByDroneClick(object sender, RoutedEventArgs e)
        {
            try {
                BL.CollectionOfAParcelByDrone(Int32.Parse(IDTextBox.Text));
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
        /// <summary>
        /// the tunction send drone to deliver a package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void Button_Click_1(object sender, RoutedEventArgs e) { if(worker.IsBusy) stop_simulationButton_Click(); this.Close(); }


        BackgroundWorker worker = new BackgroundWorker();
        private void simulationButton_Click(object sender, RoutedEventArgs e)
        {
            Simulation.Visibility = Visibility.Hidden;
            Simulation.IsEnabled = false;
            StopSimulation.Visibility = Visibility.Visible;
            StopSimulation.IsEnabled = true;
            Drone updateDrone = null;
            worker.DoWork += (object? sender, DoWorkEventArgs e) =>
            {
                 BL.StartSimulation(
                   BL,
                   droneBO.Id,
                   (droneBO) => { updateDrone = droneBO; worker.ReportProgress(1); },
                   () => worker.CancellationPending);
            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) =>
            {
                dronePO.UpdatePlDrone(droneBO);
                BL.ActionUpdateList(true);
                if(droneBO.delivery != null)
                {
                    Parcel ParcelInDrone = BL.GetParcel(droneBO.delivery.Id);
                }
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }
        private void stop_simulationButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            worker.CancelAsync();
            Simulation.Visibility = Visibility.Visible;
            Simulation.IsEnabled = true;
            StopSimulation.Visibility = Visibility.Hidden;
            StopSimulation.IsEnabled = false;
        }
    }
}
