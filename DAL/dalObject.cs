using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        DalObject() { DataSource.Initialize(); }
        static Random rnd = new Random();
        //=====================================================================
        //                     1. class add - add function
        //=====================================================================
        public class Add
        {
            //=====================================================================
            //the function addstation render information for one station
            //=====================================================================
            public static void AddStation(double LongitudeS, double LatitudeS, int ChargeSlotsS = 2)
            {
                Station addS=new Station();
                addS.Id = DataSource.MyBaseStations.Count;
                addS.Name = "station" + DataSource.MyBaseStations.Count.ToString();
                addS.ChargeSlots = ChargeSlotsS;
                addS.Longitude = LongitudeS;
                addS.Latitude = LongitudeS;
                DataSource.MyBaseStations.Add(addS);
            }

            //=====================================================================
            //the function adddrone render information for one drone 
            //=====================================================================
            public static void AddDrone(WeightCategories weightS)
            {
                Drone addD = new Drone();
                addD.Id = DataSource.MyDrones.Count;
                addD.Model = "model " + DataSource.MyDrones.Count.ToString();
                addD.MaxWeight = weightS;
                DataSource.MyDrones.Add(addD);
            }

            //=====================================================================
            //the function addcustomer render information for one customer
            //=====================================================================
            public static void AddCustomer(int idS, string nameS, string phoneS, double longitudeS, double latitudeS)
            {
                Customer addC = new Customer();
                addC.Id = idS;
                addC.Name = nameS;
                addC.Phone = phoneS;
                addC.Longitude=longitudeS;
                addC.Latitude = latitudeS;
                DataSource.MyCustomers.Add(addC);
            }
            //=====================================================================
            //the function addparcel render information for one parcel
            //=====================================================================
            public static void AddParcel(int senderIdS, int targetIdS, WeightCategories weightS, Priorities priorityS)
            {
                Parcel addP = new Parcel();
                addP.Id = DataSource.MyParcel.Count;
                addP.SenderId = senderIdS;
                addP.TargetId = targetIdS;
                addP.Weight = weightS;
                addP.Priority = priorityS;
                addP.Requested = DateTime.Now;
                addP.DroneId = -1;
                DataSource.MyParcel.Add(addP);
            }
        }
        //=====================================================================
        //                     2. class update - update functions 
        //=====================================================================
        public class Update
        {

            public static void Scheduled(int parcelIdS)
            {
                Parcel upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
                Drone setD = DataSource.MyDrones.First(drone => drone.MaxWeight >= upP.Weight);
                upP.DroneId = setD.Id;
                upP.Scheduled = DateTime.Now;
                DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))]= upP;
            }

            public static void PickUp(int parcelIdS)
            {
                Parcel upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
                upP.PickUp = DateTime.Now;
                DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
            }

            public static void Delivered(int parcelIdS)
            {
                Parcel upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
                upP.Delivered = DateTime.Now;
                DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
            }
            //=====================================================================
            //the function is not requrierd in this targil. yai!
            //=====================================================================
            public static void Charge(int DroneIdS,int StationIdS)
            {
            }
            //=====================================================================
            //the function is not requrierd in this targil. yai!
            //=====================================================================
            public static void releaseCharge(int DroneIdS)
            {
            }
        }
        //=====================================================================
        //                     3. class returnObject - return functions 
        //=====================================================================
        public class returnObject
        {

            public static IDAL.DO.Station returnStation<Station>(int StationIdS)
            {
                return DataSource.MyBaseStations.First(station => station.Id == StationIdS);
            }

            public static IDAL.DO.Drone returnDrone<Drone>(int DroneIdS)
            {
                return DataSource.MyDrones.First(drone => drone.Id == DroneIdS);
            }

            public static IDAL.DO.Customer returnCustomer<Customer>(int CustomerIdS)
            {
                return DataSource.MyCustomers.First(customer => customer.Id == CustomerIdS);
            }

            public static IDAL.DO.Parcel returnParcel<Parcel>(int ParcelIdS)
            {
                return DataSource.MyParcel.First(parcel => parcel.Id == ParcelIdS);
            }
        }
        //=====================================================================
        //             4. class returnArrayObject - return array
        //=====================================================================
        public class returnArrayObject
        {

            public static List<Station> returnStationArray()
            {
                return DataSource.MyBaseStations;
            }

            public static List<Drone> returnDroneArray()
            {
                return DataSource.MyDrones;
            }

            public static List<Customer> returnCustomerArray()
            {
                return DataSource.MyCustomers;
            }

            public static List<Parcel> returnParcelArray()
            {
                return DataSource.MyParcel;
            }
            //=====================================================================
            //returns a list of not scheduled parcels
            //=====================================================================
            public static List<Parcel> returnNotScheduledParcel()
            {
                List<Parcel> notScheduledParcel = new List<Parcel>();
                foreach (Parcel element in DataSource.MyParcel){
                    if (element.DroneId == -1) notScheduledParcel.Add(element);
                }
                return notScheduledParcel;
            }
            //=====================================================================
            //returns a list of station with empty cherge slots
            //=====================================================================
            public static List<Station> returnStationWithChargeSlots()
            {
                List<Station> stationWithChargeSlots = new List<Station>();
                foreach (Station element in DataSource.MyBaseStations)
                {
                    if (element.ChargeSlots > 0) stationWithChargeSlots.Add(element);
                }             
                return stationWithChargeSlots;
            }
        }
    }
}
