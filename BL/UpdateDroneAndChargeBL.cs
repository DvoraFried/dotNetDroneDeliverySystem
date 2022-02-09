//using DalObject;
using System.Runtime.CompilerServices;
using BO;
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
using static BO.Exceptions;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        public void SendDroneToCharge(int id)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id))) { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                BO.Drone drone = DronesListBL[droneBLIndex];
                if (drone.DroneStatus != DroneStatusesBL.empty) { throw new DroneIsNotEmptyException(); }
                DO.Station station = new DO.Station();
                foreach (DO.Station element in DalObj.returnStationArray())
                {
                    Position stationP = new Position(element.Longitude, element.Latitude);
                    if (element.EmptyChargeSlots > 0 && updateButteryStatus(drone, stationP, 0) > 0)
                    {
                        if (station.Name == null) { station = element; }
                        else
                        {
                            Position cuurentStationP = new Position(station.Longitude, station.Latitude);
                            if (DistanceBetweenCoordinates.CalculateDistance(cuurentStationP, drone.CurrentPosition) > DistanceBetweenCoordinates.CalculateDistance(stationP, drone.CurrentPosition))
                            {
                                station = element;
                            }
                        }
                    }
                }
                if (station.Name == null) { throw new NoPlaceToChargeException(); }
                drone.BatteryStatus = updateButteryStatus(drone, new Position(station.Longitude, station.Latitude), 0);
                drone.CurrentPosition = new Position(station.Longitude, station.Latitude);
                drone.DroneStatus = DroneStatusesBL.maintenance;
                DronesListBL[droneBLIndex] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DroneInCharge droneC = new DroneInCharge(drone);
                DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new DroneInCharge(drone), station.Id));
                station.DronesInCharging += 1;
                station.EmptyChargeSlots -= 1;
                DalObj.ReplaceStationById(station);
                ActionDroneChanged?.Invoke(drone);
            }
        }
        public void ReleaseDroneFromCharging(int id)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id))) { throw new ObjectDoesntExistsInListException("drone"); }
                BO.Drone drone = DronesListBL.First(d => (d.getIdBL() == id));
                if (drone.DroneStatus != DroneStatusesBL.maintenance) { throw new DroneIsNotInMaintenanceException(id); }
                BO.DroneInCharge droneInCharge = ConvertToBL.convertToDroneInChargeBL(DalObj.returnDroneInCharge(id));
                double timeInCharge = (DateTime.Now - droneInCharge.enterTime).Minutes;
                drone.BatteryStatus = Math.Min(drone.BatteryStatus + (timeInCharge / 60) * DalApi.Config.DroneLoadingRate, 100);
                drone.DroneStatus = DroneStatusesBL.empty;
                DronesListBL[DronesListBL.FindIndex(d => (d.getIdBL() == id))] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DO.Station station = DalObj.returnStationArray().ToList().First(station => station.Latitude == drone.CurrentPosition.Latitude && station.Longitude == drone.CurrentPosition.Longitude);
                station.DronesInCharging -= 1;
                station.EmptyChargeSlots += 1;
                DalObj.ReplaceStationById(station);
                ActionDroneChanged?.Invoke(drone);
            }
        }
    }
}
