using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;

namespace DalObject
{
    public class DalObject : IDAL.DO.IDAL
    {
        DalObject() { /*DataSource.Initialize();*/ }
        static Random rnd = new Random();
        //=====================================================================
        //                     1. class add - add function
        //=====================================================================

        //=====================================================================
        //the function addstation render information for one station
        //=====================================================================
        public void AddStation(double LongitudeS, double LatitudeS, int ChargeSlotsS = 2)
        {
            Station addS = new Station();
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
        public void AddDrone(WeightCategories weightS)
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
        public void AddCustomer(int idS, string nameS, string phoneS, double longitudeS, double latitudeS)
        {
            Customer addC = new Customer();
            addC.Id = idS;
            addC.Name = nameS;
            addC.Phone = phoneS;
            addC.Longitude = longitudeS;
            addC.Latitude = latitudeS;
            DataSource.MyCustomers.Add(addC);
        }
        //=====================================================================
        //the function addparcel render information for one parcel
        //=====================================================================
        public void AddParcel(int senderIdS, int targetIdS, WeightCategories weightS, Priorities priorityS)
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
        //=====================================================================
        //                     2. class update - update functions 
        //=====================================================================
        public void Scheduled(int parcelIdS)
        {
            Parcel upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
            Drone setD = DataSource.MyDrones.First(drone => drone.MaxWeight >= upP.Weight);
            upP.DroneId = setD.Id;
            upP.Scheduled = DateTime.Now;
            DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void PickUp(int parcelIdS)
        {
            Parcel upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
            upP.PickUp = DateTime.Now;
            DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void Delivered(int parcelIdS)
        {
            Parcel upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
            upP.Delivered = DateTime.Now;
            DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
        }
        //=====================================================================
        //the function is not requrierd in this targil. yai!
        //=====================================================================
        public void Charge(int DroneIdS, int StationIdS)
        {
        }
        //=====================================================================
        //the function is not requrierd in this targil. yai!
        //=====================================================================
        public void releaseCharge(int DroneIdS)
        {
        }
        //=====================================================================
        //                     3. class returnObject - return functions 
        //=====================================================================

        public Station returnStation(int StationIdS)
        {
            return DataSource.MyBaseStations.First(station => station.Id == StationIdS);
        }

        public Drone returnDrone(int DroneIdS)
        {
            return DataSource.MyDrones.First(drone => drone.Id == DroneIdS);
        }

        public Customer returnCustomer(int CustomerIdS)
        {
            return DataSource.MyCustomers.First(customer => customer.Id == CustomerIdS);
        }

        public Parcel returnParcel(int ParcelIdS)
        {
            return DataSource.MyParcel.First(parcel => parcel.Id == ParcelIdS);
        }
        //=====================================================================
        //             4. class returnArrayObject - return array
        //=====================================================================

        public IEnumerable<Station> returnStationArray()
        {
            foreach (Station element in DataSource.MyBaseStations) { yield return element; }
        }

        public IEnumerable<Drone> returnDroneArray()
        {
            foreach (Drone element in DataSource.MyDrones) { yield return element; }
        }

        public IEnumerable<Customer> returnCustomerArray()
        {
            foreach (Customer element in DataSource.MyCustomers) { yield return element; }
        }

        public IEnumerable<Parcel> returnParcelArray()
        {
            foreach (Parcel element in DataSource.MyParcel) { yield return element; }
        }
        //=====================================================================
        //returns a list of not scheduled parcels
        //=====================================================================
        public IEnumerable<Parcel> returnNotScheduledParcel()
        {
            foreach (Parcel element in DataSource.MyParcel) { if (element.DroneId == -1) yield return element; }
        }
        //=====================================================================
        //returns a list of station with empty cherge slots
        //=====================================================================
        public IEnumerable<Station> returnStationWithChargeSlots()
        {
            foreach (Station element in DataSource.MyBaseStations) { if (element.ChargeSlots > 0) yield return element; }
        }

        public double[] powerRequest()
        {
            double[] arr = new double[5];
            arr[0] = ((double)IDAL.DO.DroneStatuses.empty);
            arr[1] = ((double)IDAL.DO.WeightCategories.light);
            arr[2] = ((double)IDAL.DO.WeightCategories.medium);
            arr[3] = ((double)IDAL.DO.WeightCategories.heavy);
            //arr[4] = //קצב טעינה - ראו בהמשך
            return arr;
        }
    }
}
