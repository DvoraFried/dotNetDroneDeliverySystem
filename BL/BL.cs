using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public partial class BL
    {
        public static List<DroneBL> DronesListBL = new List<DroneBL>();

        static IDAL.DO.IDAL DalObj = DALFactory.factory();
        public class UpDate
        {
            public void UpDateDroneName(int id,string newModelName)
            {
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                if (droneBLIndex != -1) 
                {
                    DroneBL drone = DronesListBL[droneBLIndex];
                    drone.ModelBL = newModelName;
                    DataSource.MyDrones[droneBLIndex] = ConvertToDal.ConvertToDroneDal(drone);
                }; 
            }
        }
    }
}