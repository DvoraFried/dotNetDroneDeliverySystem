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
    /// Interaction logic for DisplayCustomerList.xaml
    /// </summary>
    public partial class DisplayCustomerList : Window
    {

        IBL.IBL Bl;
        public DisplayCustomerList(IBL.IBL bl)
        {
            InitializeComponent();
            Bl = bl;
            customersDisplay.ItemsSource = Bl.ReturnCustomerList();
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IBL.BO.CustomerToList customer = (sender as ListView).SelectedValue as IBL.BO.CustomerToList;
            new DisplayCustomer(Bl, Bl.convertCustomerToListToCustomerBl(customer)).Show();
        }
        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new DisplayCustomer(Bl).Show();
        }
    }
}
