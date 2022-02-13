﻿using BO;
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
        public Customer currentCustomer = null;
        BlApi.IBL BLobj;
        public DisplayParcelList(BlApi.IBL bl)
        {
            InitializeComponent();
            BLobj = bl;
            parcelDisplay.ItemsSource = BLobj.ReturnParcelList();

        }
        public DisplayParcelList(BlApi.IBL bl, Customer customer, bool toMe = false)
        {
            currentCustomer = customer;
            InitializeComponent();
            BLobj = bl;
            IEnumerable<ParcelToList> parcels = BLobj.ReturnParcelList();
            parcelDisplay.ItemsSource = from parcel in parcels
                                        where toMe ? parcel.TargetId == customer.getIdBL() : parcel.SenderId == customer.getIdBL()
                                        select parcel;
            groupBy.Visibility = clear.Visibility = Visibility.Hidden;
        }
        private void ButtonAddParcel_Click(object sender, RoutedEventArgs e)
        {
            if (currentCustomer != null)
            {
                new DisplayParcel(BLobj, currentCustomer).Show();
            }
            else { new DisplayParcel(BLobj).Show(); }
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BO.ParcelToList parcel = (sender as ListView).SelectedValue as BO.ParcelToList;
            if (parcel != null)
            {
                new DisplayParcel(BLobj, BLobj.returnParcel(parcel.Id)).Show();
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
