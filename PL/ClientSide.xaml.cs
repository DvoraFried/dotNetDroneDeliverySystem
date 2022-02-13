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
        Customer currentCustomer;
        public ClientSide(BlApi.IBL bl, Customer customer)
        {
            Bl = bl;
            InitializeComponent();
            IDTebtBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeTextBox.IsEnabled = false;
            IDTebtBox.Text = customer.getIdBL().ToString();
            NameTextBox.Text = customer.NameBL;
            PhoneTextBox.Text = customer.PhoneBL;
            LongitudeTextBox.Text = customer.Position.Longitude.ToString();
            LatitudeTextBox.Text = customer.Position.Latitude.ToString();
            currentCustomer = customer;
        }

        private void personal_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 0;
        }
        private void customerParcels_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(Bl, currentCustomer).ShowDialog();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try {
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
                new DisplayParcel(Bl, Bl.returnParcel(parcel.Id)).ShowDialog();
            }
        }

        private void toCustomerParcels_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(Bl, currentCustomer, true).ShowDialog();
        }
    }
}
