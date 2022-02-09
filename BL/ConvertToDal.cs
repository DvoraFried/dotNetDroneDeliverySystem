using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ConvertToDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DO.Station ConvertToStationDal(Station stationBl)
        {
            DO.Station stationDal = new DO.Station();
            stationDal.Id = stationBl.GetIdBL();
            stationDal.Name = stationBl.NameBL;
            stationDal.EmptyChargeSlots = stationBl.ChargeSlotsBL;
            stationDal.Longitude = stationBl.Position.Longitude;
            stationDal.Latitude = stationBl.Position.Latitude;
            return stationDal;           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DO.Drone ConvertToDroneDal(Drone droneBl)
        {
            DO.Drone droneDal = new DO.Drone();
            droneDal.Id = droneBl.getIdBL();
            droneDal.Model = droneBl.ModelBL;
            droneDal.MaxWeight = (WeightCategories)(int)droneBl.MaxWeight;
            return droneDal;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DO.Customer ConvertToCustomerDal(Customer customerBl)
        {
            DO.Customer customerDal = new DO.Customer();
            customerDal.Id = customerBl.getIdBL();
            customerDal.Name = customerBl.NameBL;
            customerDal.Phone = customerBl.PhoneBL;
            customerDal.Longitude = customerBl.Position.Longitude;
            customerDal.Latitude = customerBl.Position.Latitude;
            customerDal.isActive = customerBl.isActive;
            return customerDal;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DO.Parcel ConvertToParcelDal(Parcel parcelBl)
        {
            DO.Parcel parcelDal = new DO.Parcel();
            parcelDal.Id = parcelBl.IdBL;
            parcelDal.SenderId = parcelBl.Sender.Id;
            parcelDal.TargetId = parcelBl.Target.Id;
            parcelDal.Weight= (WeightCategories)(int)parcelBl.Weight;
            parcelDal.Priority = (Priorities)(int)parcelBl.Priority;
            parcelDal.DroneId = parcelBl.DroneIdBL == null ? -1 : parcelBl.DroneIdBL.Id;
            parcelDal.Requested = parcelBl.RequestedBL;
            parcelDal.Scheduled = parcelBl.ScheduledBL;
            parcelDal.PickUp = parcelBl.PickUpBL;
            parcelDal.Delivered = parcelBl.DeliveredBL;
            parcelDal.isActive = parcelBl.isActive;
            return parcelDal;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DO.DroneCharge ConvertToDroneChargeDal(DroneInCharge droneBl,int stationIdS)
        {
            DO.DroneCharge droneChargeDal = new DO.DroneCharge();
            droneChargeDal.DroneId = droneBl.Id;
            droneChargeDal.StationId = stationIdS;
            droneChargeDal.enterTime = droneBl.enterTime;
            return droneChargeDal;
        }
    }
}
