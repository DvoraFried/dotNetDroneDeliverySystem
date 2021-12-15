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
    /// Interaction logic for DisplayCustomers.xaml
    /// </summary>
    public partial class DisplayCustomerList : Window
    {
        IBL.IBL BLobj;
        public DisplayCustomerList(IBL.IBL bl)
        {
            InitializeComponent();
            BLobj = bl;
            customerDisplay.ItemsSource = BLobj.ReturnParcelList();
        }
        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new DisplayCustomer(BLobj).Show();
        }
        private void listView_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.CustomerToList customer = (sender as ListView).SelectedValue as IBL.BO.CustomerToList;
            new DisplayCustomer(BLobj, BLobj.convertCustomerToListToCusromerBl(customer)).Show();
        }
        private void parcelDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.CustomerToList customer = (sender as ListView).SelectedValue as IBL.BO.CustomerToList;
            new DisplayCustomer(BLobj, BLobj.convertCustomerToListToCusromerBl(customer)).Show();
        }


    }
}