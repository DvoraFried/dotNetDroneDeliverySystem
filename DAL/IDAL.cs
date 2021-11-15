using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        interface IDAL
        {
            public void AddStation(StationDAL DALS);
            public void AddDrone(WeightCategories weightS);
            public  void AddCustomer(int idS, string nameS, string phoneS, double longitudeS, double latitudeS);
            public  void AddParcel(int senderIdS, int targetIdS, WeightCategories weightS, Priorities priorityS);

            public void Scheduled(int parcelIdS);
            public void PickUp(int parcelIdS);
            public void Delivered(int parcelIdS);

            public StationDAL returnStation(int StationIdS);
            public DroneDAL returnDrone(int DroneIdS);
            public CustomerDAL returnCustomer(int CustomerIdS);
            public ParcelDAL returnParcel(int ParcelIdS);

            public IEnumerable<StationDAL> returnStationArray();
            public IEnumerable<DroneDAL> returnDroneArray();

            public double[] powerRequest();
        }

    }
}