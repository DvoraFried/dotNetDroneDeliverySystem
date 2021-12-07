
using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;
using static IBL.BO.DistanceBetweenCoordinates;
namespace IBL.BO
{
    public partial class BL : IBL
    {
        public static List<DroneBL> DronesListBL;
        static IDAL.IDAL DalObj;
        static BL BLOBJ;
        static double nonWeightPowerConsumption;
        static double lightWeightPowerConsumption;
        static double mediumWeightPowerConsumption;
        static double heavyWeightPowerConsumption;
        static double DroneLoadingRate;

        public BL()
        {
            DronesListBL = ConvertToBL.ConvertToDroneBL(DalObj.returnDroneArray());
            DalObj = DALFactory.factory();
            double[] electricityUse = DalObj.powerRequest();
            nonWeightPowerConsumption = electricityUse[0];
            lightWeightPowerConsumption = electricityUse[1];
            mediumWeightPowerConsumption = electricityUse[2];
            heavyWeightPowerConsumption = electricityUse[3];
            DroneLoadingRate = electricityUse[4];
            
        }
        public static BL GetBLOBJ
        {
            get
            {
                if (BLOBJ == null)
                    BLOBJ = new BL();
                return BLOBJ;
            }
        }
        public static double updateButteryStatus(DroneBL drone, Position position, int weight)
        {
            double distance = CalculateDistance(drone.CurrentPosition, position);
            double lessPower = drone.DroneStatus == EnumBL.DroneStatusesBL.empty ? distance* nonWeightPowerConsumption :
                               weight == (int)EnumBL.WeightCategoriesBL.light ? distance * lightWeightPowerConsumption :
                               weight == (int)EnumBL.WeightCategoriesBL.medium ? distance*mediumWeightPowerConsumption :
                               distance * heavyWeightPowerConsumption;
            if (lessPower > drone.BatteryStatus)
            {
                throw new ThereIsNotEnoughBatteryException();
            }
            return (drone.BatteryStatus - lessPower);
        }
    }
}