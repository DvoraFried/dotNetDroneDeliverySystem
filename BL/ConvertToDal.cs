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
        public static Station ConvertToStationDal(StationBL stationBl)
        {
            Station stationDal = new Station();
            stationDal.Id = stationBl.GetIdBL();
            stationDal.Name = stationBl.NameBL;
            stationDal.EmptyChargeSlots = stationBl.ChargeSlotsBL;
            stationDal.Longitude = stationBl.Position.Longitude;
            stationDal.Latitude = stationBl.Position.Latitude;
            return stationDal;           
        }
        public static Drone ConvertToDroneDal(DroneBL droneBl)
        {
            Drone droneDal = new Drone();
            droneDal.Id = droneBl.getIdBL();
            droneDal.Model = droneBl.ModelBL;
            droneDal.MaxWeight = (WeightCategories)(int)droneBl.MaxWeight;
            return droneDal;
        }
        public static Customer ConvertToCustomerDal(CustomerBL customerBl)
        {
            Customer customerDal = new Customer();
            customerDal.Id = customerBl.getIdBL();
            customerDal.Name = customerBl.NameBL;
            customerDal.Phone = customerBl.PhoneBL;
            customerDal.Longitude = customerBl.Position.Longitude;
            customerDal.Latitude = customerBl.Position.Latitude;
            customerDal.isActive = customerBl.isActive;
            return customerDal;
        }
        public static Parcel ConvertToParcelDal(ParcelBL parcelBl)
        {
            Parcel parcelDal = new Parcel();
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

        public static DroneCharge ConvertToDroneChargeDal(DroneInChargeBL droneBl,int stationIdS)
        {
            DroneCharge droneChargeDal = new DroneCharge();
            droneChargeDal.DroneId = droneBl.Id;
            droneChargeDal.StationId = stationIdS;
            droneChargeDal.enterTime = droneBl.enterTime;
            return droneChargeDal;
        }
    }
}
