
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;
using static BO.DistanceBetweenCoordinates;
using BO;

namespace BL
{
    sealed partial  class BL : BlApi.IBL
    {
        Random rnd = new Random();
        public static List<DroneBL> DronesListBL;
        static DalApi.IDal DalObj;
        static double nonWeightPowerConsumption;
        static double lightWeightPowerConsumption;
        static double mediumWeightPowerConsumption;
        static double heavyWeightPowerConsumption;
        static double DroneLoadingRate;

        BL()
        {
            DalObj = DalApi.DalFactory.GetDal();
            double[] electricityUse = DalObj.powerRequest();
            nonWeightPowerConsumption = electricityUse[0];
            lightWeightPowerConsumption = electricityUse[1];
            mediumWeightPowerConsumption = electricityUse[2];
            heavyWeightPowerConsumption = electricityUse[3];
            DroneLoadingRate = electricityUse[4];
            
            DronesListBL = ConvertToBL.ConvertToDroneArrayBL(DalObj.returnDroneArray().ToList());
            
            foreach(DroneBL drone in DronesListBL)
            {
                List<Parcel> arr = DalObj.returnParcelArray().ToList();
                if (!arr.Any(parcel => parcel.DroneId == drone.getIdBL()))
                {
                    if (rnd.Next(0, 2) == 0)
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.empty;
                        if (DalObj.returnParcelArray().ToList().Any(parcel => parcel.Delivered != null))
                        {
                            List<Parcel> parcelsThatDelivered = DalObj.returnParcelArray().ToList().FindAll(parcel => parcel.Delivered != null);
                            Customer randomCustomer = DalObj.returnCustomer(parcelsThatDelivered[rnd.Next(0, parcelsThatDelivered.Count)].TargetId);
                            drone.CurrentPosition = new Position(randomCustomer.Longitude, randomCustomer.Latitude);
                        }
                        else
                        {
                            List<Station> stations = DalObj.returnStationArray().ToList();
                            int randomIndex = rnd.Next(0, stations.Count);
                            drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                        }
                        drone.BatteryStatus = rnd.Next((int)(DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition, findClosestStation(drone.CurrentPosition)) * nonWeightPowerConsumption), 100);
                    }
                    else
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.maintenance;
                        drone.BatteryStatus = rnd.Next(0, 21);
                        List<Station> stations = DalObj.returnStationArray().ToList();
                        int randomIndex = rnd.Next(0, stations.Count);
                        drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                        SendDroneToCharge(drone.getIdBL());
                    }
                }
                else
                {
                    drone.DroneStatus = EnumBL.DroneStatusesBL.Shipping;
                    Parcel parcel = DalObj.returnParcelByDroneId(drone.getIdBL());
                    Position senderPos = new Position(DalObj.returnCustomer(parcel.SenderId).Longitude, DalObj.returnCustomer(parcel.SenderId).Latitude);
                    Position targetPos = new Position(DalObj.returnCustomer(parcel.TargetId).Longitude, DalObj.returnCustomer(parcel.TargetId).Latitude);
                    drone.CurrentPosition = parcel.PickUp == null ? findClosestStation(senderPos) : senderPos;
                    double distanceToTarget = DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition,targetPos);
                    double PowerOfdistanceFromTargetToStation = DistanceBetweenCoordinates.CalculateDistance(targetPos, findClosestStation(targetPos))*nonWeightPowerConsumption;
                    drone.BatteryStatus = (int)parcel.Weight == 1 ? rnd.Next((int)(distanceToTarget*lightWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100) :
                                          (int)parcel.Weight == 2 ? rnd.Next((int)(distanceToTarget * mediumWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100) :
                                          rnd.Next((int)(distanceToTarget * heavyWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100);
                }
            }
        }
        internal static BL instance = null;
        private static readonly object padLock = new object();

        public static BL GetBl
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new BL();
                        }
                    }
                }
                return instance;
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
            double distance = DistanceBetweenCoordinates.CalculateDistance(current, new Position(DalObj.returnStationArray().ToList()[0].Longitude, DalObj.returnStationArray().ToList()[0].Latitude)); 
            foreach (Station element in DalObj.returnStationArray())
            {
                stationPos = new Position(element.Longitude, element.Latitude);
                if (distance > DistanceBetweenCoordinates.CalculateDistance(current, stationPos)){
                    distance = DistanceBetweenCoordinates.CalculateDistance(current, stationPos);
                    closeP = stationPos;
                }
            }
            return stationPos;
        }
        public static String getFormattedLocationInDegree(double latitude, double longitude)
        {
            try
            {
                int latSeconds = (int)Math.Round(latitude * 3600);
                int latDegrees = latSeconds / 3600;
                latSeconds = Math.Abs(latSeconds % 3600);
                int latMinutes = latSeconds / 60;
                latSeconds %= 60;

                int longSeconds = (int)Math.Round(longitude * 3600);
                int longDegrees = longSeconds / 3600;
                longSeconds = Math.Abs(longSeconds % 3600);
                int longMinutes = longSeconds / 60;
                longSeconds %= 60;
                String latDegree = latDegrees >= 0 ? "N" : "S";
                String lonDegrees = longDegrees >= 0 ? "E" : "W";

                return Math.Abs(latDegrees) + "°" + latMinutes + "'" + latSeconds
                        + "\"" + latDegree + " " + Math.Abs(longDegrees) + "°" + longMinutes
                        + "'" + longSeconds + "\"" + lonDegrees;
            }
            catch (Exception e)
            {
                return "" + String.Format("%8.5f", latitude) + "  "
                        + String.Format("%8.5f", longitude);
            }
        }
    }
}