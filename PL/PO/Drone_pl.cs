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
        public Drone_pl(Drone droneBl) {
            Id = droneBl.getIdBL();
            //setId(droneBl.getIdBL());
            this.Model = droneBl.ModelBL;
            this.MaxWeight = (Enum_pl.WeightCategories)droneBl.MaxWeight;
            this.DroneStatus = (Enum_pl.DroneStatuses)droneBl.DroneStatus;
            this.Delivery =  new ParcelByTransfer_pl(droneBl.delivery);//לבנות קונסטרקטור
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

        public Enum_pl.WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }

        private Enum_pl.DroneStatuses DroneStatus;
        public Enum_pl.DroneStatuses getDroneStatus() { return DroneStatus; }
        public ParcelByTransfer_pl Delivery { get; set; }
        public Position_pl CurrentPosition { get; set; }
        
    }
}
