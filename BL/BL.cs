using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;
using BO;
using BlApi;

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
            double[] electricityUse = DalObj.PowerRequest();
            nonWeightPowerConsumption = electricityUse[0];
            lightWeightPowerConsumption = electricityUse[1];
            mediumWeightPowerConsumption = electricityUse[2];
            heavyWeightPowerConsumption = electricityUse[3];
            DroneLoadingRate = electricityUse[4];

            DronesListBL = ConvertToBL.ConvertToDroneArrayBL(DalObj.GetDroneList()).ToList();

            foreach (BO.Drone drone in DronesListBL)
            {
                List<DO.Parcel> arr = DalObj.GetParcelList().ToList();
                if (!arr.Any(parcel => parcel.DroneId == drone.Id))
                {
                    if (rnd.Next(0, 2) == 0)
                    {
                        drone.DroneStatus = BO.Enum.DroneStatusesBL.empty;
                        if (DalObj.GetParcelList().ToList().Any(parcel => parcel.Delivered != null))
                        {
                            List<DO.Parcel> parcelsThatDelivered = DalObj.GetParcelList().ToList().FindAll(parcel => parcel.Delivered != null);
                            DO.Customer randomCustomer = DalObj.GetCustomerByID(parcelsThatDelivered[rnd.Next(0, parcelsThatDelivered.Count)].TargetId);
                            drone.CurrentPosition = new Position(randomCustomer.Longitude, randomCustomer.Latitude);
                            drone.BatteryStatus = rnd.Next((int)(drone.CurrentPosition.CalculateDistanceFor(findClosestStation(drone.CurrentPosition).Position) * nonWeightPowerConsumption), 100);
                        }
                        else
                        {
                            List<DO.Station> stations = DalObj.GetStationList().ToList();
                            int randomIndex = rnd.Next(0, stations.Count);
                            drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                        }
                    }
                    else
                    {
                        drone.DroneStatus = BO.Enum.DroneStatusesBL.maintenance;
                        drone.BatteryStatus = rnd.Next(0, 21);
                        List<DO.Station> stations = DalObj.GetStationList().ToList();
                        int randomIndex = rnd.Next(0, stations.Count);
                        drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                        DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new DroneInCharge(drone), stations[randomIndex].Id));
                    }
                }
                else
                {
                    drone.DroneStatus = BO.Enum.DroneStatusesBL.Shipping;
                    DO.Parcel parcel = DalObj.GetParcelByDroneId(drone.Id);
                    Position senderPos = new Position(DalObj.GetCustomerByID(parcel.SenderId).Longitude, DalObj.GetCustomerByID(parcel.SenderId).Latitude);
                    Position targetPos = new Position(DalObj.GetCustomerByID(parcel.TargetId).Longitude, DalObj.GetCustomerByID(parcel.TargetId).Latitude);
                    drone.CurrentPosition = parcel.PickUp == null ? findClosestStation(senderPos).Position : senderPos;
                    double distanceToTarget = drone.CurrentPosition.CalculateDistanceFor(targetPos);
                    double PowerOfdistanceFromTargetToStation = targetPos.CalculateDistanceFor(findClosestStation(targetPos).Position) * nonWeightPowerConsumption;
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

        #region Internal auxiliary methods
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static double updateButteryStatus(BO.Drone drone, Position position, int weight, Position targetP = null)
        {
            double distance = targetP == null ? drone.CurrentPosition.CalculateDistanceFor(position) : position.CalculateDistanceFor(targetP);
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
            
            foreach (DO.Station element in DalObj.GetStationList())
            {
                Position stationP = new Position(element.Longitude, element.Latitude);
                if (element.EmptyChargeSlots > 0)
                {
                    if (station.Name == null) { station = element; }
                    else
                    {
                        Position cuurentStationP = new Position(station.Longitude, station.Latitude);
                        if (cuurentStationP.CalculateDistanceFor(current) > stationP.CalculateDistanceFor(current))
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
        #endregion

        public void StartSimulation(IBL BL, int droneID, Action<BO.Drone> droneSimulation, Func<bool> needToStop)
        {
            var simulator = new Simulation(BL, droneID, droneSimulation, needToStop);
        }
    }
}