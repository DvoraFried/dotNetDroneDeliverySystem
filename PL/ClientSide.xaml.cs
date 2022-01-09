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
    /// Interaction logic for ClientSide.xaml
    /// </summary>
    public partial class ClientSide : Window
    {

        BlApi.IBL Bl;
        CustomerBL currentCustomer;
        public ClientSide(BlApi.IBL bl, CustomerBL customer)
        {
            Bl = bl;
            InitializeComponent();
            //exIsent.Visibility = exSENDme.Visibility = Visibility.Visible;
            //foreach (DeliveryAtCustomer parcel in customer.ImTheSender) { Isent.Items.Add(parcel); }
            //foreach (DeliveryAtCustomer parcel in customer.ImTheTarget) { Isent.Items.Add(parcel); }
            IDTebtBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeTextBox.IsEnabled = false;
            IDTebtBox.Text = customer.getIdBL().ToString();
            NameTextBox.Text = customer.NameBL;
            PhoneTextBox.Text = customer.PhoneBL;
            LongitudeTextBox.Text = customer.Position.Longitude.ToString();
            LatitudeTextBox.Text = customer.Position.Latitude.ToString();
            //ADD_BUTTON.Visibility = Visibility.Hidden;
            currentCustomer = customer;
        }

        private void personal_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 0;
        }
        private void customerParcels_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 1;
            new DisplayParcelList(Bl, currentCustomer).ShowDialog();
        }
        private void ConfirmParcelReceipt_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 2;
        }
        /*        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 3;
        }
*/
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.UpDateCustomerData(Int32.Parse(IDTebtBox.Text), NameTextBox.Text, PhoneTextBox.Text);
            }
            catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            MessageBox.Show("uptade!");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
        private void ButtonAddParcel_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcel(Bl, currentCustomer).ShowDialog();
        }

    }
}
