﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        
        #region STATION'S FUNCTIONS
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void UpDateStationData(int id, string name = null, int chargeslots = -1);
        public void DisplayStatoin(int idS);
        public void DisplayStatoinList();
        public IEnumerable<StationToList> GetStationList();
        public IEnumerable<StationToList> GetStationListSortedByEmptySlots();
        public Station GetToStationByID(int stationID);
        #endregion

        #region DRONE'S FUNCRIONS
        public void AddDrone(int id, string model, BO.Enum.WeightCategoriesBL weight, int stationId);
        public void UpDateDroneName(int id, string newModelName, bool simulation = false);
        public void DeleteDrone(int id);
        public void SendDroneToCharge(int id, bool simulation = false);
        public void ReleaseDroneFromCharging(int id, bool simulation = false);
        public void AssigningPackageToDrone(int idD, bool simulation = false);
        public void DisplayDrone(int idD);
        public void DisplayDroneList();
        public IEnumerable<Drone> GetDronesByStatusAndMaxW(int droneStatus, int droneMaxWeight);
        public IEnumerable<Drone> GetDronesSortrdByStatusOrder();
        public Drone ConvertDroneInChargeToDrone(DroneInCharge chargeBL);
        #endregion

        #region CUSTOMER'S FUNCTIONS
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public void UpDateCustomerData(int id, string name = null, string newPhone = null);
        public void RemoveCustomerById(int idCustomer);
        public void DisplayCustomer(int idC);
        public void DisplayCustomerList();
        public IEnumerable<CustomerToList> GetCustomerList();
        public Customer GetCustomerByID(int customerID);
        #endregion

        #region PARCEL'S FUNCTIONS
        public void AddParcel(int idSender, int idTarget, BO.Enum.WeightCategoriesBL weight, BO.Enum.PrioritiesBL priority);
        public void CollectionOfAParcelByDrone(int idD, bool simulation = false);
        public void DeliveryOfAParcelByDrone(int idD, bool simulation = false);
        public void DeleteParcel(Parcel parcel);
        public void DisplayParcel(int idP);
        public void DisplayParcelList();
        public void DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone();
        public IEnumerable<ParcelToList> GetParcelList();
        public IEnumerable<ParcelToList> GetPacelListGroupBySender();
        public Parcel GetParcel(int parcelID);
        #endregion

        #region ACTIONS 
        public Action<Parcel,bool> ActionParcelChanged { get; set; }
        public Action<Drone> ActionDroneChanged { get; set; }
        public Action<Customer> ActionCustomerChanged { get; set; }
        public Action<bool> ActionUpdateList { get; set; }
        #endregion

        public void StartSimulation(IBL BL, int droneID, Action<Drone> droneSimulation, Func<bool> needToStop);
        public EmpolyeeBL GetEmployee(int idE);
       
        public bool UserIsCustomer(string name, int id);
        public bool UserIsEmployee(string name, int id);
        public bool UserIsManager(string name, int id);
    }
}