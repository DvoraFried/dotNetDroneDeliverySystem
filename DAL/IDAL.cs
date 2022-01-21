using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalApi
{
    public interface IDAL
    {
        public void AddStationDAL(StationDAL DALS);
        public void AddDroneDAL(DroneDAL DALD);
        public  void AddCustomerDAL(CustomerDAL DALC);
        public  void AddParcelDAL(ParcelDAL DALP);
        public void AddParcelDALWithNoTargetOrSender(ParcelDAL DALP);
        
        public void Scheduled(int parcelIdS);
        public void PickUp(int parcelIdS);
        public void Delivered(int parcelIdS);
        public void Charge(DroneChargeDAL DALDC);
        public void releaseCharge(DroneChargeDAL DALDC);

        public StationDAL returnStation(int StationIdS);
        public DroneDAL returnDrone(int DroneIdS);
        public CustomerDAL returnCustomer(int CustomerIdS);
        public EmployeeDAL returnEmployee(int idE);
        public ParcelDAL returnParcel(int ParcelIdS);
        public ParcelDAL returnParcelByDroneId(int DroneIdS);
        public DroneChargeDAL returnDroneInCharge(int idDC);
        public IEnumerable<StationDAL> returnStationArray();
        public IEnumerable<DroneDAL> returnDroneArray();
        public IEnumerable<CustomerDAL> returnCustomerArray();
        public IEnumerable<EmployeeDAL> returnEmployeeArray();
        public IEnumerable<ParcelDAL> returnParcelArray();
        public IEnumerable<ParcelDAL> returnParcelWithOutTargetArray();
        public double[] powerRequest();

        public void ReplaceStationById(StationDAL DALS);
        public void ReplaceDroneById(DroneDAL DALD);
        public void ReplaceCustomerById(CustomerDAL DALC);
        public void ReplaceParcelById(ParcelDAL DALP);
        public void DeleteObjFromDroneCharges(int id);
        public void RemoveParcelById(ParcelDAL DALP);

    }
}