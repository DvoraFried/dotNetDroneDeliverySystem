using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace DalObject
{
    internal sealed class DalObject : DalApi.IDAL {

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
        public void AddStationDAL(StationDAL DALS)
        {
            DataSource.MyBaseStations.Add(DALS);
        }
        public void AddDroneDAL(DroneDAL DALD)
        {
            DataSource.MyDrones.Add(DALD);
        }
        public void AddCustomerDAL(CustomerDAL DALC)
        {
            DataSource.MyCustomers.Add(DALC);
        }
        public void AddParcelDAL(ParcelDAL DALP)
        {
            DataSource.MyParcels.Add(DALP);
        }

        //=====================================================================
        //                     2. class update - update functions 
        //=====================================================================
        public void Scheduled(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            DroneDAL setD = DataSource.MyDrones.First(drone => drone.MaxWeight >= upP.Weight);
            upP.DroneId = setD.Id;
            upP.Scheduled = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void PickUp(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            upP.PickUp = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void Delivered(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            upP.Delivered = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }
        public void Charge(DroneChargeDAL DALDC)
        {
            DataSource.MyDroneCharges.Add(DALDC);
        }
        public void releaseCharge(DroneChargeDAL Drone)
        {
            DataSource.MyDroneCharges.Remove(Drone);
        }
        //=====================================================================
        //                     3. class returnObject - return functions 
        //=====================================================================

        public StationDAL returnStation(int StationIdS)
        {
            return DataSource.MyBaseStations.First(station => station.Id == StationIdS);
        }

        public DroneDAL returnDrone(int DroneIdS)
        {
            return DataSource.MyDrones.First(drone => drone.Id == DroneIdS);
        }
        public CustomerDAL returnCustomer(int CustomerIdS)
        {
            return DataSource.MyCustomers.First(customer => customer.Id == CustomerIdS);
        }
        public EmployeeDAL returnEmployee(int idE)
        {
            return DataSource.MyEmployees.First(employee => employee.Id == idE);
        }
        public ParcelDAL returnParcel(int ParcelIdS)
        {
            return DataSource.MyParcels.First(parcel => parcel.Id == ParcelIdS);
        }
        public ParcelDAL returnParcelByDroneId(int DroneIdS)
        {
            return DataSource.MyParcels.First(parcel => parcel.DroneId == DroneIdS);
        }
        public DroneChargeDAL returnDroneInCharge(int idDC)
        {
            return DataSource.MyDroneCharges.First(drone => drone.DroneId == idDC);
        }
        
        //=====================================================================
        //             4. class returnArrayObject - return array
        //=====================================================================

        public IEnumerable<StationDAL> returnStationArray()
        {
            foreach (StationDAL element in DataSource.MyBaseStations) { yield return element; }
        }

        public IEnumerable<DroneDAL> returnDroneArray()
        {
            foreach (DroneDAL element in DataSource.MyDrones) { yield return element; }
        }

        public IEnumerable<CustomerDAL> returnCustomerArray()
        {
            foreach (CustomerDAL element in DataSource.MyCustomers) { if (element.isActive) { yield return element; } }
        }
        public IEnumerable<EmployeeDAL> returnEmployeeArray()
        {
            foreach (EmployeeDAL element in DataSource.MyEmployees) { yield return element; }
        }

        public IEnumerable<ParcelDAL> returnParcelArray()
        {
            foreach (ParcelDAL element in DataSource.MyParcels) { if (element.isActive) { yield return element; } }
        }
 
        //=====================================================================
        //returns a list of not scheduled parcels
        //=====================================================================
        public IEnumerable<ParcelDAL> returnNotScheduledParcel()
        {
            //String.IsNullOrEmpty(element.DroneId.ToString())
            foreach (ParcelDAL element in DataSource.MyParcels) { if (element.DroneId == -1) yield return element; }
        }
        //=====================================================================
        //returns a list of station with empty cherge slots
        //=====================================================================
        public IEnumerable<StationDAL> returnStationWithChargeSlots()
        {
            foreach (StationDAL element in DataSource.MyBaseStations) { if (element.EmptyChargeSlots > 0) yield return element; }
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
        public void ReplaceStationById(StationDAL DALS)
        {
            DataSource.MyBaseStations[DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(s => s.Id == DALS.Id))] = DALS;
        }
        public void ReplaceDroneById(DroneDAL DALD)
        {
            DataSource.MyDrones[DataSource.MyDrones.IndexOf(DataSource.MyDrones.First(d => d.Id == DALD.Id))] = DALD;
        }
        public void ReplaceCustomerById(CustomerDAL DALC)
        {
            DataSource.MyCustomers[DataSource.MyCustomers.IndexOf(DataSource.MyCustomers.First(c => c.Id == DALC.Id))] = DALC;
        }
        public void ReplaceParcelById(ParcelDAL DALP)
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
        public void RemoveParcelById(ParcelDAL DALP)
        {
            DataSource.MyParcels.RemoveAll(p => p.Id == DALP.Id);
        }
    }

}
