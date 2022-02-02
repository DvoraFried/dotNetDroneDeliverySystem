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
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("id"));
        }
        public int getId() { return id; }
        private string Model;
        public string getModel()
        {
            return Model;
        }
        public void set_Model(string newModel)
        {
            Model = newModel;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Model"));
        }
        public Enum_pl.WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public void setDroneStatus(Enum_pl.DroneStatuses newS)
        {
            DroneStatus = newS;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("DroneStatus"));
        }
        private Enum_pl.DroneStatuses DroneStatus;
        public Enum_pl.DroneStatuses getDroneStatus()
        {
            return DroneStatus;
        }
        public ParcelByTransfer_pl Delivery { get; set; }
        public Position_pl CurrentPosition { get; set; }
        
    }
}
