﻿using IBL.BO;
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
        IBL.IBL BLobj;
        public DisplayParcelList(IBL.IBL bl)
        {
            InitializeComponent();
            BLobj = bl;
            parcelDisplay.ItemsSource = BLobj.ReturnParcelList();
        }

        private void ButtonAddParcel_Click(object sender, RoutedEventArgs e)
        {
            IBL.BO.ParcelBL parcel = (sender as ListView).SelectedValue as IBL.BO.ParcelBL;
            new DisplayParcel(BLobj, parcel).Show();
        }
    }
}
