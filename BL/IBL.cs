using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AddDrone(int id, string model, EnumBL.WeightCategoriesBL weight, int stationId);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority);
        public void UpDateDroneName(int id, string newModelName);
        public void UpDateStationData(int id, string name = null, int chargeslots = -1);
        public void UpDateCustomerData(int id, string name = null, string newPhone = null);
        public void SendDroneToCharge(int id);
        public void ReleaseDroneFromCharging(int id);
        public void AssigningPackageToDrone(int idD);
        public void CollectionOfAParcelByDrone(int idD);
        public void DeliveryOfAParcelByDrone(int idD);
        public void DisplayStatoin(int idS);
        public void DisplayDrone(int idD);
        public void DisplayCustomer(int idC);
        public void DisplayParcel(int idP);
        public void DisplayStatoinList();
        public void DisplayDroneList();
        public void DisplayCustomerList();
        public void DisplayParcelList();
        public void DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone();
        public List<DroneBL> ReturnDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight);
        public IEnumerable<DroneBL> ReturnDronesByStatusOrder();
        public List<ParcelToList> ReturnParcelList();
        public IEnumerable<ParcelToList> ReturnPacelListGroupBySender();
        public List<CustomerToList> ReturnCustomerList();
        public CustomerBL convertCustomerToCustomerBl(int customerID);
        public StationBL convertStationToStationBl(int stationID);
        public List<StationToList> ReturnStationList();
        public IEnumerable<StationToList> ReturnStationListSortedByEmptySlots();
        public void RemoveCustomerById(int idCustomer);
        public DroneBL convertDroneInChargeBLToDroneBl(DroneInChargeBL chargeBL);
        public ParcelBL convertParcelToParcelBl(int parcelID);
        public EmpolyeeBL returnEmployee(int idE);
        public bool userIsCustomer(string name, int id);
        public bool userIsEmployee(string name, int id);
        public bool userIsManager(string name, int id);
        public void DeleteParcel(ParcelBL parcel);
    }
}
