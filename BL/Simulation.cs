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
        IBL BL;
        public Simulation(IBL BL,int droneID,Action<Drone> dronedroneSimulation, Action<Parcel> parcelSimulation, Func<bool> needToStop)
        {
            int DELAY = 500;
            double SPEED = 1;
            Drone drone = DronesListBL.First(d => d.getIdBL() == droneID);
            Parcel parcel = drone.DroneStatus != BO.Enum.DroneStatusesBL.empty ? BL.returnParcel(DronesListBL.First(d => d.getIdBL() == droneID).delivery.Id) : null;
            ;
            this.BL = BL;
            while (!needToStop())
            {
                try
                {
                    switch (drone.DroneStatus)
                    {
                        case BO.Enum.DroneStatusesBL.empty:
                            try {
                                BL.AssigningPackageToDrone(droneID, true);
                                parcel = BL.returnParcel(DronesListBL.First(d => d.getIdBL() == droneID).delivery.Id);
                                parcelSimulation(BL.returnParcel(parcel.IdBL));
                            }
                            catch {
                                if (drone.BatteryStatus < 100)
                                    BL.SendDroneToCharge(droneID, true);
                            }
                            break;
                        case BO.Enum.DroneStatusesBL.maintenance:
                            while (drone.BatteryStatus < 100 && !needToStop()) 
                            {
                                drone.BatteryStatus += SPEED;
                                dronedroneSimulation(drone);
                                Thread.Sleep(DELAY - 100);
                            }
                            BL.ReleaseDroneFromCharging(droneID, true);
                            break;
                        case BO.Enum.DroneStatusesBL.Shipping:
                            Parcel parcelInDrone = BL.returnParcel(drone.delivery.Id);
                            if (parcelInDrone.PickUpBL != null)
                                BL.DeliveryOfAParcelByDrone(droneID, true);
                            else
                                BL.CollectionOfAParcelByDrone(droneID, true);
                            parcelSimulation(BL.returnParcel(parcel.IdBL));
                            break;
                    }
                }
                catch (ThereIsNotEnoughBatteryException e) {
                    BL.SendDroneToCharge(droneID, true);
                }
                drone = DronesListBL.First(d => d.getIdBL() == droneID);
                dronedroneSimulation(drone);
                if(parcel != null) parcelSimulation(BL.returnParcel(parcel.IdBL));
                Thread.Sleep(DELAY);
            }
        }
    }
}
