using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalApi
{
    public interface IDal
    {
        public void AddStationDAL(Station DALS);
        public void AddDroneDAL(Drone DALD);
        public void AddCustomerDAL(Customer DALC);
        public void AddParcelDAL(Parcel DALP);
        /* 
         public void Scheduled(int parcelIdS);
         public void PickUp(int parcelIdS);
         public void Delivered(int parcelIdS);
         public void releaseCharge(DroneCharge DALDC);*/
        public void Charge(DroneCharge DALDC);
        public Station returnStation(int StationIdS);
        public Drone returnDrone(int DroneIdS);
        public Customer returnCustomer(int CustomerIdS);
        public Employee returnEmployee(int idE);
        public Parcel returnParcel(int ParcelIdS);
        public Parcel returnParcelByDroneId(int DroneIdS);
        public DroneCharge returnDroneInCharge(int idDC);
        public IEnumerable<Station> returnStationArray();
        public IEnumerable<Drone> returnDroneArray();
        public IEnumerable<Customer> returnCustomerArray();
        public IEnumerable<Employee> returnEmployeeArray();
        public IEnumerable<Parcel> returnParcelArray();
        public double[] powerRequest();

        public void ReplaceStationById(Station DALS);
        public void ReplaceDroneById(Drone DALD);
        public void ReplaceCustomerById(Customer DALC);
        public void ReplaceParcelById(Parcel DALP);
        public void DeleteObjFromDroneCharges(int id);
        public void RemoveParcelById(Parcel DALP);
    }
}