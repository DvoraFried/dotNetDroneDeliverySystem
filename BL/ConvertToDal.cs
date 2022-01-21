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
        public static StationDAL ConvertToStationDal(StationBL stationBl)
        {
            StationDAL stationDal = new StationDAL();
            stationDal.Id = stationBl.GetIdBL();
            stationDal.Name = stationBl.NameBL;
            stationDal.EmptyChargeSlots = stationBl.ChargeSlotsBL;
            stationDal.Longitude = stationBl.Position.Longitude;
            stationDal.Latitude = stationBl.Position.Latitude;
            return stationDal;           
        }
        public static DroneDAL ConvertToDroneDal(DroneBL droneBl)
        {
            DroneDAL droneDal = new DroneDAL();
            droneDal.Id = droneBl.getIdBL();
            droneDal.Model = droneBl.ModelBL;
            droneDal.MaxWeight = (WeightCategories)(int)droneBl.MaxWeight;
            return droneDal;
        }
        public static CustomerDAL ConvertToCustomerDal(CustomerBL customerBl)
        {
            CustomerDAL customerDal = new CustomerDAL();
            customerDal.Id = customerBl.getIdBL();
            customerDal.Name = customerBl.NameBL;
            customerDal.Phone = customerBl.PhoneBL;
            customerDal.Longitude = customerBl.Position.Longitude;
            customerDal.Latitude = customerBl.Position.Latitude;
            customerDal.isActive = customerBl.isActive;
            return customerDal;
        }
        public static ParcelDAL ConvertToParcelDal(ParcelBL parcelBl)
        {
            ParcelDAL parcelDal = new ParcelDAL();
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

        public static DroneChargeDAL ConvertToDroneChargeDal(DroneInChargeBL droneBl,int stationIdS)
        {
            DroneChargeDAL droneChargeDal = new DroneChargeDAL();
            droneChargeDal.DroneId = droneBl.Id;
            droneChargeDal.StationId = stationIdS;
            droneChargeDal.enterTime = droneBl.enterTime;
            return droneChargeDal;
        }
    }
}
