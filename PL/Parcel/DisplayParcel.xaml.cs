﻿using BO;
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
    /// Interaction logic for DisplayParcel.xaml
    /// </summary>
    public partial class DisplayParcel : Window
    {
        public static BlApi.IBL Bl;
        int weight = 0;
        int priority = 0;
        bool isClient = false;
        Parcel currentParcel;
        Parcel_pl parcelPO;
        public DisplayParcel(BlApi.IBL bl, Parcel parcel, bool client = false)
        {
            isClient = client;
            parcelPO = new Parcel_pl(bl,parcel);
            Bl = bl;
            DataContext = parcelPO;
            InitializeComponent();
            ADD_BUTTON.Visibility = PRIORITYlabel.Visibility = weightLabel.Visibility = senderIdlbel.Visibility = targetIdLabel.Visibility = priorityCheckBox.Visibility = WeightCheckBox.Visibility = IDSenderTextBox.Visibility = TargetIDTextBox.Visibility = Visibility.Hidden;
            displayParcel.Visibility = DELETE_BUTTON.Visibility = Visibility.Visible;
            senderTextBox.Items.Add(parcel.Sender); 
            targetTextBox.Items.Add(parcel.Target); 
            ParcelInDroneTextBox.Text = parcel.DroneIdBL != null ? parcel.DroneIdBL.ToString() : "non drone assign yet";
            currentParcel = parcel;
        }
        public DisplayParcel(BlApi.IBL bl, Customer customer)
        {
            Bl = bl;
            InitializeComponent();
            IDSenderTextBox.Text = customer.Id.ToString();
            IDSenderTextBox.IsEnabled = false;
        }
        public DisplayParcel(BlApi.IBL bl)
        {
            Bl = bl;
            InitializeComponent();
        }
        /// <summary>
        /// the function open a customer window with the customer that is related to the parcl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showCustomer(object sender, RoutedEventArgs e)
        {
            BO.CustomerOnDelivery customer = (sender as ListView).SelectedValue as BO.CustomerOnDelivery;
            if (customer != null && !isClient)
            {
                new DisplayCustomer(Bl, Bl.GetCustomerByID(customer.Id)).ShowDialog();
            }
        }
        ///
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// the function add parcel to drone list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDSenderTextBox.Text) || string.IsNullOrWhiteSpace(TargetIDTextBox.Text))
            {
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddParcel(int.Parse(IDSenderTextBox.Text), int.Parse(TargetIDTextBox.Text), (BO.Enum.WeightCategoriesBL)weight, (BO.Enum.PrioritiesBL)priority);
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        /// <summary>
        /// function delete the customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.DeleteParcel(currentParcel);
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void light_Checked(object sender, RoutedEventArgs e)
        {
            weight = 0;
        }

        private void medium_Checked(object sender, RoutedEventArgs e)
        {
            weight = 1;
        }

        private void heavy_Checked(object sender, RoutedEventArgs e)
        {
            weight = 2;
        }

        private void usual_Checked(object sender, RoutedEventArgs e)
        {
            priority = 0;
        }

        private void rapid_Checked(object sender, RoutedEventArgs e)
        {
            priority = 1;
        }

        private void emergency_Checked(object sender, RoutedEventArgs e)
        {
            priority = 2;
        }
        /// <summary>
        /// function closes the window. dah!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
