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
        private int IdBL;
        public void setIdBL(int idD) 
        {
            if (idD <=0) {throw new UnValidIdException(idD, "drone");}
            IdBL = idD;
        }
        public int getIdBL() { return IdBL; }
        public string ModelBL { get; set; }
        public WeightCategoriesBL MaxWeight { get; set; }
        public int BatteryStatus { get; set; }
        //Drone status
        //DeliveryByTransfer
        //private Position CurrentPosition;
        public void setCurrentPosition(int stationId)
        {
            IEnumerable<StationBL> staions = new List<StationBL>(); //  במקום זה -> צריך למשוך מהדאטא את רשימת התחנות
            foreach (StationBL elemnt in staions)
            {

            }
        }
    }
}
