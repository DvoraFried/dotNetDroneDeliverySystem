using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlApi;
using BO;
using static BL.BL;
using static BO.Exceptions;

namespace BL
{
    class Simulation
    {
        IBL BL;
        public Simulation(IBL BL,int droneID,Action<Drone> dronedroneSimulation, Func<bool> needToStop)
        {
            int DELAY = 500;
            double SPEED = 1;
            Drone drone = DronesListBL.First(d => d.getIdBL() == droneID);
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
                            }
                            catch {
                                if (drone.BatteryStatus < 100)
                                    BL.SendDroneToCharge(droneID, true);
                            }
                            break;
                        case BO.Enum.DroneStatusesBL.maintenance:
                            while (drone.BatteryStatus <= 100) 
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
                            break;
                    }
                }
                catch (ThereIsNotEnoughBatteryException e)
                {
                    BL.SendDroneToCharge(droneID, true);
                }
                drone = DronesListBL.First(d => d.getIdBL() == droneID);
                dronedroneSimulation(drone);
                Thread.Sleep(DELAY);
            }
        }
    }
}
