using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class drone_pl: DependencyObject
    {
        public drone_pl(BlApi.IBL blObj,Drone droneBl) {
            Id = droneBl.Id;
            this.Model = droneBl.ModelBL;
            this.MaxWeight = (Enum_pl.WeightCategories)droneBl.MaxWeight;
            this.DroneStatus = (Enum_pl.DroneStatuses)droneBl.DroneStatus;
            this.Delivery =  new ParcelByTransfer_pl(droneBl.delivery);
            this.CurrentPosition = new Position_pl(droneBl.CurrentPosition);
            BatteryStatus = droneBl.BatteryStatus;
            blObj.ActionDroneChanged += UpdatePlDrone;
        }

        public void UpdatePlDrone(BO.Drone droneBl)
        {
            this.DroneStatus = (Enum_pl.DroneStatuses)droneBl.DroneStatus;
            this.Delivery = new ParcelByTransfer_pl(droneBl.delivery);
            this.CurrentPosition = new Position_pl(droneBl.CurrentPosition);
            this.BatteryStatus = droneBl.BatteryStatus;
        }

        public static readonly DependencyProperty idProperty =
        DependencyProperty.Register("Id",
                     typeof(object),
                     typeof(drone_pl),
                     new UIPropertyMetadata(0));
        public int Id
        {
            get { return (int)GetValue(idProperty);}
            set { SetValue(idProperty, value);}
        }

        public static readonly DependencyProperty modelProperty =
        DependencyProperty.Register("Model",
               typeof(object),
               typeof(drone_pl),
               new UIPropertyMetadata(0));
        public string Model
        {
            get { return (string)GetValue(modelProperty);}
            set { SetValue(modelProperty, value);}
        }

        public static readonly DependencyProperty DroneStatusProperty =
        DependencyProperty.Register("DroneStatus",
               typeof(object),
               typeof(drone_pl),
               new UIPropertyMetadata(0));
        public Enum_pl.DroneStatuses DroneStatus
        {
            get { return (Enum_pl.DroneStatuses)GetValue(DroneStatusProperty); }
            set { SetValue(DroneStatusProperty, value); }
        }

        public static readonly DependencyProperty DeliveryProperty =
        DependencyProperty.Register("Delivery",
               typeof(object),
               typeof(drone_pl),
               new UIPropertyMetadata(0));
        public ParcelByTransfer_pl Delivery
        {
            get { return (ParcelByTransfer_pl)GetValue(DeliveryProperty); }
            set { SetValue(DeliveryProperty, value); }
        }

        private static readonly DependencyProperty BatteryStatusProperty =
        DependencyProperty.Register("BatteryStatus",
            typeof(object),
            typeof(drone_pl),
            new UIPropertyMetadata(0));
        public double BatteryStatus
        {
            get { return (double)GetValue(BatteryStatusProperty); }
            set { SetValue(BatteryStatusProperty, value); }
        }

        public static readonly DependencyProperty MaxWeightProperty =
        DependencyProperty.Register("MaxWeight",
            typeof(object),
            typeof(drone_pl),
            new UIPropertyMetadata(0));
        public Enum_pl.WeightCategories MaxWeight
        {
            get { return (Enum_pl.WeightCategories)GetValue(MaxWeightProperty); }
            set { SetValue(MaxWeightProperty, value); }
        }

        private static readonly DependencyProperty CurrentPositionProperty =
        DependencyProperty.Register("CurrentPosition",
          typeof(object),
          typeof(drone_pl),
          new UIPropertyMetadata(0));
        public Position_pl CurrentPosition
        {
            get { return (Position_pl)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }
        
    }
}
