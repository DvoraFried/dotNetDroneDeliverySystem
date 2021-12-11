﻿using DalObject;
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
            
            DronesListBL = ConvertToBL.ConvertToDroneArrayBL(DalObj.returnDroneArray().ToList());
            
            foreach(DroneBL drone in DronesListBL)
            {
                if(DalObj.returnParcelArray().Any(parcel => parcel.DroneId == drone.getIdBL()))
                {
                    if (rnd.Next(0, 2) == 0)
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.empty;
                        List<ParcelDAL> parcelsThatDelivered = DalObj.returnParcelArray().ToList().FindAll(parcel => parcel.Delivered != new DateTime());
                        CustomerDAL randomCustomer = DalObj.returnCustomer(parcelsThatDelivered[rnd.Next(0, parcelsThatDelivered.Count)].TargetId);
                        drone.CurrentPosition = new Position(randomCustomer.Longitude, randomCustomer.Latitude);
                        drone.BatteryStatus = rnd.Next((int)(DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition, findClosestStation(drone.CurrentPosition)) * nonWeightPowerConsumption), 100);
                    }
                    else
                    {
                        drone.DroneStatus = EnumBL.DroneStatusesBL.maintenance;
                        drone.BatteryStatus = rnd.Next(0, 21);
                        List<StationDAL> stations = (List<StationDAL>)DalObj.returnStationArray();
                        int randomIndex = rnd.Next(0, stations.Count);
                        drone.CurrentPosition = new Position(stations[randomIndex].Longitude, stations[randomIndex].Latitude);
                    }
                }
                else
                {
                    drone.DroneStatus = EnumBL.DroneStatusesBL.Shipping;
                    ParcelDAL parcel = DalObj.returnParcelByDroneId(drone.getIdBL());
                    Position senderPos = new Position(DalObj.returnCustomer(parcel.SenderId).Longitude, DalObj.returnCustomer(parcel.SenderId).Latitude);
                    Position targetPos = new Position(DalObj.returnCustomer(parcel.TargetId).Longitude, DalObj.returnCustomer(parcel.TargetId).Latitude);
                    drone.CurrentPosition = parcel.PickUp == new DateTime() ? findClosestStation(senderPos) : senderPos;
                    double distanceToTarget = DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition,targetPos);
                    double PowerOfdistanceFromTargetToStation = DistanceBetweenCoordinates.CalculateDistance(targetPos, findClosestStation(targetPos))*nonWeightPowerConsumption;
                    drone.BatteryStatus = (int)parcel.Weight == 1 ? rnd.Next((int)(distanceToTarget*lightWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100) :
                                          (int)parcel.Weight == 2 ? rnd.Next((int)(distanceToTarget * mediumWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100) :
                                          rnd.Next((int)(distanceToTarget * heavyWeightPowerConsumption + PowerOfdistanceFromTargetToStation), 100);
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