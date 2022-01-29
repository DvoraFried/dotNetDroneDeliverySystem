using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Drone_pl
    {
        private int idBL;
        public void setIdBL(int idD)
        {
            idBL = idD;
        }
        public int getIdBL() { return idBL; }
        public string ModelBL { get; set; }
        public Enum_pl.WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public Enum_pl.DroneStatuses DroneStatus { get; set; }
        public ParcelByTransfer_pl delivery { get; set; }
        public Position_pl CurrentPosition { get; set; }
        
    }
}
