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
    /// Interaction logic for DisplayParcel.xaml
    /// </summary>
    public partial class DisplayParcel : Window
    {
        IBL.IBL Bl;
        int weight = 0;
        int priority = 0;
        public DisplayParcel(IBL.IBL bl, ParcelBL parcel)
        {
            InitializeComponent();
            ADD_BUTTON.Visibility = PRIORITYlabel.Visibility = weightLabel.Visibility = senderIdlbel.Visibility = targetIdLabel.Visibility = priorityCheckBox.Visibility = WeightCheckBox.Visibility = IDSenderTextBox.Visibility = TargetIDTextBox.Visibility = Visibility.Hidden;
            displayParcel.Visibility = Visibility.Visible;
            senderTextBox.Text = parcel.Sender.ToString();
            targetTextBox.Text = parcel.Target.ToString();
            IDSenderTextBox.IsEnabled = TargetIDTextBox.IsEnabled = false;
            ParcelInDroneTextBox.Text = parcel.DroneIdBL != null ? parcel.DroneIdBL.ToString() : "non drone assign yet";
            RequestedTimeTextBox.Text = parcel.RequestedBL.ToString();
            AssignToDroneTimeTextBox.Text = parcel.ScheduledBL != null ? parcel.ScheduledBL.ToString() : "deos not assign yet";
            PickupTimeTextBox.Text = parcel.PickUpBL != null ? parcel.PickUpBL.ToString() : "deos not pickUp yet";
            DeliveredTimeTextBox.Text = parcel.DeliveredBL != null ? parcel.DeliveredBL.ToString() : "deos not delivered yet";
            PriorityTextBox.Text = parcel.Priority.ToString();
            WeightTextBox.Text = parcel.Weight.ToString();
        }
        public DisplayParcel(IBL.IBL bl)
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
            if (string.IsNullOrWhiteSpace(IDSenderTextBox.Text) || string.IsNullOrWhiteSpace(TargetIDTextBox.Text))
            {
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddParcel(Int32.Parse(IDSenderTextBox.Text), Int32.Parse(TargetIDTextBox.Text), (EnumBL.WeightCategoriesBL)weight, (EnumBL.PrioritiesBL)priority);
                    this.Close();
                }
                catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
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
    }
}
