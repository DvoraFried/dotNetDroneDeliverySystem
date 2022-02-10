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
                if (!DalObj.returnCustomerArray().ToList().Any(c => c.Id == idCustomer)) { throw new ObjectDoesntExistsInListException("customer"); }
                foreach (DO.Parcel parcel in DalObj.returnParcelArray().ToList())
                {
                    if ((parcel.TargetId == idCustomer || parcel.SenderId == idCustomer) && (parcel.Delivered == null))
                    { throw new ThereAreParcelForTheCustomer(parcel.TargetId); }
                }
                DO.Customer customer = DalObj.returnCustomerArray().ToList().First(c => c.Id == idCustomer);
                customer.isActive = false;
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
                    parcel.isActive = false;
                    DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
                }
                else { throw new ParcelAlreadyScheduled(); }
            }
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
       /* public void DeleteDrone(BO.Drone drone)
        {
            lock (DalObj)
            {
                drone.isActive = false;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
            }
        }*/

        public void StartSimulation(IBL BL, int droneID, Action<BO.Drone> droneSimulation, Func<bool> needToStop)
        {
            var simulator = new Simulation(BL, droneID, droneSimulation, needToStop);
        }
    }
}
