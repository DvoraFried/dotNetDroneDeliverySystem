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
            InitializeComponent();
            addCustomer.Visibility = Visibility.Hidden;
            toHide.Visibility = Visibility.Visible;
            customerIDTextBox.IsEnabled = customerLndTextBox.IsEnabled = customerLtdTextBox.IsEnabled = false;
            customerIDTextBox.Text = customer.getIdBL().ToString();
            customerNameTextBox.Text = customer.NameBL;
            customerPhoneTextBox.Text = customer.PhoneBL;
            customerLndTextBox.Text = customer.Position.Longitude.ToString();
            customerLtdTextBox.Text = customer.Position.Latitude.ToString();
            heSend.Content = customer.
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
        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(customerIDTextBox.Text) || string.IsNullOrWhiteSpace(customerNameTextBox.Text) || string.IsNullOrWhiteSpace(customerPhoneTextBox.Text) || string.IsNullOrWhiteSpace(customerLndTextBox.Text) || string.IsNullOrWhiteSpace(customerLtdTextBox.Text))
            {
                MessageBox.Show("one of the fields is empty");
            }
            else
            {
                try
                {
                    Bl.AddCustomer(Int32.Parse(customerIDTextBox.Text), customerNameTextBox.Text, customerPhoneTextBox.Text, double.Parse(customerLndTextBox.Text), double.Parse(customerLtdTextBox.Text));
                    this.Close();
                }
                catch (FormatException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (OverflowException) { MessageBox.Show("data reciving error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

    }
}
