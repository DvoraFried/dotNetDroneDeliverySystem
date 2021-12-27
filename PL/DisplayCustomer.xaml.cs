using BO;
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
        public DisplayCustomer(BlApi.IBL bl, CustomerBL customer)
        {
            Bl = bl;
            InitializeComponent();
            exIsent.Visibility = exSENDme.Visibility = Visibility.Visible;
            foreach(DeliveryAtCustomer parcel in customer.ImTheSender) { Isent.Items.Add(parcel);}
            foreach (DeliveryAtCustomer parcel in customer.ImTheTarget) { Isent.Items.Add(parcel); }
            IDTebtBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeTextBox.IsEnabled = false;
            IDTebtBox.Text = customer.getIdBL().ToString();
            NameTextBox.Text = customer.NameBL;
            PhoneTextBox.Text = customer.PhoneBL;
            LongitudeTextBox.Text = customer.Position.Longitude.ToString();
            LatitudeTextBox.Text = customer.Position.Latitude.ToString();
            ADD_BUTTON.Visibility = Visibility.Hidden;
            UPDATE_BUTTON.Visibility = Visibility.Visible;
        }
        private void showParcel(object sender, RoutedEventArgs e)
        {
            BO.DeliveryAtCustomer parcel = (sender as ListView).SelectedValue as BO.DeliveryAtCustomer;
            if (parcel != null)
            {
                this.Close();
                new DisplayParcel(Bl, Bl.convertParcelToParcelBl(parcel.Id)).ShowDialog();
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
            Bl.RemoveCustomerById(Int32.Parse(IDTebtBox.Text));
            this.Close();
        }
    }
}
