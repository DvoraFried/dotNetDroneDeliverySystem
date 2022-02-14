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
        public void SendDroneToCharge(int id, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!ReturnDroneListWithoutDeletedDrones().Any(d => (d.Id == id))) {
                    throw new ObjectDoesntExistsInListException("drone"); }

                BO.Drone drone = DronesListBL.First(d => (d.Id == id));
                if (drone.DroneStatus != DroneStatusesBL.empty) {
                    throw new DroneIsNotEmptyException(); }

                DO.Station station = ConvertToDal.ConvertToStationDal(findClosestStation(drone.CurrentPosition));
                
                drone.BatteryStatus = updateButteryStatus(drone, new Position(station.Longitude, station.Latitude), 0);
                drone.CurrentPosition = new Position(station.Longitude, station.Latitude);
                drone.DroneStatus = DroneStatusesBL.maintenance;
               
                DronesListBL[DronesListBL.FindIndex(d => d.Id == id)] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new DroneInCharge(drone), station.Id));
                
                station.DronesInCharging += 1;
                station.EmptyChargeSlots -= 1;
                DalObj.ReplaceStationById(station);
                
                if(!simulation)
                ActionDroneChanged?.Invoke(drone);
            }
        }

        public void ReleaseDroneFromCharging(int id, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.Id == id))) { 
                    throw new ObjectDoesntExistsInListException("drone"); }
                
                BO.Drone drone = DronesListBL.First(d => (d.Id == id));
                if (drone.DroneStatus != DroneStatusesBL.maintenance) {
                    throw new DroneIsNotInMaintenanceException(id); }
                
                double timeInCharge = (DateTime.Now - DalObj.returnDroneInCharge(id).enterTime).Minutes;
                drone.BatteryStatus = Math.Min(drone.BatteryStatus + (timeInCharge / 60) * DalApi.Config.DroneLoadingRate, 100);
                drone.DroneStatus = DroneStatusesBL.empty;
                DronesListBL[DronesListBL.FindIndex(d => (d.Id == id))] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
               
                DO.Station station = DalObj.returnStationArray().ToList().First(station => station.Latitude == drone.CurrentPosition.Latitude && station.Longitude == drone.CurrentPosition.Longitude);
                station.DronesInCharging -= 1;
                station.EmptyChargeSlots += 1;
                DalObj.ReplaceStationById(station);
               
                if(!simulation)
                ActionDroneChanged?.Invoke(drone);
            }
        }
    }
}
