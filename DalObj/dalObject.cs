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


        #region add function
        /// <summary>
        /// function gets dal station and add it to the list
        /// </summary>
        /// <param name="DALS"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStationDAL(Station DALS)
        {
            DataSource.MyBaseStations.Add(DALS);
        }
        /// <summary>
        /// function gets dal drone and add it to the list
        /// </summary>
        /// <param name="DALD"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneDAL(Drone DALD)
        {
            DataSource.MyDrones.Add(DALD);
        }
        /// <summary>
        /// function gets dal customer and add it to the list
        /// </summary>
        /// <param name="DALC"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomerDAL(Customer DALC)
        {
            DataSource.MyCustomers.Add(DALC);
        }
        /// <summary>
        /// function gets dal parcel and add it to the list
        /// </summary>
        /// <param name="DALP"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcelDAL(Parcel DALP)
        {
            DataSource.MyParcels.Add(DALP);
        }
        /// <summary>
        /// /// <summary>
        /// function gets dal dronechareg obj and add it to the list
        /// </summary>
        /// <param name="DALS"></param>
        /// </summary>
        /// <param name="DALDC"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Charge(DroneCharge DALDC)
        {
            DataSource.MyDroneCharges.Add(DALDC);
        }
        #endregion

        #region return object 
        /// <summary>
        /// function returns station by given id
        /// </summary>
        /// <param name="StationIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int StationIdS)
        {
            return DataSource.MyBaseStations.First(station => station.Id == StationIdS);
        }
        /// <summary>
        /// function returns cutomer by given id
        /// </summary>
        /// <param name="CustomerIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerByID(int CustomerIdS)
        {
            return DataSource.MyCustomers.First(customer => customer.Id == CustomerIdS);
        }
        /// <summary>
        /// function returns employee by given id
        /// </summary>
        /// <param name="idE"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Employee GetEmployee(int idE)
        {
            return DataSource.MyEmployees.First(employee => employee.Id == idE);
        }

        /// <summary>
        /// function returns parcel by given condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Parcel GetParcelByCondition(Predicate<Parcel> condition)
        {
            return DataSource.MyParcels.First(p => condition(p));
        }

        /// <summary>
        /// function returns station by given drone in charge id
        /// </summary>
        /// <param name="idDC"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneInChargeByID(int idDC)
        {
            return DataSource.MyDroneCharges.First(drone => drone.DroneId == idDC);
        }
        #endregion

        #region return array
        /// <summary>
        /// functions return stations list frome the list in data
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationList()
        {
            return from element in DataSource.MyBaseStations select element;
        }
        /// <summary>
        /// functions return drone list frome the list in data
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDroneList()
        {
            return from element in DataSource.MyDrones select element;
        }
        /// <summary>
        /// functions return customer list frome the list in data
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerList()
        {
            return from element in DataSource.MyCustomers select element;
        }
        /// <summary>
        /// functions return employees list frome the list in data
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Employee> GetEmployeeList()
        {
            return from element in DataSource.MyEmployees select element; 
        }
        /// <summary>
        /// functions return parcels list frome the list in data
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelList()
        {
            return from element in DataSource.MyParcels select element;
        }
        /// <summary>
        /// functions return id for a parcel
        /// </summary>
        /// <returns></returns>
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

        #region replace object by id
        /// <summary>
        ///  function replace station by id from the id of the station arg
        /// </summary>
        /// <param name="DALS"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceStationById(Station DALS)
        {
            DataSource.MyBaseStations[DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(s => s.Id == DALS.Id))] = DALS;
        }
        /// <summary>
        /// function replace drone by id from the id of the drone arg
        /// </summary>
        /// <param name="DALD"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceDroneById(Drone DALD)
        {
            DataSource.MyDrones[DataSource.MyDrones.IndexOf(DataSource.MyDrones.First(d => d.Id == DALD.Id))] = DALD;
        }
        /// <summary>
        ///  function replace customer by id from the id of the customer arg
        /// </summary>
        /// <param name="DALC"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceCustomerById(Customer DALC)
        {
            DataSource.MyCustomers[DataSource.MyCustomers.IndexOf(DataSource.MyCustomers.First(c => c.Id == DALC.Id))] = DALC;
        }
        /// <summary>
        ///  function replace parcel by id from the id of the parcel arg
        /// </summary>
        /// <param name="DALP"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceParcelById(Parcel DALP)
        {
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.Id == DALP.Id))] = DALP;
        }
        #endregion
    }

}
