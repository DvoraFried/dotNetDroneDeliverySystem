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
    /// Interaction logic for DisplayParcelList.xaml
    /// </summary>
    public partial class DisplayParcelList : Window
    {
        BlApi.IBL BLobj;
        public DisplayParcelList(BlApi.IBL bl)
        {
            InitializeComponent();
            BLobj = bl;
            parcelDisplay.ItemsSource = BLobj.ReturnParcelList();
        }

        private void ButtonAddParcel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new DisplayParcel(BLobj).ShowDialog();
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BO.ParcelToList parcel = (sender as ListView).SelectedValue as BO.ParcelToList;
            if (parcel != null)
            {
                this.Close();
                new DisplayParcel(BLobj, BLobj.convertParcelToParcelBl(parcel.Id)).ShowDialog();
            }
        }
        private void ButtonGroupBySender_Click(object sender, RoutedEventArgs e)
        {
            parcelDisplay.ItemsSource = BLobj.ReturnPacelListGroupBySender();
        }

        private void buttonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            parcelDisplay.ItemsSource = BLobj.ReturnParcelList();
        }
    }
}
