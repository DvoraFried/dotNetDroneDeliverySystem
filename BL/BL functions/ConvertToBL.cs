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
            /// function converts do drone array to bo drona array
            /// </summary>
            /// <param name="droneDalArray"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static IEnumerable<BO.Drone> ConvertToDroneArrayBL(IEnumerable<DO.Drone> droneDalArray)
            {
                lock (DalObj)
                {
                    return (from d in droneDalArray
                            select new BO.Drone(DalObj, d.Id, d.Model, (BO.Enum.WeightCategoriesBL)(int)d.MaxWeight, 0, null, 0, d.IsActive));
                }
            }

            /// <summary>
            /// function create bo custoner obj frome do customer obj
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
            /// function create bo parcel obj frome do parcel obj
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
            /// function create bo custoners array  frome do customers array
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
            /// function create bo station obj frome do station obj
            /// </summary>
            /// <param name="stationDAL"></param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static BO.Station ConvertToStationBL(DO.Station stationDAL)
            {
                return new BO.Station(stationDAL.Id, stationDAL.Name, new Position(stationDAL.Longitude, stationDAL.Latitude), stationDAL.DronesInCharging + stationDAL.EmptyChargeSlots, DronesListBL);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            internal static EmpolyeeBL convertToEmployee(int idE)
            {
                lock (DalObj)
                {
                    Employee employeeDAL = DalObj.GetEmployee(idE);
                    return new EmpolyeeBL(idE, employeeDAL.Name, employeeDAL.Manager);
                }
            }
        }
    }
}