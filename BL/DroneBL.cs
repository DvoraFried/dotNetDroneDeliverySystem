using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public class DroneBL
    {
        public DroneBL(int id,string model,WeightCategoriesBL maxW, DroneStatusesBL status,Position p)
        {
            Random rnd = new Random();
            this.setIdBL(id);
            this.ModelBL = model;
            this.MaxWeight = maxW;
            this.CurrentPosition = p;
            this.BatteryStatus = rnd.Next(20, 41);
            this.DroneStatus = status;
        }
        private int idBL;
        public void setIdBL(int idD) 
        {
            if (idD <=0) {throw new UnValidIdException(idD, "drone");}
            idBL = idD;
        }
        public int getIdBL() { return idBL; }
        public string ModelBL { get; set; }
        public WeightCategoriesBL MaxWeight { get; set; }
        public int BatteryStatus { get; set; }
        public DroneStatusesBL DroneStatus;
        //DeliveryByTransfer
        public Position CurrentPosition;

/*        public void setCurrentPosition(int stationId)
        {
            IEnumerable<StationBL> staions = new List<StationBL>(); //  במקום זה -> צריך למשוך מהדאטא את רשימת התחנות
            foreach (StationBL elemnt in staions)
            {

            }
        }*/
    }
}
