using System.Runtime.CompilerServices;
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
    sealed partial class BL : BlApi.IBL
    {
        Random rnd = new Random();
        public static List<BO.Drone> DronesListBL;
        internal static DalApi.IDal DalObj;
        static double nonWeightPowerConsumption;
        static double lightWeightPowerConsumption;
        static double mediumWeightPowerConsumption;
        static double heavyWeightPowerConsumption;
        static double DroneLoadingRate;

        #region BL CTOR
        BL()
        {
            DalObj = DalApi.DalFactory.GetDal();
            double[] electricityUse = DalObj.powerRequest();
            nonWeightPowerConsumption = electricityUse[0];
            lightWeightPowerConsumption = electricityUse[1];
            mediumWeightPowerConsumption = electricityUse[2];
            heavyWeightPowerConsumption = electricityUse[3];
            DroneLoadingRate = electricityUse[4];

            DronesListBL = ConvertToBL.ConvertToDroneArrayBL(DalObj.returnDroneArray()).ToList();

            foreach (BO.Drone drone in DronesListBL)
            {
                List<DO.Parcel> arr = DalObj.returnParcelArray().ToList();
                if (!arr.Any(parcel => parcel.DroneId == drone.Id))
                {
                    if (rnd.Next(0, 2) == 0)
                    {
                        drone.DroneStatus = BO.Enum.DroneStatusesBL.empty;
                        if (DalObj.returnParcelArray().ToList().Any(parcel => parcel.Delivered != null))
                        {
                            List<DO.Parcel> parcelsThatDelivered = DalObj.returnParcelArray().ToList().FindAll(parcel => parcel.Delivered != null);
                            DO.Customer randomCustomer = DalObj.returnCustomer(parcelsThatDelivered[rnd.Next(0, parcelsThatDelivered.Count)].TargetId);
                            drone.CurrentPosition = new Position(randomCustomer.Longitude, randomCustomer.Latitude);
                            drone.BatteryStatus = rnd.Next((int)(DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition, findClosestStation(drone.CurrentPosition).Position) * nonWeightPowerConsumption), 100);
                        }
                        else
                        {
                            List<DO.Station> stations = DalObj.returnStationArray().ToList();
                            int randomIndex = rnd.Next(0, stations.Count);
                            drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                        }
                    }
                    else
                    {
                        drone.DroneStatus = BO.Enum.DroneStatusesBL.maintenance;
                        drone.BatteryStatus = rnd.Next(0, 21);
                        List<DO.Station> stations = DalObj.returnStationArray().ToList();
                        int randomIndex = rnd.Next(0, stations.Count);
                        drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                        DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new DroneInCharge(drone), stations[randomIndex].Id));
                    }
                }
                else
                {
                    drone.DroneStatus = BO.Enum.DroneStatusesBL.Shipping;
                    DO.Parcel parcel = DalObj.returnParcelByDroneId(drone.Id);
                    Position senderPos = new Position(DalObj.returnCustomer(parcel.SenderId).Longitude, DalObj.returnCustomer(parcel.SenderId).Latitude);
                    Position targetPos = new Position(DalObj.returnCustomer(parcel.TargetId).Longitude, DalObj.returnCustomer(parcel.TargetId).Latitude);
                    drone.CurrentPosition = parcel.PickUp == null ? findClosestStation(senderPos).Position : senderPos;
                    double distanceToTarget = DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition, targetPos);
                    double PowerOfdistanceFromTargetToStation = DistanceBetweenCoordinates.CalculateDistance(targetPos, findClosestStation(targetPos).Position) * nonWeightPowerConsumption;
                    drone.BatteryStatus = (int)parcel.Weight == 1 ? rnd.Next((int)(distanceToTarget * lightWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100) :
                                          (int)parcel.Weight == 2 ? rnd.Next((int)(distanceToTarget * mediumWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100) :
                                          rnd.Next((int)(distanceToTarget * heavyWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100);
                }
            }
        }
        #endregion

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

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static double updateButteryStatus(BO.Drone drone, Position position, int weight, Position targetP = null)
        {
            double distance = targetP == null ? CalculateDistance(drone.CurrentPosition, position) : CalculateDistance(position, targetP);
            double lessPower = drone.DroneStatus == BO.Enum.DroneStatusesBL.empty ? distance * nonWeightPowerConsumption :
                               weight == (int)BO.Enum.WeightCategoriesBL.light ? distance * lightWeightPowerConsumption :
                               weight == (int)BO.Enum.WeightCategoriesBL.medium ? distance * mediumWeightPowerConsumption :
                               distance * heavyWeightPowerConsumption;
            if (lessPower > drone.BatteryStatus) {
                throw new ThereIsNotEnoughBatteryException();
            }
            return drone.BatteryStatus - lessPower;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static BO.Station findClosestStation(Position current)
        {
            DO.Station station = new DO.Station() {Name= null};
            
            foreach (DO.Station element in DalObj.returnStationArray())
            {
                Position stationP = new Position(element.Longitude, element.Latitude);
                if (element.EmptyChargeSlots > 0)
                {
                    if (station.Name == null) { station = element; }
                    else
                    {
                        Position cuurentStationP = new Position(station.Longitude, station.Latitude);
                        if (DistanceBetweenCoordinates.CalculateDistance(cuurentStationP, current) > DistanceBetweenCoordinates.CalculateDistance(stationP, current))
                        {
                            station = element;
                        }
                    }
                }
            }
            if (station.Name == null) throw new NoPlaceToChargeException();
            return ConvertToBL.ConvertToStationBL(station);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static String getFormattedLocationInDegree(double latitude, double longitude)
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