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

        public static DroneBL ConvertToDroneBL(DroneDAL droneDal,int statusD,Position p,int closectStationId)
        {
            DroneBL droneBl = new DroneBL(droneDal.Id,droneDal.Model,(EnumBL.WeightCategoriesBL)(int)droneDal.MaxWeight,(EnumBL.DroneStatusesBL)statusD,p, closectStationId);
            return droneBl;
        }
    }
}
