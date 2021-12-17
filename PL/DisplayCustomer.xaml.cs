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
    /// Interaction logic for DisplayCustomer.xaml
    /// </summary>
    public partial class DisplayCustomer : Window
    {
        IBL.IBL Bl;
        public DisplayCustomer(IBL.IBL bl, CustomerBL customer)
        {
            Bl = bl;
            InitializeComponent();
            Isent.Visibility = SENDme.Visibility = Visibility.Visible;
            Isent.Content = returnLis(customer.ImTheSender);
            SENDme.Content = returnLis(customer.ImTheTarget);
            IDTebtBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeTextBox.IsEnabled = false;
            IDTebtBox.Text = customer.getIdBL().ToString();
            NameTextBox.Text = customer.NameBL;
            PhoneTextBox.Text = customer.PhoneBL;
            LongitudeTextBox.Text = customer.Position.Longitude.ToString();
            LatitudeTextBox.Text = customer.Position.Latitude.ToString();
            ADD_BUTTON.Visibility = Visibility.Hidden;
            UPDATE_BUTTON.Visibility = Visibility.Visible;

        }
        private string returnLis(List<DeliveryAtCustomer> parcels)
        {
            if (parcels.Count == 0) { return "No parcels found"; }
            string myList = "";
            foreach (DeliveryAtCustomer parcel in parcels) { myList += parcel.ToString(); }
            return myList;
        }
        public DisplayCustomer(IBL.IBL bl)
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
    }
}
