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

        public BO.Customer ReturnCustomer(int id)
        {
            return ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(id));
        }
        public IEnumerable<BO.Drone> ReturnDronesByStatusOrder()
        {
            IEnumerable<BO.Drone> dList = DronesListBL.OrderBy(d => d.DroneStatus);
            foreach (BO.Drone element in dList)
            {
                yield return element;
            }
        }
        public List<ParcelToList> ReturnParcelList()
        {
            return (from P in DalObj.returnParcelArray()
                    where P.isActive
                    select new ParcelToList(DalObj, ConvertToBL.ConvertToParcelBL(P))).ToList();
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
            return (from C in DalObj.returnCustomerArray()
                    where C.isActive
                    select new CustomerToList(DalObj, ConvertToBL.ConvertToCustomrtBL(C))).ToList();
        }
        public List<StationToList> ReturnStationList()
        {
            return (from S in DalObj.returnStationArray()
                    select new StationToList(ConvertToBL.ConvertToStationBL(S))).ToList();
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
        public BO.Customer convertCustomerToCustomerBl(int customerID)
        {
            return ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(customerID));
        }
        public BO.Station convertStationToStationBl(int stationID)
        {
            return ConvertToBL.ConvertToStationBL(DalObj.returnStation(stationID));
        }
        public BO.Parcel convertParcelToParcelBl(int parcelID)
        {
            return ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(parcelID));
        }
        public BO.Drone convertDroneInChargeBLToDroneBl(DroneInCharge chargeBL)
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
