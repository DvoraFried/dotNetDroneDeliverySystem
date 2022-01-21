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
        public List<DroneBL> ReturnDronesByStatusAndMaxW(int droneStatus,int droneMaxWeight)
        {
            List<DroneBL> droneUpdateList = new List<DroneBL>();
            if (droneStatus != -1 && droneMaxWeight != -1)
            {
                foreach (DroneBL element in DronesListBL) { if ((int)element.DroneStatus == droneStatus&& (int)element.MaxWeight == droneMaxWeight) { droneUpdateList.Add(element); } }
            }
            else if (droneStatus != -1)
            {
                foreach(DroneBL element in DronesListBL) { if ((int)element.DroneStatus == droneStatus) { droneUpdateList.Add(element); }}
            }
            else if (droneMaxWeight != -1)
            {
                foreach (DroneBL element in DronesListBL) { if ((int)element.MaxWeight == droneMaxWeight) { droneUpdateList.Add(element); } }
            }
            else {
                return DronesListBL;
            }
            return droneUpdateList;
        }
        public IEnumerable<DroneBL> ReturnDronesByStatusOrder()
        {
            IEnumerable<DroneBL> dList = DronesListBL.OrderBy(d => d.DroneStatus);
            foreach (DroneBL element in dList)
            {
                yield return element;
            }
        }
        public List<ParcelToList> ReturnParcelList()
        {
            List<ParcelToList> parcelsUpdateList = new List<ParcelToList>();
            foreach (ParcelDAL parcel in DalObj.returnParcelArray().ToList())
            {
                if (parcel.isActive)
                {
                    parcelsUpdateList.Add(new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(parcel)));
                }
            }
            foreach(ParcelDAL parcel in DalObj.returnParcelWithOutTargetArray().ToList())
            {
                if (parcel.isActive)
                {
                    parcelsUpdateList.Add(new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(parcel)));
                }
            }
            return parcelsUpdateList;
        }
        public IEnumerable<ParcelToList> ReturnPacelListGroupBySender()
        {
            IEnumerable<ParcelToList> pList = ReturnParcelList().OrderBy(s => s.SenderId);

            foreach (ParcelToList element in pList)
            {
                yield return element;
            }
        }
        public List<CustomerToList> ReturnCustomerList()
        {
            List<CustomerToList> customersToReturn = new List<CustomerToList>();
            foreach (CustomerDAL customer in DalObj.returnCustomerArray())
            {
                if (customer.isActive)
                {
                    customersToReturn.Add(new CustomerToList(DalObj, ConvertToBL.ConvertToCustomrtBL(customer)));
                }
            }
            return customersToReturn;
        }
        public List<StationToList> ReturnStationList()
        {
            List<StationToList> stationsToReturn = new List<StationToList>();
            foreach (StationDAL station in DalObj.returnStationArray())
            {
                stationsToReturn.Add(new StationToList(ConvertToBL.ConvertToStationBL(station)));
            }
            return stationsToReturn;
        }
        public IEnumerable<StationToList> ReturnStationListSortedByEmptySlots()
        {
            IEnumerable<StationToList> sList = ReturnStationList().OrderBy(s => s.AvailableChargingStations);
            
            foreach (StationToList element in sList)
            {
                yield return element;
            }
        }
        public EmpolyeeBL returnEmployee(int idE)
        {
            return ConvertToBL.convertToEmployee(idE);
        }
        public CustomerBL convertCustomerToCustomerBl(int customerID)
        {
            return ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(customerID));
        }
        public StationBL convertStationToStationBl(int stationID)
        {
            return ConvertToBL.ConvertToStationBL(DalObj.returnStation(stationID));
        }
        public ParcelBL convertParcelToParcelBl(int parcelID)
        {
            return ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(parcelID));
        }
        public DroneBL convertDroneInChargeBLToDroneBl(DroneInChargeBL chargeBL)
        {
            return DronesListBL.First(drone => drone.getIdBL() == chargeBL.Id);
        }
        public bool userIsCustomer(string name, int id)
        {
            if (DalObj.returnCustomerArray().Any(c => c.Id == id && c.Name == name))
            {
                return true;
            }
            return false;
        }
        public bool userIsEmployee(string name,int id)
        {
            if (DalObj.returnEmployeeArray().Any(c => c.Id == id&&c.Name==name&& !c.Manager))
            {
                return true;
            }
            return false;
        }
        public bool userIsManager(string name, int id)
        {
            if (DalObj.returnEmployeeArray().Any(c => c.Id == id && c.Name == name&&c.Manager))
            {
                return true;
            }
            return false;
        }
    }
}
