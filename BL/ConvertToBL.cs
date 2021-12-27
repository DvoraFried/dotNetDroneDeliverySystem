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
            public static List<DroneBL> ConvertToDroneArrayBL(List<DroneDAL> droneDalArray)
            {
                List<DroneBL> droneArrayBl = new List<DroneBL>();
                foreach (DroneDAL drone in droneDalArray)
                {
                    droneArrayBl.Add(new DroneBL(DalObj,drone.Id, drone.Model, (EnumBL.WeightCategoriesBL)(int)drone.MaxWeight, 0, null, 0));
                };
                return droneArrayBl;
            }
            public static CustomerBL ConvertToCustomrtBL(CustomerDAL customerDal)
            {
                CustomerBL customerBL = new CustomerBL(DalObj,customerDal.Id, customerDal.Name, customerDal.Phone, new Position(customerDal.Longitude, customerDal.Latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()));
                return customerBL;
            }
            public static ParcelBL ConvertToParcelBL(ParcelDAL parcelDal)
            {
                ParcelBL parcelBL = new ParcelBL(DalObj, parcelDal.SenderId, parcelDal.TargetId, (int)parcelDal.Weight, (int)parcelDal.Priority,parcelDal.isActive, parcelDal.Id, parcelDal.Requested, parcelDal.Scheduled, parcelDal.PickUp, parcelDal.Delivered);
                return parcelBL;
            }
            public static List<ParcelBL> ConvertToParcelArrayBL(List<ParcelDAL> parcelsDal)
            {
                List<ParcelBL> parcelsBl = new List<ParcelBL>();
                foreach (ParcelDAL parcelDal in parcelsDal)
                {
                    parcelsBl.Add(ConvertToBL.ConvertToParcelBL(parcelDal));
                }
                return parcelsBl;
            }
            public static StationBL ConvertToStationBL(StationDAL stationDAL)
            {
                StationBL stationBL = new StationBL(stationDAL.Id, stationDAL.Name, new Position(stationDAL.Longitude, stationDAL.Latitude), stationDAL.DronesInCharging + stationDAL.EmptyChargeSlots, DronesListBL);
                return stationBL;
            }
            public static EmpolyeeBL convertToEmployee(int idE)
            {
                EmployeeDAL employeeDAL = DalObj.returnEmployee(idE);
                return new EmpolyeeBL(idE, employeeDAL.Name, employeeDAL.Manager);
            }
        }
    }
}
