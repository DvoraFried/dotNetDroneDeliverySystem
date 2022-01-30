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
            public static List<BO.Drone> ConvertToDroneArrayBL(List<DO.Drone> droneDalArray)
            {
                List<BO.Drone> droneArrayBl = new List<BO.Drone>();
                foreach (DO.Drone drone in droneDalArray)
                {
                    droneArrayBl.Add((BO.Drone)new BO.Drone(DalObj, drone.Id, drone.Model, (BO.Enum.WeightCategoriesBL)(int)drone.MaxWeight, 0, null, 0, drone.isActive));
                };
                return droneArrayBl;
            }
            public static BO.Customer ConvertToCustomrtBL(DO.Customer customerDal)
            {
                BO.Customer customerBL = new BO.Customer(DalObj,customerDal.Id, customerDal.Name, customerDal.Phone, new Position(customerDal.Longitude, customerDal.Latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()), customerDal.isActive);
                return customerBL;
            }
            public static BO.Parcel ConvertToParcelBL(DO.Parcel parcelDal)
            {
                BO.Parcel parcelBL = new BO.Parcel(DalObj, parcelDal.SenderId, parcelDal.TargetId, (int)parcelDal.Weight, (int)parcelDal.Priority,parcelDal.isActive, parcelDal.Id, parcelDal.Requested, parcelDal.Scheduled, parcelDal.PickUp, parcelDal.Delivered);
                return parcelBL;
            }
            public static List<BO.Parcel> ConvertToParcelArrayBL(List<DO.Parcel> parcelsDal)
            {
                List<BO.Parcel> parcelsBl = new List<BO.Parcel>();
                foreach (DO.Parcel parcelDal in parcelsDal)
                {
                    parcelsBl.Add(ConvertToBL.ConvertToParcelBL(parcelDal));
                }
                return parcelsBl;
            }
            public static BO.Station ConvertToStationBL(DO.Station stationDAL)
            {
                BO.Station stationBL = new BO.Station(stationDAL.Id, stationDAL.Name, new Position(stationDAL.Longitude, stationDAL.Latitude), stationDAL.DronesInCharging + stationDAL.EmptyChargeSlots, DronesListBL);
                return stationBL;
            }
            public static EmpolyeeBL convertToEmployee(int idE)
            {
                Employee employeeDAL = DalObj.returnEmployee(idE);
                return new EmpolyeeBL(idE, employeeDAL.Name, employeeDAL.Manager);
            }
            public static DroneInCharge convertToDroneInChargeBL(DO.DroneCharge droneChargeDAL)
            {
                return new DroneInCharge(droneChargeDAL.DroneId, droneChargeDAL.enterTime);
            }
        }
    }
}
