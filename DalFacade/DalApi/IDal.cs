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
        #region STATION'S FUNCTION
        public void AddStationDAL(Station DALS);
        public Station returnStation(int StationIdS);
        public IEnumerable<Station> returnStationArray();
        public void ReplaceStationById(Station DALS);
        #endregion

        #region DRONE'S FUNCTION
        public void AddDroneDAL(Drone DALD);
        public void Charge(DroneCharge DALDC);
        public DroneCharge returnDroneInCharge(int idDC);
        public IEnumerable<Drone> returnDroneArray();
        public void ReplaceDroneById(Drone DALD);
        #endregion

        #region CUSTOMER'S FUNCTION
        public void AddCustomerDAL(Customer DALC);
        public Customer returnCustomer(int CustomerIdS);
        public IEnumerable<Customer> returnCustomerArray();
        public void ReplaceCustomerById(Customer DALC);
        #endregion

        #region PARCEL'S FUNCTION
        public void AddParcelDAL(Parcel DALP);
        public Parcel returnParcel(int ParcelIdS);
        public Parcel returnParcelByDroneId(int DroneIdS);
        public IEnumerable<Parcel> returnParcelArray();
        public void ReplaceParcelById(Parcel DALP);
        public int GetNewParcelId();
        #endregion

        public Employee returnEmployee(int idE);
        public IEnumerable<Employee> returnEmployeeArray();

        public double[] powerRequest();

    }
}