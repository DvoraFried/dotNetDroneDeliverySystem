using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using static BL.BL;
using static BO.Exceptions;

namespace BL
{
    class Simulation
    {
        /// <summary>
        /// the class sinulation gat drone id and relevent actions and a bool function to stop the simulation
        /// while the need to stop condition is fales it updates the drone repeatedly by collecting parcels and sending 
        /// to charg and calls to the updat functions by the action functions
        /// by 
        /// </summary>
        IBL BL;
        IDal Dal = DalApi.DalFactory.GetDal();
        public Simulation(IBL BL,int droneID,Action<Drone> droneSimulation, Func<bool> needToStop)
        {
            int DELAY = 500;
            double SPEED = 1;
            Drone drone = DronesListBL.First(d => d.Id == droneID);
            Parcel parcel = drone.DroneStatus == BO.Enum.DroneStatusesBL.Shipping ? BL.GetParcel(DronesListBL.First(d => d.Id == droneID).delivery.Id) : null;
            this.BL = BL;
            while (!needToStop())
            {
                try
                {
                    switch (drone.DroneStatus)
                    {
                        #region case BO.Enum.DroneStatusesBL.empty
                        case BO.Enum.DroneStatusesBL.empty:
                            try
                            {
                                BL.AssigningPackageToDrone(droneID, true);
                                parcel = BL.GetParcel(DronesListBL.First(d => d.Id == droneID).delivery.Id);
                            }
                            catch
                            {
                                if (drone.BatteryStatus < 100 && drone.DroneStatus == BO.Enum.DroneStatusesBL.empty)
                                {
                                    BL.SendDroneToCharge(droneID, true);
                                    Drone updatDrone = DronesListBL.First(d => d.Id == droneID);
                                    Station station = ConvertToBL.ConvertToStationBL(Dal.GetStationList().First(S => S.Longitude == updatDrone.CurrentPosition.Longitude && S.Latitude == updatDrone.CurrentPosition.Latitude));
                                    makeProsses(drone, station.Position.Latitude, station.Position.Longitude, DELAY, droneSimulation, needToStop);
                                }
                            }
                            break;
                        #endregion

                        # region case BO.Enum.DroneStatusesBL.maintenance
                        case BO.Enum.DroneStatusesBL.maintenance:
                            while (drone.BatteryStatus < 100 && !needToStop())
                            {
                                drone.BatteryStatus += SPEED;
                                droneSimulation(drone);
                                Thread.Sleep(DELAY - 100);
                            }
                            BL.ReleaseDroneFromCharging(droneID, true);
                            break;
                        #endregion

                        # region case BO.Enum.DroneStatusesBL.Shipping
                        case BO.Enum.DroneStatusesBL.Shipping:
                            Parcel parcelInDrone = BL.GetParcel(drone.delivery.Id);
                            DO.Customer target;
                            if (parcelInDrone.PickUpBL != null)
                            {
                                target = Dal.GetCustomerByID(parcel.Target.Id);
                                makeProsses(drone, target.Latitude, target.Longitude, DELAY, droneSimulation, needToStop);
                                BL.DeliveryOfAParcelByDrone(droneID, true);
                            }
                            else
                            {
                                target = Dal.GetCustomerByID(parcel.Sender.Id);
                                makeProsses(drone, target.Latitude, target.Longitude, DELAY, droneSimulation, needToStop);
                                BL.CollectionOfAParcelByDrone(droneID, true);
                            }
                            break;
                            #endregion
                    }
                }
                catch (ThereIsNotEnoughBatteryException e)
                {
                    if (drone.DroneStatus == BO.Enum.DroneStatusesBL.empty)
                        BL.SendDroneToCharge(droneID, true);
                    else drone.BatteryStatus += 5; 
                }
                drone = DronesListBL.First(d => d.Id == droneID);
                droneSimulation(drone);
                Thread.Sleep(DELAY);
            }
        }


        /// <summary>
        /// The function simulates the glider's journey -
        /// takes care of changing the battery values ​​and location
        /// so that they are displayed in real time 
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="targetLatitude"></param>
        /// <param name="targetLongitude"></param>
        /// <param name="DELAY"></param>
        /// <param name="droneSimulation"></param>
        /// <param name="needToStop"></param>
        internal void makeProsses(Drone drone, double targetLatitude, double targetLongitude, int DELAY, Action<Drone> droneSimulation, Func<bool> needToStop)
        {
            while (drone.CurrentPosition.Latitude != targetLatitude && drone.CurrentPosition.Longitude != targetLongitude && !needToStop())
            {
                drone.CurrentPosition.Longitude += drone.CurrentPosition.Longitude > targetLongitude ? -1 : 1;
                drone.CurrentPosition.Latitude += drone.CurrentPosition.Latitude > targetLatitude ? -1 : 1;
                drone.BatteryStatus -= 0.4;
                if(drone.DroneStatus == BO.Enum.DroneStatusesBL.Shipping) {
                    drone.delivery.Distance = drone.CurrentPosition.CalculateDistanceFor(new Position(targetLongitude, targetLatitude));
                }
                droneSimulation(drone);
                Thread.Sleep(DELAY);
            }
        }
    }
}
