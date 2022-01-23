using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL 
    {
        public class ConvertToBL
        {
            public static List<DroneBL> ConvertToDroneArrayBL(List<Drone> droneDalArray)
            {
                List<DroneBL> droneArrayBl = new List<DroneBL>();
                foreach (Drone drone in droneDalArray)
                {
                    droneArrayBl.Add(new DroneBL(DalObj,drone.Id, drone.Model, (EnumBL.WeightCategoriesBL)(int)drone.MaxWeight, 0, null, 0));
                };
                return droneArrayBl;
            }
            public static CustomerBL ConvertToCustomrtBL(Customer customerDal)
            {
                CustomerBL customerBL = new CustomerBL(DalObj,customerDal.Id, customerDal.Name, customerDal.Phone, new Position(customerDal.Longitude, customerDal.Latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()), customerDal.isActive);
                return customerBL;
            }
            public static ParcelBL ConvertToParcelBL(Parcel parcelDal)
            {
                ParcelBL parcelBL = new ParcelBL(DalObj, parcelDal.SenderId, parcelDal.TargetId, (int)parcelDal.Weight, (int)parcelDal.Priority,parcelDal.isActive, parcelDal.Id, parcelDal.Requested, parcelDal.Scheduled, parcelDal.PickUp, parcelDal.Delivered);
                return parcelBL;
            }
            public static List<ParcelBL> ConvertToParcelArrayBL(List<Parcel> parcelsDal)
            {
                List<ParcelBL> parcelsBl = new List<ParcelBL>();
                foreach (Parcel parcelDal in parcelsDal)
                {
                    parcelsBl.Add(ConvertToBL.ConvertToParcelBL(parcelDal));
                }
                return parcelsBl;
            }
            public static StationBL ConvertToStationBL(Station stationDAL)
            {
                StationBL stationBL = new StationBL(stationDAL.Id, stationDAL.Name, new Position(stationDAL.Longitude, stationDAL.Latitude), stationDAL.DronesInCharging + stationDAL.EmptyChargeSlots, DronesListBL);
                return stationBL;
            }
            public static EmpolyeeBL convertToEmployee(int idE)
            {
                Employee employeeDAL = DalObj.returnEmployee(idE);
                return new EmpolyeeBL(idE, employeeDAL.Name, employeeDAL.Manager);
            }
            public static DroneInChargeBL convertToDroneInChargeBL(DroneCharge droneChargeDAL)
            {
                return new DroneInChargeBL(droneChargeDAL.DroneId, droneChargeDAL.enterTime);
            }
        }
    }
}
