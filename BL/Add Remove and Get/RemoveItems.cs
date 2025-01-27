﻿using System.Runtime.CompilerServices;
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
        /// <summary>
        /// function 'remove' customer by id by changing the "isActiv" field, 
        /// excaption will be thrown if ther customer doesnt exist or if there is a parcel on the way to it,
        /// or a parcel he sent but not yet delivered.
        /// </summary>
        /// <param name="idCustomer"></param>
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

        /// <summary>
        /// function delete parcel by changing its "isactive" field.
        /// exception will be thrown if the parcel was already Scheduled.
        /// </summary>
        /// <param name="parcel"></param>
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

        /// <summary>
        /// function delete drone by id by changing its isactive field.
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int droneId)
        {
            lock (DalObj)
            {
                BO.Drone drone = DronesListBL.First(d => d.Id == droneId); 
                if (drone.DroneStatus == BO.Enum.DroneStatusesBL.maintenance) 
                    ReleaseDroneFromCharging(droneId); // so it wont take a place...
                drone.isActive = false;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL[DronesListBL.FindIndex(d => d.Id == droneId)] = drone;
                if(ActionUpdateList != null) ActionUpdateList?.Invoke(true);
            }
        }
    }
}
