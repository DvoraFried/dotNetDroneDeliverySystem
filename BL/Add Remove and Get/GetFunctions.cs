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
    public partial class BL : BlApi.IBL
    {
        #region GET LISTS FUNCTIONS
        /// <summary>
        /// the function gets from PL two filters,
        /// and returns drones list filter by the parameters:
        /// </summary>
        /// <param name="droneStatus">by default: -1 (Defined in PL)</param>
        /// <param name="droneMaxWeight">by default: -1 (Defined in PL)</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> GetDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight)
        {
            // Pulls out all active drones or in shipping:
            List<BO.Drone> drones = (from d in DronesListBL where d.isActive || d.delivery != null select d).ToList();
            
            if (droneStatus != -1) {
                if (droneMaxWeight != -1) {
                    return from D in drones where ((int)D.DroneStatus == droneStatus && (int)D.MaxWeight == droneMaxWeight) select D;
                }
                else {
                    return from D in drones where ((int)D.DroneStatus == droneStatus) select D;
                }
            }
            else if (droneMaxWeight != -1) {
                return  (from D in drones where ((int)D.MaxWeight == droneMaxWeight) select D);
            }
            return drones;
        }

        /// <summary>
        /// the function returns the drones list grouped by status order.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> GetDronesByStatusOrder()
        {
            lock (DalObj)
            {
                return from d in DronesListBL.OrderBy(d => d.DroneStatus)
                       where d.isActive || d.delivery != null
                       select d;
            }
        }

        /// <summary>
        /// the function returns the list of parcel:
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelList()
        {
            lock (DalObj)
            {
                return (from P in DalObj.GetParcelList()
                        where P.IsActive
                        select new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(P)));
            }
        }

        /// <summary>
        /// internal function that returns drones list (without deleted drones) - 
        /// Auxiliary function only, not in IBL interface.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal IEnumerable<BO.Drone> getDroneListWithoutDeletedDrones()
        {
            lock (DalObj)
            {
                return (from D in DronesListBL
                        where D.isActive
                        select D);
            }
        }

        /// <summary>
        /// the function returns the list of parcel as a list of 'parcel to list' grouped by sender.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetPacelListGroupBySender()
        {
            return from parcel in GetParcelList()
                   orderby parcel.SenderId
                   select parcel;        
        }

        /// <summary>
        /// the function returns customer list (without deleted customers).
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomerList()
        {
            lock (DalObj)
            {
                return (from C in DalObj.GetCustomerList()
                        where C.IsActive
                        select new CustomerToList(DalObj, ConvertToBL.ConvertToCustomrtBL(C)));
            }
        }

        /// <summary>
        /// the function returns stations list.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationList()
        {
            lock (DalObj)
            {
                return (from S in DalObj.GetStationList()
                        select new StationToList(ConvertToBL.ConvertToStationBL(S)));
            }
        }

        /// <summary>
        /// the functions returns stations list sorted by available charging lots.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationListSortedByEmptySlots()
        {
            return (from station in GetStationList()
                    orderby station.AvailableChargingStations 
                    select station);
        }
        #endregion

        #region GET OBJ BY ID FUNCTIONS
        /// <summary>
        /// the function returns employee by id
        /// </summary>
        /// <param name="emplyeeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public EmpolyeeBL GetEmployee(int emplyeeId)
        {
            return ConvertToBL.convertToEmployee(emplyeeId);
        }

        /// <summary>
        /// the function returns customer by id
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer GetCustomerByID(int customerID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToCustomrtBL(DalObj.GetCustomerByID(customerID));
            }
        }

        /// <summary>
        /// funstione returns station by id
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetToStationByID(int stationID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToStationBL(DalObj.GetStation(stationID));
            }
        }

        /// <summary>
        /// function returns parcel by id
        /// </summary>
        /// <param name="parcelID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetParcel(int parcelID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToParcelBL(DalObj.GetParcel(parcelID));
            }
        }

        /// <summary>
        /// function gets drone in charge and returns this drone as BO.Drone (by id)
        /// </summary>
        /// <param name="droneInCharge"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone ConvertDroneInChargeToDrone(DroneInCharge droneInCharge)
        {
            lock (DalObj)
            {
                return DronesListBL.First(drone => drone.Id == droneInCharge.Id);
            }
        }
        #endregion

        #region GET TRUE/FALSE FOR USER STATUS
        /// <summary>
        /// function checks if the user is a customer:
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserIsCustomer(string name, int id)
        {
            lock (DalObj)
            {
                return DalObj.GetCustomerList().Any(c => c.Id == id && c.Name == name);
            }
        }

        /// <summary>
        /// function checks if user is employee
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserIsEmployee(string name,int id)
        {
            lock (DalObj)
            {
                return DalObj.GetEmployeeList().Any(c => c.Id == id && c.Name == name && !c.Manager);
            }
        }

        /// <summary>
        /// function chacks if user is manager
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserIsManager(string name, int id)
        {
            lock (DalObj)
            {
                return DalObj.GetEmployeeList().Any(c => c.Id == id && c.Name == name && c.Manager);
            }
        }
        #endregion
    }
}
