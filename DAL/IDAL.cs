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
            public void AddStation(double LongitudeS, double LatitudeS, int ChargeSlotsS);
            public void AddDrone(WeightCategories weightS);
            public  void AddCustomer(int idS, string nameS, string phoneS, double longitudeS, double latitudeS);
            public  void AddParcel(int senderIdS, int targetIdS, WeightCategories weightS, Priorities priorityS);

            public void Scheduled(int parcelIdS);
            public void PickUp(int parcelIdS);
            public void Delivered(int parcelIdS);

            public Station returnStation(int StationIdS);
            public Drone returnDrone(int DroneIdS);
            public Customer returnCustomer(int CustomerIdS);
            public Parcel returnParcel(int ParcelIdS);

            public IEnumerable<Station> returnStationArray();
            public IEnumerable<Drone> returnDroneArray();

            public double[] powerRequest();
        }

    }
}
