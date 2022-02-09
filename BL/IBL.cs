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
        public Action<Drone> ActionDroneChanged { get; set; }
        public event Action<Drone, bool> droneSimulation;
        public void StartSimulation(IBL BL,int droneID, Action<Drone, int> droneSimulation, Func<bool> needToStop);
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AddDrone(int id, string model, BO.Enum.WeightCategoriesBL weight, int stationId);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public void AddParcel(int idSender, int idTarget, BO.Enum.WeightCategoriesBL weight, BO.Enum.PrioritiesBL priority);

        public void UpDateDroneName(int id, string newModelName);
        public void UpDateStationData(int id, string name = null, int chargeslots = -1);
        public void UpDateCustomerData(int id, string name = null, string newPhone = null);

        public void SendDroneToCharge(int id);
        public void ReleaseDroneFromCharging(int id);
        public void AssigningPackageToDrone(int idD);
        public void CollectionOfAParcelByDrone(int idD);
        public void DeliveryOfAParcelByDrone(int idD);

        public void DeleteParcel(Parcel parcel);
        public void RemoveCustomerById(int idCustomer);


        public void DisplayStatoin(int idS);
        public void DisplayDrone(int idD);
        public void DisplayCustomer(int idC);
        public void DisplayParcel(int idP);
        public void DisplayStatoinList();
        public void DisplayDroneList();
        public void DisplayCustomerList();
        public void DisplayParcelList();
        public void DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone();

        public IEnumerable<Drone> ReturnDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight);
        public IEnumerable<Drone> ReturnDronesByStatusOrder();
        public IEnumerable<ParcelToList> ReturnParcelList();
        public IEnumerable<ParcelToList> ReturnPacelListGroupBySender();
        public IEnumerable<CustomerToList> ReturnCustomerList();
        //public Customer ReturnCustomer(int id);
        public IEnumerable<StationToList> ReturnStationList();
        public IEnumerable<StationToList> ReturnStationListSortedByEmptySlots();
        public EmpolyeeBL returnEmployee(int idE);

        public Customer convertCustomerToCustomerBl(int customerID);
        public Station convertStationToStationBl(int stationID);
        public Drone convertDroneInChargeBLToDroneBl(DroneInCharge chargeBL);
        public Parcel returnParcel(int parcelID);

        public bool userIsCustomer(string name, int id);
        public bool userIsEmployee(string name, int id);
        public bool userIsManager(string name, int id);
    }
}