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
        /// <summary>
        /// function fined the closest station to the drone, update the drone and station data in accordance,
        /// than replace it and create an droneincharge intance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="simulation"></param>
        public void SendDroneToCharge(int id, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!getDroneListWithoutDeletedDrones().Any(d => (d.Id == id))) {
                    throw new ObjectDoesntExistsInListException("drone"); }

                BO.Drone drone = DronesListBL.First(d => (d.Id == id));
                if (drone.DroneStatus != DroneStatusesBL.empty) {
                    throw new DroneIsNotEmptyException(); }

                BO.Station station = (findClosestStation(drone.CurrentPosition));
                
                drone.BatteryStatus = updateButteryStatus(drone, station.Position, 0);
                drone.CurrentPosition = station.Position;
                drone.DroneStatus = DroneStatusesBL.maintenance;
               
                DronesListBL[DronesListBL.FindIndex(d => d.Id == id)] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new DroneInCharge(drone), station.Id));

                DO.Station dalStation = ConvertToDal.ConvertToStationDal(station);
                dalStation.DronesInCharging += 1;
                dalStation.EmptyChargeSlots -= 1;
                DalObj.ReplaceStationById(dalStation);

                if (!simulation)
                {
                    ActionDroneChanged?.Invoke(drone);
                    if(ActionUpdateList != null) ActionUpdateList?.Invoke(true);
                }
            }
        }

        /// <summary>
        /// the function releae drone from charge by calculatin how much battery to add and change the station slots to be suitable 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="simulation"></param>
        public void ReleaseDroneFromCharging(int id, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.Id == id))) { 
                    throw new ObjectDoesntExistsInListException("drone"); }
                
                BO.Drone drone = DronesListBL.First(d => (d.Id == id));
                if (drone.DroneStatus != DroneStatusesBL.maintenance) {
                    throw new DroneIsNotInMaintenanceException(id); }
                
                double timeInCharge = (DateTime.Now - DalObj.GetDroneInChargeByID(id).EnterTime).Minutes;
                drone.BatteryStatus = Math.Min(drone.BatteryStatus + (timeInCharge / 60) * DroneLoadingRate, 100);
                drone.DroneStatus = DroneStatusesBL.empty;
                DronesListBL[DronesListBL.FindIndex(d => (d.Id == id))] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
               
                DO.Station station = DalObj.GetStationList().ToList().First(station => station.Latitude == drone.CurrentPosition.Latitude && station.Longitude == drone.CurrentPosition.Longitude);
                station.DronesInCharging -= 1;
                station.EmptyChargeSlots += 1;
                DalObj.ReplaceStationById(station);

                if (!simulation) {
                    ActionDroneChanged?.Invoke(drone);
                    if(ActionUpdateList != null) ActionUpdateList?.Invoke(true); }
                }
        }
    }
}
