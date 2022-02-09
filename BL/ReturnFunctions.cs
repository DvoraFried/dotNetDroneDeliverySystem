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
        public List<BO.Drone> ReturnDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight)
        {
            List<BO.Drone> droneUpdateList = new List<BO.Drone>();
            if (droneStatus != -1)
            {
                if(droneMaxWeight != -1)
                return (from D in DronesListBL where ((int)D.DroneStatus == droneStatus && (int)D.MaxWeight == droneMaxWeight) select D).ToList();
                else 
                return (from D in DronesListBL where ((int)D.DroneStatus == droneStatus) select D).ToList();
            }
            else if (droneMaxWeight != -1)
            {
                return (from D in DronesListBL where ((int)D.MaxWeight == droneMaxWeight) select D).ToList();
            }
            return DronesListBL;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer ReturnCustomer(int id)
        {
            lock (DalObj)
            {
                return ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(id));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Drone> ReturnDronesByStatusOrder()
        {
            IEnumerable<BO.Drone> dList = DronesListBL.OrderBy(d => d.DroneStatus);
            foreach (BO.Drone element in dList)
            {
                yield return element;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<ParcelToList> ReturnParcelList()
        {
            lock (DalObj)
            {
                return (from P in DalObj.returnParcelArray()
                        where P.isActive
                        select new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(P))).ToList();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ReturnPacelListGroupBySender()
        {
            IEnumerable<ParcelToList> pList = ReturnParcelList().OrderBy(s => s.SenderId);

            foreach (ParcelToList element in pList)
            {
                yield return element;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<CustomerToList> ReturnCustomerList()
        {
            lock (DalObj)
            {
                return (from C in DalObj.returnCustomerArray()
                        where C.isActive
                        select new CustomerToList(DalObj, ConvertToBL.ConvertToCustomrtBL(C))).ToList();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<StationToList> ReturnStationList()
        {
            lock (DalObj)
            {
                return (from S in DalObj.returnStationArray()
                        select new StationToList(ConvertToBL.ConvertToStationBL(S))).ToList();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> ReturnStationListSortedByEmptySlots()
        {
            IEnumerable<StationToList> sList = ReturnStationList().OrderBy(s => s.AvailableChargingStations);
            
            foreach (StationToList element in sList)
            {
                yield return element;
            }
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
        public BO.Parcel convertParcelToParcelBl(int parcelID)
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

        public BO.Parcel returnParcel(int id)
        {
            return ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(id));
        }
    }
}
