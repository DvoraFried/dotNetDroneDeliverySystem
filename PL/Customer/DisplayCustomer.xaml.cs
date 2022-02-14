using BO;
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
    /// Interaction logic for DisplayCustomer.xaml
    /// </summary>
    public partial class DisplayCustomer : Window
    {
        BlApi.IBL Bl;
        Customer_pl CustomerPO;

        /// <summary>
        /// CTOR to open a specific cusomer data 
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customer"></param>

        public DisplayCustomer(BlApi.IBL bl, Customer customer)
        {
            Bl = bl;

            CustomerPO = new Customer_pl(Bl, customer);
            InitializeComponent();
            DataContext = CustomerPO;

            exIsent.Visibility = exSENDme.Visibility = DELETE_BUTTON.Visibility = UPDATE_BUTTON.Visibility = Visibility.Visible;
            ADD_BUTTON.Visibility = Visibility.Hidden;
            IDTebtBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeTextBox.IsEnabled = false;

            foreach (DeliveryAtCustomer parcel in customer.ImTheSender) { Isent.Items.Add(parcel);}
            foreach (DeliveryAtCustomer parcel in customer.ImTheTarget) { SENDme.Items.Add(parcel); }
            LongitudeTextBox.Text = customer.Position.Longitude.ToString();
            LatitudeTextBox.Text = customer.Position.Latitude.ToString();
        }
        private void showParcel(object sender, RoutedEventArgs e)
        {
            BO.DeliveryAtCustomer parcel = (sender as ListView).SelectedValue as BO.DeliveryAtCustomer;
            if (parcel != null)
            {
                new DisplayParcel(Bl, Bl.GetParcel(parcel.Id)).Show();
            }
        }
        public DisplayCustomer(BlApi.IBL bl)
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
            if (string.IsNullOrWhiteSpace(IDTebtBox.Text) || string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(PhoneTextBox.Text) || string.IsNullOrWhiteSpace(LongitudeTextBox.Text)|| string.IsNullOrWhiteSpace(LatitudeTextBox.Text))
            {
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddCustomer(Int32.Parse(IDTebtBox.Text), NameTextBox.Text, PhoneTextBox.Text,double.Parse(LongitudeTextBox.Text), double.Parse(LatitudeTextBox.Text));
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
                Bl.UpDateCustomerData(Int32.Parse(IDTebtBox.Text), NameTextBox.Text, PhoneTextBox.Text);
                this.Close();
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.RemoveCustomerById(Int32.Parse(IDTebtBox.Text));
                this.Close();
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
