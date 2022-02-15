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
        internal class ConvertToBL
        {
            /// <summary>
            /// function converts DO drones ienumerable to BO drones ienumerable:
            /// </summary>
            /// <param name="dronesDal"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static IEnumerable<BO.Drone> ConvertToDroneArrayBL(IEnumerable<DO.Drone> dronesDal)
            {
                lock (DalObj)
                {
                    return (from d in dronesDal
                            select new BO.Drone(DalObj, d.Id, d.Model, (BO.Enum.WeightCategoriesBL)(int)d.MaxWeight, 0, null, 0, d.IsActive));
                }
            }

            /// <summary>
            /// function create BO parcels ienumerable frome DO parcels ienumerable:
            /// </summary>
            /// <param name="parcelsDal"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static IEnumerable<BO.Parcel> ConvertToParcelArrayBL(IEnumerable<DO.Parcel> parcelsDal)
            {
                lock (DalObj)
                {
                    return (from parcel in parcelsDal
                            select ConvertToBL.ConvertToParcelBL(parcel));
                }
            }


            /// <summary>
            /// function create BO parcel obj frome DO parcel obj:
            /// </summary>
            /// <param name="parcelDal"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static BO.Parcel ConvertToParcelBL(DO.Parcel parcelDal)
            {
                lock (DalObj)
                {
                    return new BO.Parcel(DalObj, parcelDal.SenderId, parcelDal.TargetId, (int)parcelDal.Weight, (int)parcelDal.Priority, parcelDal.IsActive, parcelDal.Id, parcelDal.Requested, parcelDal.Scheduled, parcelDal.PickUp, parcelDal.Delivered, parcelDal.DroneId);
                }
            }

            /// <summary>
            /// function create BO custoner obj frome DO customer obj:
            /// </summary>
            /// <param name="customerDal"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static BO.Customer ConvertToCustomrtBL(DO.Customer customerDal)
            {
                lock (DalObj)
                {
                    return new BO.Customer(DalObj, customerDal.Id, customerDal.Name, customerDal.Phone, new Position(customerDal.Longitude, customerDal.Latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.GetParcelList().ToList()), customerDal.IsActive);
                }
            }

            /// <summary>
            /// function create BO station obj frome DO station obj:
            /// </summary>
            /// <param name="stationDAL"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static BO.Station ConvertToStationBL(DO.Station stationDAL)
            {
                return new BO.Station(stationDAL.Id, stationDAL.Name, new Position(stationDAL.Longitude, stationDAL.Latitude), stationDAL.DronesInCharging + stationDAL.EmptyChargeSlots, DronesListBL);
            }

            /// <summary>
            /// function create BO employee obj frome DO employee obj:
            /// </summary>
            /// <param name="idEmployee"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static EmpolyeeBL convertToEmployee(int idEmployee)
            {
                lock (DalObj)
                {
                    Employee employeeDAL = DalObj.GetEmployee(idEmployee);
                    return new EmpolyeeBL(idEmployee, employeeDAL.Name, employeeDAL.Manager);
                }
            }
        }
    }
}