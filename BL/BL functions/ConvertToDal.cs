using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class ConvertToDal
    {
        /// <summary>
        /// function convert BO station obj to DO station obj:
        /// </summary>
        /// <param name="stationBl"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static DO.Station ConvertToStationDal(Station stationBl)
        {
            DO.Station stationDal = new DO.Station();
            stationDal.Id = stationBl.Id;
            stationDal.Name = stationBl.NameBL;
            stationDal.EmptyChargeSlots = stationBl.ChargeSlotsBL;
            stationDal.Longitude = stationBl.Position.Longitude;
            stationDal.Latitude = stationBl.Position.Latitude;
            return stationDal;           
        }

        /// <summary>
        /// function convert BO drone obj to DO drone obj
        /// </summary>
        /// <param name="droneBl"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static DO.Drone ConvertToDroneDal(Drone droneBl)
        {
            DO.Drone droneDal = new DO.Drone();
            droneDal.Id = droneBl.Id;
            droneDal.Model = droneBl.ModelBL;
            droneDal.MaxWeight = (WeightCategories)(int)droneBl.MaxWeight;
            return droneDal;
        }

        /// <summary>
        /// function convert BO customer obj to DO customer obj
        /// </summary>
        /// <param name="customerBl"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static DO.Customer ConvertToCustomerDal(Customer customerBl)
        {
            DO.Customer customerDal = new DO.Customer();
            customerDal.Id = customerBl.Id;
            customerDal.Name = customerBl.Name;
            customerDal.Phone = customerBl.Phone;
            customerDal.Longitude = customerBl.Position.Longitude;
            customerDal.Latitude = customerBl.Position.Latitude;
            customerDal.IsActive = customerBl.isActive;
            return customerDal;
        }

        /// <summary>
        /// function convert BO parcel obj to DO parcel obj
        /// </summary>
        /// <param name="parcelBl"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static DO.Parcel ConvertToParcelDal(Parcel parcelBl)
        {
            DO.Parcel parcelDal = new DO.Parcel();
            parcelDal.Id = parcelBl.Id;
            parcelDal.SenderId = parcelBl.Sender.Id;
            parcelDal.TargetId = parcelBl.Target.Id;
            parcelDal.Weight= (WeightCategories)(int)parcelBl.Weight;
            parcelDal.Priority = (Priorities)(int)parcelBl.Priority;
            parcelDal.DroneId = parcelBl.DroneIdBL == null ? -1 : parcelBl.DroneIdBL.Id;
            parcelDal.Requested = parcelBl.RequestedBL;
            parcelDal.Scheduled = parcelBl.ScheduledBL;
            parcelDal.PickUp = parcelBl.PickUpBL;
            parcelDal.Delivered = parcelBl.DeliveredBL;
            parcelDal.IsActive = parcelBl.IsActive;
            return parcelDal;
        }

        /// <summary>
        /// function convert BO drone in charge obj to DO drone in charge obj
        /// </summary>
        /// <param name="droneBl"></param>
        /// <param name="stationIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static DO.DroneCharge ConvertToDroneChargeDal(DroneInCharge droneBl,int stationIdS)
        {
            DO.DroneCharge droneChargeDal = new DO.DroneCharge();
            droneChargeDal.DroneId = droneBl.Id;
            droneChargeDal.StationId = stationIdS;
            droneChargeDal.EnterTime = droneBl.EnterTime;
            return droneChargeDal;
        }
    }
}
