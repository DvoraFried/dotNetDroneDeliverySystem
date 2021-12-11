using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;
using static IBL.BO.DistanceBetweenCoordinates;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
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
                        // position
                        // position
                        drone.BatteryStatus = rnd.Next((int)(DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition, findClosestStation(drone.CurrentPosition)) * nonWeightPowerConsumption), 100);
                    }
                    else
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.maintenance;
                        drone.BatteryStatus = rnd.Next(0, 21);
                        int randomStationIndex = rnd.Next(0, DalObj.returnParcelArray().Count<ParcelDAL>());
                        drone.CurrentPosition = new Position((DataSource.MyBaseStations[randomStationIndex]).Longitude, (DataSource.MyBaseStations[randomStationIndex]).Latitude);
                    }
                }
                else
                {
                    drone.DroneStatus = EnumBL.DroneStatusesBL.Shipping;
                    int senderIndex = DataSource.MyCustomers.FindIndex(customer => customer.Id == DataSource.MyParcels[parcelIndex].SenderId);
                    Position senderPos = new Position(DataSource.MyCustomers[senderIndex].Longitude, DataSource.MyCustomers[senderIndex].Latitude);
                    int targetIndex = DataSource.MyCustomers.FindIndex(customer => customer.Id == DataSource.MyParcels[parcelIndex].TargetId);
                    Position targetPos = new Position(DataSource.MyCustomers[targetIndex].Longitude, DataSource.MyCustomers[targetIndex].Latitude);
                    // how to cheke date time of pickup
                    drone.CurrentPosition = true ? findClosestStation(drone.CurrentPosition) : senderPos;
                    double distanceToTarget = DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition,targetPos);
                    double distanceFromTargetToStation = DistanceBetweenCoordinates.CalculateDistance(targetPos, findClosestStation(targetPos));
                    drone.BatteryStatus = (int)DataSource.MyParcels[parcelIndex].Weight == 1 ? rnd.Next((int)(distanceToTarget*lightWeightPowerConsumption + distanceFromTargetToStation*nonWeightPowerConsumption), 100) :
                                          (int)DataSource.MyParcels[parcelIndex].Weight == 2 ? rnd.Next((int)(distanceToTarget * mediumWeightPowerConsumption + distanceFromTargetToStation * nonWeightPowerConsumption), 100) :
                                          rnd.Next((int)(distanceToTarget * heavyWeightPowerConsumption + distanceFromTargetToStation * nonWeightPowerConsumption), 100);

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

        public static Position findClosestStation(Position current)
        {
            Position stationPos = null, closeP = current;
            double distance = DistanceBetweenCoordinates.CalculateDistance(current, new Position(DataSource.MyBaseStations[0].Longitude, DataSource.MyBaseStations[0].Latitude)); 
            foreach (StationDAL element in DataSource.MyBaseStations)
            {
                stationPos = new Position(element.Longitude, element.Latitude);
                if (distance > DistanceBetweenCoordinates.CalculateDistance(current, stationPos)){
                    distance = DistanceBetweenCoordinates.CalculateDistance(current, stationPos);
                    closeP = stationPos;
                }
            }
            return stationPos;
        }
    }
}