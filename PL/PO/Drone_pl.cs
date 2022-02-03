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
    public class Drone_pl: DependencyObject
    {
        public Drone_pl(BlApi.IBL blObj,Drone droneBl) {
            Id = droneBl.getIdBL();
            this.Model = droneBl.ModelBL;
            this.MaxWeight = (Enum_pl.WeightCategories)droneBl.MaxWeight;
            this.DroneStatus = (Enum_pl.DroneStatuses)droneBl.DroneStatus;
            this.Delivery =  new ParcelByTransfer_pl(droneBl.delivery);//לבנות קונסטרקטור
            this.CurrentPosition = new Position_pl(droneBl.CurrentPosition);//לבנות קונסטרקטור
            blObj.ActionDroneChanged += UpdatePlDrone;
        }
        public Drone_pl(BlApi.IBL blObj)
        {
            blObj.ActionDroneChanged += UpdatePlDrone;
        }

        public void UpdatePlDrone(BO.Drone droneBl)
        {
            Id = droneBl.getIdBL();
            this.Model = droneBl.ModelBL;
            this.MaxWeight = (Enum_pl.WeightCategories)droneBl.MaxWeight;
            this.DroneStatus = (Enum_pl.DroneStatuses)droneBl.DroneStatus;
            this.Delivery = new ParcelByTransfer_pl(droneBl.delivery);//לבנות קונסטרקטור
            this.CurrentPosition = new Position_pl(droneBl.CurrentPosition);//לבנות קונסטרקטור
        }

        public static readonly DependencyProperty idProperty =
        DependencyProperty.Register("Id",
                     typeof(object),
                     typeof(Drone_pl),
                     new UIPropertyMetadata(0));
        public int Id
        {
            get { return (int)GetValue(idProperty);}
            set { SetValue(idProperty, value);}
        }

        public static readonly DependencyProperty modelProperty =
        DependencyProperty.Register("Model",
               typeof(object),
               typeof(Drone_pl),
               new UIPropertyMetadata(0));
        public string Model
        {
            get { return (string)GetValue(modelProperty);}
            set { SetValue(modelProperty, value);}
        }

        public static readonly DependencyProperty DroneStatusProperty =
        DependencyProperty.Register("DroneStatus",
               typeof(object),
               typeof(Drone_pl),
               new UIPropertyMetadata(0));
        public Enum_pl.DroneStatuses DroneStatus
        {
            get { return (Enum_pl.DroneStatuses)GetValue(DroneStatusProperty); }
            set { SetValue(DroneStatusProperty, value); }
        }

        public static readonly DependencyProperty DeliveryProperty =
        DependencyProperty.Register("Delivery",
               typeof(object),
               typeof(Drone_pl),
               new UIPropertyMetadata(0));
        public ParcelByTransfer_pl Delivery
        {
            get { return (ParcelByTransfer_pl)GetValue(DeliveryProperty); }
            set { SetValue(DeliveryProperty, value); }
        }

        public static readonly DependencyProperty BatteryStatusProperty =
        DependencyProperty.Register("BatteryStatus",
            typeof(object),
            typeof(Drone_pl),
            new UIPropertyMetadata(0));
        public double BatteryStatus
        {
            get { return (double)GetValue(BatteryStatusProperty); }
            set { SetValue(BatteryStatusProperty, value); }
        }

        public Enum_pl.WeightCategories MaxWeight { get; set; }
        public Position_pl CurrentPosition { get; set; }
        
    }
}
