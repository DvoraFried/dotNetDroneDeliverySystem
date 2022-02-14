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
        public IEnumerable<BO.Drone> ReturnDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight)
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
        public IEnumerable<BO.Drone> ReturnDronesByStatusOrder()
        {
            lock (DalObj)
            {
                return from d in DronesListBL.OrderBy(d => d.DroneStatus)
                       where d.isActive || d.delivery != null
                       select d;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> ReturnDrones()
        {
            lock (DalObj)
            {
                return from d in DronesListBL
                       select d;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ReturnParcelList()
        {
            lock (DalObj)
            {
                return (from P in DalObj.returnParcelArray()
                        where P.isActive
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
        public IEnumerable<ParcelToList> ReturnPacelListGroupBySender()
        {
            return from parcel in ReturnParcelList()
                   orderby parcel.SenderId
                   select parcel;        
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> ReturnCustomerList()
        {
            lock (DalObj)
            {
                return (from C in DalObj.returnCustomerArray()
                        where C.isActive
                        select new CustomerToList(DalObj, ConvertToBL.ConvertToCustomrtBL(C)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> ReturnStationList()
        {
            lock (DalObj)
            {
                return (from S in DalObj.returnStationArray()
                        select new StationToList(ConvertToBL.ConvertToStationBL(S)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> ReturnStationListSortedByEmptySlots()
        {
            return (from station in ReturnStationList()
                    orderby station.AvailableChargingStations 
                    select station);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public EmpolyeeBL returnEmployee(int idE)
        {
            return ConvertToBL.convertToEmployee(idE);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer convertCustomerToCustomerBl(int customerID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(customerID));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station convertStationToStationBl(int stationID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToStationBL(DalObj.returnStation(stationID));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel returnParcel(int parcelID)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(parcelID));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone convertDroneInChargeBLToDroneBl(DroneInCharge chargeBL)
        {
            lock (DalObj)
            {
                return DronesListBL.First(drone => drone.getIdBL() == chargeBL.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool userIsCustomer(string name, int id)
        {
            lock (DalObj)
            {
                if (DalObj.returnCustomerArray().Any(c => c.Id == id && c.Name == name))
                {
                    return true;
                }
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool userIsEmployee(string name,int id)
        {
            lock (DalObj)
            {
                if (DalObj.returnEmployeeArray().Any(c => c.Id == id && c.Name == name && !c.Manager))
                {
                    return true;
                }
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool userIsManager(string name, int id)
        {
            lock (DalObj)
            {
                if (DalObj.returnEmployeeArray().Any(c => c.Id == id && c.Name == name && c.Manager))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
