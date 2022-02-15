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
    /// Interaction logic for ClientSide.xaml
    /// </summary>
    public partial class ClientSide : Window
    {

        BlApi.IBL Bl;
        Customer_pl currentCustomer;
        public ClientSide(BlApi.IBL bl, Customer customer)
        {
            Bl = bl;
            currentCustomer = new Customer_pl(Bl,customer);
            InitializeComponent();
            DataContext = currentCustomer;
            IDTebtBox.IsEnabled = LongitudeTextBox.IsEnabled = LatitudeTextBox.IsEnabled = false;
            LongitudeTextBox.Text = customer.Position.Longitude.ToString();
            LatitudeTextBox.Text = customer.Position.Latitude.ToString();
        }

        private void customerParcels_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(Bl, Bl.GetCustomerByID(currentCustomer.Id)).Show();
        }
        private void toCustomerParcels_Click(object sender, RoutedEventArgs e)
        {
            new DisplayParcelList(Bl, Bl.GetCustomerByID(currentCustomer.Id), true).Show();
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                Bl.UpDateCustomerData(Int32.Parse(IDTebtBox.Text), NameTextBox.Text, PhoneTextBox.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            MessageBox.Show("uptade!");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
