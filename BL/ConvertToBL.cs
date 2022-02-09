using System.Runtime.CompilerServices;
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
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static List<BO.Drone> ConvertToDroneArrayBL(List<DO.Drone> droneDalArray)
            {
                lock (DalObj)
                {
                    List<BO.Drone> droneArrayBl = new List<BO.Drone>();
                    foreach (DO.Drone drone in droneDalArray)
                    {
                        droneArrayBl.Add((BO.Drone)new BO.Drone(DalObj, drone.Id, drone.Model, (BO.Enum.WeightCategoriesBL)(int)drone.MaxWeight, 0, null, 0, drone.isActive));
                    };
                    return droneArrayBl;
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static BO.Customer ConvertToCustomrtBL(DO.Customer customerDal)
            {
                lock (DalObj)
                {
                    BO.Customer customerBL = new BO.Customer(DalObj, customerDal.Id, customerDal.Name, customerDal.Phone, new Position(customerDal.Longitude, customerDal.Latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()), customerDal.isActive);
                    return customerBL;
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static BO.Drone ConvertToDroneBL(DO.Drone droneDal)
            {
                lock (DalObj)
                {
                    BO.Drone droneBl = new BO.Drone(DalObj, droneDal.Id, droneDal.Model, (BO.Enum.WeightCategoriesBL)(int)droneDal.MaxWeight, 0, null, 0, droneDal.isActive);
                    return droneBl;
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static BO.Parcel ConvertToParcelBL(DO.Parcel parcelDal)
            {
                lock (DalObj)
                {
                    BO.Parcel parcelBL = new BO.Parcel(DalObj, parcelDal.SenderId, parcelDal.TargetId, (int)parcelDal.Weight, (int)parcelDal.Priority, parcelDal.isActive, parcelDal.Id, parcelDal.Requested, parcelDal.Scheduled, parcelDal.PickUp, parcelDal.Delivered, parcelDal.DroneId);
                    return parcelBL;
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]

            public static IEnumerable<BO.Parcel> ConvertToParcelArrayBL(IEnumerable<DO.Parcel> parcelsDal)
            {
                lock (DalObj)
                {
                    return (IEnumerable<BO.Parcel>)(from parcel in parcelsDal
                                                    select new { Parcel = ConvertToBL.ConvertToParcelBL(parcel) });
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static BO.Station ConvertToStationBL(DO.Station stationDAL)
            {
                BO.Station stationBL = new BO.Station(stationDAL.Id, stationDAL.Name, new Position(stationDAL.Longitude, stationDAL.Latitude), stationDAL.DronesInCharging + stationDAL.EmptyChargeSlots, DronesListBL);
                return stationBL;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static EmpolyeeBL convertToEmployee(int idE)
            {
                lock (DalObj)
                {
                    Employee employeeDAL = DalObj.returnEmployee(idE);
                    return new EmpolyeeBL(idE, employeeDAL.Name, employeeDAL.Manager);
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public static DroneInCharge convertToDroneInChargeBL(DO.DroneCharge droneChargeDAL)
            {
                return new DroneInCharge(droneChargeDAL.DroneId, droneChargeDAL.enterTime);
            }
        }
    }
}