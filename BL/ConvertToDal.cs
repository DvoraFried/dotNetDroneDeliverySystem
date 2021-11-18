using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
        public class ConvertToDal
        {
            public static StationDAL ConvertToStationDal(StationBL stationBl)
            {
            StationDAL stationDal = new StationDAL();
            stationDal.Id = stationBl.getIdBL();
            stationDal.Name = stationBl.NameBL;
            stationDal.ChargeSlots = stationBl.ChargeSlotsBL;
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
            customerDal.Longitude = customerBl.position.Longitude;
            customerDal.Latitude = customerBl.position.Latitude;
            return customerDal;
           }
            public static ParcelDAL ConvertToParcelDal(ParcelBL parcelBl)
            {
            ParcelDAL parcelDal = new ParcelDAL();
            parcelDal.Id = parcelBl.getIdBL();
            parcelDal.SenderId = parcelBl.getSenderIdBL();
            parcelDal.TargetId = parcelBl.getTargetIdBL();
            parcelDal.Weight= (WeightCategories)(int)parcelBl.Weight;
            parcelDal.Priority = (Priorities)(int)parcelBl.Priority;
            parcelDal.DroneId = parcelBl.DroneIdBL.getIdBL();
            parcelDal.Requested = parcelBl.RequestedBL;
            parcelDal.Scheduled = parcelBl.ScheduledBL;
            parcelDal.PickUp = parcelBl.PickUpBL;
            parcelDal.Delivered = parcelBl.DeliveredBL;
            return parcelDal;
            }
    }
}
