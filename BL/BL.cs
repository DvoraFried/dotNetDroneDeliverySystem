
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
        Random rnd = new Random();

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
            DalObj = DALFactory.factory();
            
            double[] electricityUse = DalObj.powerRequest();
            nonWeightPowerConsumption = electricityUse[0];
            lightWeightPowerConsumption = electricityUse[1];
            mediumWeightPowerConsumption = electricityUse[2];
            heavyWeightPowerConsumption = electricityUse[3];
            DroneLoadingRate = electricityUse[4];
            
            DronesListBL = ConvertToBL.ConvertToDroneArrayBL((List<DroneDAL>)DalObj.returnDroneArray());
            
            foreach(DroneBL drone in DronesListBL)
            {
                int parcelIndex = DataSource.MyParcels.FindIndex(parcel => parcel.DroneId == drone.getIdBL());
                if(parcelIndex == -1)
                {
                    int status = rnd.Next(0, 2);
                    if (status == 0)
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.empty;
                        //=== i dont want to write it now!!!
                    }
                    else
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.maintenance;
                        drone.BatteryStatus = rnd.Next(0, 21);
                        // drone.CurrentPosition = ;
                    }
                }
                else
                {
                    drone.DroneStatus = EnumBL.DroneStatusesBL.Shipping;
                    //=== i dont want to write it now!!!
                }
            }
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