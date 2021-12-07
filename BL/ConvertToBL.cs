using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ConvertToBL
    {
        public static List<DroneBL> ConvertToDroneArrayBL(List<DroneDAL> droneDalArray)
        {
            List<DroneBL> droneArrayBl = new List<DroneBL>();
            foreach(DroneDAL drone in droneDalArray)
            {
                droneArrayBl.Add(new DroneBL(drone.Id, drone.Model, (EnumBL.WeightCategoriesBL)(int)drone.MaxWeight, 0, null, 0));
            };
            return droneArrayBl;
        }
    }
}
