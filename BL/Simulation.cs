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
        public Simulation(IBL BL,int droneID,Action<Drone> droneSimulation,/* Action<Parcel,bool> parcelSimulation,*/ Func<bool> needToStop)
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
                        case BO.Enum.DroneStatusesBL.empty:
                            try
                            {
                                BL.AssigningPackageToDrone(droneID, true);
                                parcel = BL.GetParcel(DronesListBL.First(d => d.Id == droneID).delivery.Id);
                                //parcelSimulation(BL.GetParcel(parcel.Id), true);
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
                        case BO.Enum.DroneStatusesBL.maintenance:
                            while (drone.BatteryStatus < 100 && !needToStop())
                            {
                                drone.BatteryStatus += SPEED;
                                droneSimulation(drone);
                                Thread.Sleep(DELAY - 100);
                            }
                            BL.ReleaseDroneFromCharging(droneID, true);
                            break;
                        case BO.Enum.DroneStatusesBL.Shipping:
                            Parcel parcelInDrone = BL.GetParcel(drone.delivery.Id);
                            DO.Customer target;
                            if (parcelInDrone.PickUpBL != null)
                            {
                                BL.DeliveryOfAParcelByDrone(droneID, true);
                                target = Dal.GetCustomerByID(parcel.Target.Id);
                                makeProsses(drone, target.Latitude, target.Longitude, DELAY, droneSimulation, needToStop);
                            }
                            else
                            {
                                BL.CollectionOfAParcelByDrone(droneID, true);
                            }
                           // parcelSimulation(BL.GetParcel(parcel.Id), true);
                            break;

                    }
                }
                catch (ThereIsNotEnoughBatteryException e)
                {
                    if (drone.DroneStatus == BO.Enum.DroneStatusesBL.empty)
                        BL.SendDroneToCharge(droneID, true);
                }
                drone = DronesListBL.First(d => d.Id == droneID);
                droneSimulation(drone);
               // if(parcel != null) parcelSimulation(BL.GetParcel(parcel.Id),true);
                Thread.Sleep(DELAY);
            }
        }
        internal void makeProsses(Drone drone, double Latitude, double Longitude, int DELAY, Action<Drone> droneSimulation, Func<bool> needToStop)
        {
            while (drone.CurrentPosition.Latitude != Latitude && drone.CurrentPosition.Longitude != Longitude && !needToStop())
            {
                drone.CurrentPosition.Longitude += drone.CurrentPosition.Longitude > Longitude ? -1 : 1;
                drone.CurrentPosition.Latitude += drone.CurrentPosition.Latitude > Latitude ? -1 : 1;
                drone.BatteryStatus -= 0.5;
                if(drone.DroneStatus == BO.Enum.DroneStatusesBL.Shipping) {
                    drone.delivery.Distance = BO.DistanceBetweenCoordinates.CalculateDistance(drone.CurrentPosition, new Position(Longitude, Latitude));
                }
                droneSimulation(drone);
                Thread.Sleep(DELAY);
            }
        }
    }
}
