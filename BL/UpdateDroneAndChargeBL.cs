﻿using DalObject;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Exceptions;

namespace BL
{
    public partial class BL : IBL.IBL
    {
            public void SendDroneToCharge(int id)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id))) { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                DroneBL drone = DronesListBL[droneBLIndex];
                if (drone.DroneStatus!= DroneStatusesBL.empty) { throw new DroneIsNotInMaintenanceException(id); }             
                StationDAL station = new StationDAL();
                foreach (StationDAL element in DalObj.returnStationArray())
                {
                    Position stationP = new Position(element.Longitude, element.Latitude);
                    if (element.EmptyChargeSlots > 0 && updateButteryStatus(drone, stationP, 0) > 0)
                    {
                        if (station.Name == null) { station = element;}
                        else
                        {
                            Position cuurentStationP = new Position(station.Longitude, station.Latitude);
                            if (DistanceBetweenCoordinates.CalculateDistance(cuurentStationP, drone.CurrentPosition)>DistanceBetweenCoordinates.CalculateDistance(stationP, drone.CurrentPosition))
                            {
                                station = element;
                            }
                        }
                    }
                }
                if(station.Name == null) { throw new NoPlaceToChargeException(); }
                drone.BatteryStatus = updateButteryStatus(drone, new Position(station.Longitude, station.Latitude), 0);
                drone.CurrentPosition = new Position(station.Longitude, station.Latitude);
                drone.DroneStatus = DroneStatusesBL.maintenance;
                DronesListBL[droneBLIndex] = drone;
                DataSource.MyDrones[droneBLIndex] = ConvertToDal.ConvertToDroneDal(drone);
                //missing the update of the station
                DroneInChargeBL droneC = new DroneInChargeBL(drone);
                DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(droneC, station.Id));
            }
            public void ReleaseDroneFromCharging(int id,double timeInCharge)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id))) { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                DroneBL drone = DronesListBL[droneBLIndex];
                if (drone.DroneStatus != DroneStatusesBL.maintenance) { throw new DroneIsNotInMaintenanceException(id); }
                drone.BatteryStatus = (int)(drone.BatteryStatus+ drone.BatteryStatus * DataSource.Config.DroneLoadingRate);
                drone.DroneStatus = DroneStatusesBL.empty;
                DronesListBL[droneBLIndex] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DataSource.MyDrones[droneBLIndex] = ConvertToDal.ConvertToDroneDal(drone);
                //up up the stations charginslot in 1
                DalObj.DeleteObjFromDroneCharges(drone.getIdBL());
            }
    }
}
