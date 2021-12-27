using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public  ClientSide(BlApi.IBL bl, CustomerBL customer)
        {
            Bl = bl;
            InitializeComponent();

        }

        private void personal_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 0;
        }

        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 1;
        }

        private void customerParcels_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 2;
        }

        private void ConfirmParcelReceipt_Click(object sender, RoutedEventArgs e)
        {
            app.SelectedIndex = 3;
        }
    }
}
