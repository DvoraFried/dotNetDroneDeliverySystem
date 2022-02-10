﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PO.Enum_pl;

namespace PO
{
    public class Parcel_pl: DependencyObject
    {
        public Parcel_pl(BlApi.IBL blObj, Parcel parcelBL)
        {
            Id = parcelBL.GetParcelId();
            Weight = (Enum_pl.WeightCategories)parcelBL.Weight;
            Priority = (Enum_pl.Priorities)parcelBL.Priority;
            DroneInParcel = new DroneInParcel_pl(parcelBL.DroneIdBL);
            Requested = parcelBL.RequestedBL;
            Scheduled = parcelBL.ScheduledBL;
            PickUp = parcelBL.PickUpBL;
            Delivered = parcelBL.DeliveredBL;
            Sender = new CustomerOnDelivery_pl(parcelBL.Sender);
            Target = new CustomerOnDelivery_pl(parcelBL.Target);
            blObj.ActionParcelChanged += UpdatePlParcel;
        }
        public Parcel_pl(BlApi.IBL blObj)
        {
            blObj.ActionParcelChanged += UpdatePlParcel;
        }
        public void UpdatePlParcel(BO.Parcel parcelBL)
        {
            Id = parcelBL.GetParcelId();
            Weight = (Enum_pl.WeightCategories)parcelBL.Weight;
            Priority = (Enum_pl.Priorities)parcelBL.Priority;
            DroneInParcel = new DroneInParcel_pl(parcelBL.DroneIdBL);
            Requested = parcelBL.RequestedBL;
            Scheduled = parcelBL.ScheduledBL;
            PickUp = parcelBL.PickUpBL;
            Delivered = parcelBL.DeliveredBL;
            Sender = new CustomerOnDelivery_pl(parcelBL.Sender);
            Target = new CustomerOnDelivery_pl(parcelBL.Target);
        }

    public static readonly DependencyProperty idPProperty =
        DependencyProperty.Register("IdParcel",
                     typeof(object),
                     typeof(drone_pl),
                     new UIPropertyMetadata(0));
        private int Id
        {
            get { return (int)GetValue(idPProperty); }
            set { SetValue(idPProperty, value); }
        }

        public static readonly DependencyProperty WeightPProperty =
        DependencyProperty.Register("WeightParcel",
                     typeof(object),
                     typeof(Parcel_pl),
                     new UIPropertyMetadata(0));
        private WeightCategories Weight
        {
            get { return (WeightCategories)GetValue(WeightPProperty); }
            set { SetValue(WeightPProperty, value); }
        }

        public static readonly DependencyProperty PriorityPProperty =
        DependencyProperty.Register("PriorityParcel",
                   typeof(object),
                   typeof(Parcel_pl),
                   new UIPropertyMetadata(0));
        private Priorities Priority
        {
            get { return (Priorities)GetValue(PriorityPProperty); }
            set { SetValue(PriorityPProperty, value); }
        }

        public static readonly DependencyProperty RequestedPProperty =
        DependencyProperty.Register("RequestedParcel",
                typeof(object),
                typeof(Parcel_pl),
                new UIPropertyMetadata(0));
        private DateTime? Requested
        {
            get { return (DateTime?)GetValue(RequestedPProperty); }
            set { SetValue(RequestedPProperty, value); }
        }

        public static readonly DependencyProperty ScheduledPProperty =
        DependencyProperty.Register("ScheduledParcel",
              typeof(object),
              typeof(Parcel_pl),
              new UIPropertyMetadata(0));
        private DateTime? Scheduled
        {
            get { return (DateTime?)GetValue(ScheduledPProperty); }
            set { SetValue(ScheduledPProperty, value); }
        }

        public static readonly DependencyProperty PickUpPProperty =
        DependencyProperty.Register("PickUpParcel",
              typeof(object),
              typeof(Parcel_pl),
              new UIPropertyMetadata(0));
        private DateTime? PickUp
        {
            get { return (DateTime?)GetValue(PickUpPProperty); }
            set { SetValue(PickUpPProperty, value); }
        }

        public static readonly DependencyProperty DeliveredPProperty =
        DependencyProperty.Register("DeliveredParcel",
              typeof(object),
              typeof(Parcel_pl),
              new UIPropertyMetadata(0));
        private DateTime? Delivered
        {
            get { return (DateTime?)GetValue(DeliveredPProperty); }
            set { SetValue(DeliveredPProperty, value); }
        }

        public static readonly DependencyProperty DroneInParcelProperty =
        DependencyProperty.Register("DroneInParcel",
          typeof(object),
          typeof(Parcel_pl),
          new UIPropertyMetadata(0));
        private DroneInParcel_pl DroneInParcel
        {
            get { return (DroneInParcel_pl)GetValue(DroneInParcelProperty); }
            set { SetValue(DroneInParcelProperty, value); }
        }
        private CustomerOnDelivery_pl Sender { get; set; }
        private CustomerOnDelivery_pl Target { get; set; }
    }
}

