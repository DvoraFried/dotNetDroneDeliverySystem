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
        public Station GetStation(int StationIdS);
        public IEnumerable<Station> GetStationList();
        public void ReplaceStationById(Station DALS);
        #endregion

        #region DRONE'S FUNCTION
        public void AddDroneDAL(Drone DALD);
        public void Charge(DroneCharge DALDC);
        public DroneCharge GetDroneInChargeByID(int idDC);
        public IEnumerable<Drone> GetDroneList();
        public void ReplaceDroneById(Drone DALD);
        #endregion

        #region CUSTOMER'S FUNCTION
        public void AddCustomerDAL(Customer DALC);
        public Customer GetCustomerByID(int CustomerIdS);
        public IEnumerable<Customer> GetCustomerList();
        public void ReplaceCustomerById(Customer DALC);
        #endregion

        #region PARCEL'S FUNCTION
        public void AddParcelDAL(Parcel DALP);
        public Parcel GetParcelByCondition(Predicate<Parcel> condition);
        public IEnumerable<Parcel> GetParcelList();
        public void ReplaceParcelById(Parcel DALP);
        public int GetNewParcelId();
        #endregion

        public Employee GetEmployee(int idE);
        public IEnumerable<Employee> GetEmployeeList();

        public double[] PowerRequest();

    }
}