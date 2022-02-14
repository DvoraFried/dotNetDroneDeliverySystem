using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;
using DO;
using BO;
using BlApi;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomerById(int idCustomer)
        {
            lock (DalObj)
            {
                if (!DalObj.GetCustomerList().ToList().Any(c => c.Id == idCustomer)) {
                    throw new ObjectDoesntExistsInListException("customer"); }

                foreach (DO.Parcel parcel in DalObj.GetParcelList())
                {
                    if ((parcel.TargetId == idCustomer || parcel.SenderId == idCustomer) && (parcel.Delivered == null))
                    { throw new ThereAreParcelForTheCustomer(parcel.TargetId); }
                }

                DO.Customer customer = DalObj.GetCustomerList().First(c => c.Id == idCustomer);
                customer.IsActive = false;
                DalObj.ReplaceCustomerById(customer);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(BO.Parcel parcel)
        {
            lock (DalObj)
            {
                if (parcel.ScheduledBL == null)
                {
                    parcel.IsActive = false;
                    DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
                }
                else {
                    throw new ParcelAlreadyScheduled(); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            lock (DalObj)
            {
                BO.Drone drone = DronesListBL.First(d => d.Id == id);
                drone.isActive = false;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL[DronesListBL.FindIndex(d => d.Id == id)] = drone;
            }
        }

        public void StartSimulation(IBL BL, int droneID, Action<BO.Drone> droneSimulation, Action<BO.Parcel,bool> parcelSimulation, Func<bool> needToStop)
        {
            var simulator = new Simulation(BL, droneID, droneSimulation, parcelSimulation, needToStop);
        }
    }
}
