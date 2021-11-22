using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public interface IDAL
        {
            public void AddStationDAL(StationDAL DALS);
            public void AddDroneDAL(DroneDAL DALD);
            public  void AddCustomerDAL(CustomerDAL DALC);
            public  void AddParcelDAL(ParcelDAL DALP);

            public void Scheduled(int parcelIdS);
            public void PickUp(int parcelIdS);
            public void Delivered(int parcelIdS);
            public void Charge(DroneChargeDAL DALDC);

            public StationDAL returnStation(int StationIdS);
            public DroneDAL returnDrone(int DroneIdS);
            public CustomerDAL returnCustomer(int CustomerIdS);
            public ParcelDAL returnParcel(int ParcelIdS);

            public IEnumerable<StationDAL> returnStationArray();
            public IEnumerable<DroneDAL> returnDroneArray();
            public IEnumerable<CustomerDAL> returnCustomerArray();
            public IEnumerable<ParcelDAL> returnParcelArray();

            public double[] powerRequest();

            public void ReplaceStationByIndex(StationDAL DALS, int idx);
            public void ReplaceDroneByIndex(DroneDAL DALD, int idx);
            public void ReplaceCustomerByIndex(CustomerDAL DALC, int idx);
            public void ReplaceParcelByIndex(ParcelDAL DALP, int idx);

            public void DeleteObjFromDroneCharges(int id);
        }

    }
}