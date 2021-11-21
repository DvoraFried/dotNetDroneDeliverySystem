using DalObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BL
    {
        public class UpDate
        {
            public void UpDateDroneName(int id, string newModelName)
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
