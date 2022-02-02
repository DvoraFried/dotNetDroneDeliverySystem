using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Drone_pl: INotifyPropertyChanged
    {
        public Drone_pl(Drone droneBl) {
            setId(droneBl.getIdBL());
            this.Model = droneBl.ModelBL;
            this.MaxWeight = (Enum_pl.WeightCategories)droneBl.MaxWeight;
            this.DroneStatus = (Enum_pl.DroneStatuses)droneBl.DroneStatus;
            this.Delivery =  new ParcelByTransfer_pl(droneBl.delivery);//לבנות קונסטרקטור
            this.CurrentPosition = new Position_pl(droneBl.CurrentPosition);//לבנות קונסטרקטור
        }
        private int id;

        public event PropertyChangedEventHandler PropertyChanged;

        public void setId(int idD)
        {
            id = idD;
        }
        public int getId() { return id; }
        public string Model{ get; set; }
        public Enum_pl.WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public Enum_pl.DroneStatuses DroneStatus { get; set; }
        public ParcelByTransfer_pl Delivery { get; set; }
        public Position_pl CurrentPosition { get; set; }
        
    }
}
