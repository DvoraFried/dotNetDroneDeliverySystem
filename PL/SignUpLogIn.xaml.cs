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
    /// Interaction logic for SignUpLogIn.xaml
    /// </summary>
    public partial class SignUpLogIn : Window
    {
        BlApi.IBL bl;
        public SignUpLogIn()
        {
            bl = BLFactory.factory();
            InitializeComponent();
        }
        private void registerTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = MyTabControl.SelectedIndex + 1;
            MyTabControl.SelectedIndex = newIndex;
        }
        private void logInTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = MyTabControl.SelectedIndex - 1;
            MyTabControl.SelectedIndex = newIndex;
        }
        private void ButtonsignUp_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {

            if (bl.userIsCustomer(name.Text, Int32.Parse(id.Password));
            {
                CustomerToList customer = (sender as ListView).SelectedValue as CustomerToList;
                Close();
                new ClientSide(bl, bl.convertToB  (customer)).ShowDialog();
            }
           /* CustomerToList customer = (sender as ListView).SelectedValue as CustomerToList;*/
            Close();
            new ClientSide(bl, bl.conver.ShowDialog();
        }
    }
}
