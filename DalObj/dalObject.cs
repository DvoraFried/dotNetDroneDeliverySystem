using System.Runtime.CompilerServices;
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

        #region ADD OBJECTS
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStationDAL(Station DALS)
        {
            DataSource.MyBaseStations.Add(DALS);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneDAL(Drone DALD)
        {
            DataSource.MyDrones.Add(DALD);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomerDAL(Customer DALC)
        {
            DataSource.MyCustomers.Add(DALC);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcelDAL(Parcel DALP)
        {
            DataSource.MyParcels.Add(DALP);
        }
        #endregion

        #region UPDATE OBJECTS 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Charge(DroneCharge DALDC)
        {
            DataSource.MyDroneCharges.Add(DALDC);
        }
        #endregion

        #region GET OBJECT  
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int StationIdS)
        {
            return DataSource.MyBaseStations.First(station => station.Id == StationIdS);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerByID(int CustomerIdS)
        {
            return DataSource.MyCustomers.First(customer => customer.Id == CustomerIdS);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Employee GetEmployee(int idE)
        {
            return DataSource.MyEmployees.First(employee => employee.Id == idE);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int ParcelIdS)
        {
            return DataSource.MyParcels.First(parcel => parcel.Id == ParcelIdS);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelByDroneId(int DroneIdS)
        {
            return DataSource.MyParcels.First(parcel => parcel.DroneId == DroneIdS);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneInChargeByID(int idDC)
        {
            return DataSource.MyDroneCharges.First(drone => drone.DroneId == idDC);
        }
        #endregion

        #region GET LISTS
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationList()
        {
            foreach (Station element in DataSource.MyBaseStations) { yield return element; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDroneList()
        {
            foreach (Drone element in DataSource.MyDrones) { yield return element; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerList()
        {
            foreach (Customer element in DataSource.MyCustomers) { yield return element; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Employee> GetEmployeeList()
        {
            foreach (Employee element in DataSource.MyEmployees) { yield return element; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelList()
        {
            foreach (Parcel element in DataSource.MyParcels) { if (element.IsActive) { yield return element; } }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetNewParcelId()
        {
            return DataSource.Config.idParcels++;
        }
        public double[] PowerRequest()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.available;
            arr[1] = DataSource.Config.carryLightWeight;
            arr[2] = DataSource.Config.carrymediumWeight;
            arr[3] = DataSource.Config.carryHeavyWeight;
            arr[4] = DataSource.Config.DroneLoadingRate;
            return arr;
        }
        #endregion

        #region REPLACR OBJECT BY ID
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceStationById(Station DALS)
        {
            DataSource.MyBaseStations[DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(s => s.Id == DALS.Id))] = DALS;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceDroneById(Drone DALD)
        {
            DataSource.MyDrones[DataSource.MyDrones.IndexOf(DataSource.MyDrones.First(d => d.Id == DALD.Id))] = DALD;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceCustomerById(Customer DALC)
        {
            DataSource.MyCustomers[DataSource.MyCustomers.IndexOf(DataSource.MyCustomers.First(c => c.Id == DALC.Id))] = DALC;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceParcelById(Parcel DALP)
        {
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.Id == DALP.Id))] = DALP;
        }
        #endregion
    }

}
