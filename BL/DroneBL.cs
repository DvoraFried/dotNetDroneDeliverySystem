using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    public class DroneBL
    {
        private int IdBL;
        public void setIdBL(int idD) 
        {
            IEnumerable<DroneBL> drones = new List<DroneBL>(); //  במקום זה -> צריך למשוך מהדאטא את רשימת הרחפנים
            foreach (DroneBL drone in drones)
            {
                if (drone.getIdBL() == idD)
                {
                    throw new ArgumentException("~ The ID number already exists in the system ~");
                }
            }
            IdBL = idD;
        }
        public int getIdBL() { return IdBL; }
        public string ModelBL { get; set; }
        public WeightCategoriesBL MaxWeight { get; set; }
        public int BatteryStatus { get; set; }
        //Drone status
        //DeliveryByTransfer
        public Position CurrentPosition { get; set; }

    }
}
