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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> GetDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight)
        {
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> ReturnDroneListWithoutDeletedDrones()
        {
            lock (DalObj)
            {
                return (from D in DronesListBL
                        where D.isActive
                        select D);
            }
        }

            [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetPacelListGroupBySender()
        {
            return from parcel in GetParcelList()
                   orderby parcel.SenderId
                   select parcel;        
        }

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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationList()
        {
            lock (DalObj)
            {
                return (from S in DalObj.GetStationList()
                        select new StationToList(ConvertToBL.ConvertToStationBL(S)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationListSortedByEmptySlots()
        {
            return (from station in GetStationList()
                    orderby station.AvailableChargingStations 
                    select station);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public EmpolyeeBL GetEmployee(int idE)
        {
            return ConvertToBL.convertToEmployee(idE);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer GetCustomerByID(int customerID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToCustomrtBL(DalObj.GetCustomerByID(customerID));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station GetToStationByID(int stationID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToStationBL(DalObj.GetStation(stationID));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetParcel(int parcelID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToParcelBL(DalObj.GetParcel(parcelID));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone ConvertDroneInChargeToDrone(DroneInCharge chargeBL)
        {
            lock (DalObj)
            {
                return DronesListBL.First(drone => drone.Id == chargeBL.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserIsCustomer(string name, int id)
        {
            lock (DalObj)
            {
                return DalObj.GetCustomerList().Any(c => c.Id == id && c.Name == name);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserIsEmployee(string name,int id)
        {
            lock (DalObj)
            {
                return DalObj.GetEmployeeList().Any(c => c.Id == id && c.Name == name && !c.Manager);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserIsManager(string name, int id)
        {
            lock (DalObj)
            {
                return DalObj.GetEmployeeList().Any(c => c.Id == id && c.Name == name && c.Manager);
            }
        }
    }
}
