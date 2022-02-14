﻿using BO;
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
        Parcel_pl ParcelInDrone = null;
        int maxWeight = 1;
        public DisplayDrone(BlApi.IBL BL,Drone drone)
        {
            droneBO = drone;
            dronePO = new Drone_pl(BL,drone);
            this.BL = BL;
            InitializeComponent();
            DataContext = dronePO;
            /*light.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.light ? true : false;
            medium.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.medium ? true : false;
            heavy.IsChecked = drone.MaxWeight == BO.Enum.WeightCategoriesBL.heavy ? true : false;*/
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

        private void DELETE_Button_Click(object sender, RoutedEventArgs e)
        {
            BL.DeleteDrone(int.Parse(IDTextBox.Text));
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

        private void Button_Click_1(object sender, RoutedEventArgs e) { if(worker.IsBusy) stop_simulationButton_Click(); this.Close(); }


        BackgroundWorker worker = new BackgroundWorker();
        private void simulationButton_Click(object sender, RoutedEventArgs e)
        {
            Simulation.Visibility = Visibility.Hidden;
            Simulation.IsEnabled = false;
            StopSimulation.Visibility = Visibility.Visible;
            StopSimulation.IsEnabled = true;
            bool t = true;
            Drone updateDrone = null;
            Parcel updateParcel = null;
            worker.DoWork += (object? sender, DoWorkEventArgs e) =>
            {
                 BL.StartSimulation(
                   BL,
                   droneBO.Id,
                   (droneBO) => { updateDrone = droneBO; worker.ReportProgress(1); },
/*                   (parcelBO,t) => { updateParcel = parcelBO; worker.ReportProgress(0); },
*/                   () => worker.CancellationPending);
            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) =>
            {
                dronePO.UpdatePlDrone(droneBO);
                BL.ActionUpdateList(true);
                if(droneBO.delivery != null)
                {
                    Parcel ParcelInDrone = BL.GetParcel(droneBO.delivery.Id);
                  //  BL.ActionParcelChanged(ParcelInDrone, true);
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