using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal
{
    internal sealed class DalObject : IDal {

        internal static DalObject instance = null;
        private static readonly object padLock = new object();
        DalObject() 
        {
            DataSource.Initialize();
        }
        public static DalObject GetDal
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new DalObject();
                        }
                    }
                }
                return instance;
            }
        }
        static Random rnd = new Random();

        //=====================================================================
        //                     1. class add - add function
        //=====================================================================
        public void AddStationDAL(Station DALS)
        {
            DataSource.MyBaseStations.Add(DALS);
        }
        public void AddDroneDAL(Drone DALD)
        {
            DataSource.MyDrones.Add(DALD);
        }
        public void AddCustomerDAL(Customer DALC)
        {
            DataSource.MyCustomers.Add(DALC);
        }
        public void AddParcelDAL(Parcel DALP)
        {
            DataSource.MyParcels.Add(DALP);
        }

        //=====================================================================
        //                     2. class update - update functions 
        //=====================================================================
        public void Charge(DroneCharge DALDC)
        {

            DataSource.MyDroneCharges.Add(DALDC);
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
        public Employee returnEmployee(int idE)
        {
            return DataSource.MyEmployees.First(employee => employee.Id == idE);
        }
        public Parcel returnParcel(int ParcelIdS)
        {
            return DataSource.MyParcels.First(parcel => parcel.Id == ParcelIdS);
        }
        public Parcel returnParcelByDroneId(int DroneIdS)
        {
            return DataSource.MyParcels.First(parcel => parcel.DroneId == DroneIdS);
        }
        public DroneCharge returnDroneInCharge(int idDC)
        {
            return DataSource.MyDroneCharges.First(drone => drone.DroneId == idDC);
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
        public IEnumerable<Employee> returnEmployeeArray()
        {
            foreach (Employee element in DataSource.MyEmployees) { yield return element; }
        }

        public IEnumerable<Parcel> returnParcelArray()
        {
            foreach (Parcel element in DataSource.MyParcels) { if (element.isActive) { yield return element; } }
        }
 
        //=====================================================================
        //returns a list of not scheduled parcels
        //=====================================================================
        public IEnumerable<Parcel> returnNotScheduledParcel()
        {
            //String.IsNullOrEmpty(element.DroneId.ToString())
            foreach (Parcel element in DataSource.MyParcels) { if (element.DroneId == -1) yield return element; }
        }
        //=====================================================================
        //returns a list of station with empty cherge slots
        //=====================================================================
        public IEnumerable<Station> returnStationWithChargeSlots()
        {
            foreach (Station element in DataSource.MyBaseStations) { if (element.EmptyChargeSlots > 0) yield return element; }
        }

        public double[] powerRequest()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.available;
            arr[1] = DataSource.Config.carryLightWeight;
            arr[2] = DataSource.Config.carrymediumWeight;
            arr[3] = DataSource.Config.carryHeavyWeight;
            arr[4] = DataSource.Config.DroneLoadingRate;
            return arr;
        }
        //=====================================================================
        //replace object in id
        //=====================================================================
        public void ReplaceStationById(Station DALS)
        {
            DataSource.MyBaseStations[DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(s => s.Id == DALS.Id))] = DALS;
        }
        public void ReplaceDroneById(Drone DALD)
        {
            DataSource.MyDrones[DataSource.MyDrones.IndexOf(DataSource.MyDrones.First(d => d.Id == DALD.Id))] = DALD;
        }
        public void ReplaceCustomerById(Customer DALC)
        {
            DataSource.MyCustomers[DataSource.MyCustomers.IndexOf(DataSource.MyCustomers.First(c => c.Id == DALC.Id))] = DALC;
        }
        public void ReplaceParcelById(Parcel DALP)
        {
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.Id == DALP.Id))] = DALP;
        }
        public void DeleteObjFromDroneCharges(int id)
        {
            throw new NotImplementedException();
        }
        //=============================================
        //remove item frm list by ID
        //=============================================
        public void RemoveParcelById(Parcel DALP)
        {
            DataSource.MyParcels.RemoveAll(p => p.Id == DALP.Id);
        }
    }

}
