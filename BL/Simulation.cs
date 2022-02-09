using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlApi;
using BO;
using static BL.BL;

namespace BL
{
    class Simulation
    {
        IBL BL;
        public Simulation(IBL BL,int droneID,Action<Drone,int> dronedroneSimulation, Func<bool> needToStop)
        {
            Drone drone = DronesListBL.First(d => d.getIdBL() == droneID);
            this.BL = BL;
            while (!needToStop())
            {
               switch (drone.DroneStatus)
                {
                    case BO.Enum.DroneStatusesBL.empty:
                        try {
                            BL.AssigningPackageToDrone(droneID);
                        }
                        catch {
                            try {
                                BL.SendDroneToCharge(droneID);
                            }
                            catch
                            {
                                ///???
                            }
                         }
                        break;
                    case BO.Enum.DroneStatusesBL.maintenance:
                        if(drone.BatteryStatus == 100) { BL.ReleaseDroneFromCharging(droneID); }
                        break;
                    case BO.Enum.DroneStatusesBL.Shipping:
                        Parcel parcelInDrone = BL.returnParcel(drone.delivery.Id);
                        if (parcelInDrone.PickUpBL != null)
                            BL.DeliveryOfAParcelByDrone(droneID); 
                        else
                            BL.CollectionOfAParcelByDrone(droneID); 
                        break;
               }
                //here we need to add the logic of the drone and threadsleep(delay) after every step
            }
        }
    }
}
