using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.IO;
using static DalFacade.DalApi.Exeptions.Exceptions;
using System.Xml.Linq;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        internal static DalXml instance = null;
        private static readonly object padLock = new object();

        static string dir = @"..\..\..\..\xml\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public static DalXml GetDal
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new DalXml();
                        }
                    }
                }
                return instance;
            }
        }

        static string customerFilePath = @"customerList.xml";
        static string dronesInChargeFilePath = @"dronesInCharge.xml";
        static string stationFilePath = @"stationList.xml";
        static string droneFilePath = @"droneList.xml";
        static string parcelFilePath = @"parcelList.xml";
        static string employeeFilePath = @"employeeList.xml";
        /// <summary>
        /// the constructor call the initilixe func to random list and add them to the xml pages
        /// </summary>
        public DalXml()
        {
            DataSource.Initialize();
            DL.XMLTools.SaveListToXMLSerializer<Customer>(DataSource.MyCustomers, dir + customerFilePath);
            DL.XMLTools.SaveListToXMLSerializer<DroneCharge>(DataSource.MyDroneCharges, dir + dronesInChargeFilePath);
            DL.XMLTools.SaveListToXMLSerializer<Station>(DataSource.MyBaseStations, dir + stationFilePath);
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(DataSource.MyParcels, dir + parcelFilePath);
            DL.XMLTools.SaveListToXMLSerializer<Employee>(DataSource.MyEmployees, dir + employeeFilePath);
            DL.XMLTools.SaveDronesWithXElement(DataSource.MyDrones, dir + droneFilePath);
        }
        #region ADD TO XML FUNCTIONS
        /// <summary>
        /// function add station to the xml station list page
        /// </summary>
        /// <param name="DALS"></param>
        public void AddStationDAL(Station DALS)
        {
            List<Station> stations = DL.XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + stationFilePath).ToList();
            if(stations.Any(station => station.Id == DALS.Id))
            {
                throw new ObjectExistsInListException("Station");
            }
            stations.Add(DALS);
            DL.XMLTools.SaveListToXMLSerializer<Station>(stations, dir + stationFilePath);
        }
        /// <summary>
        /// function add drone to the xml drone list page
        /// </summary>
        /// <param name="DALD"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneDAL(Drone DALD)
        {
            List<Drone> drones = DL.XMLTools.LoadListWithXElement(dir+droneFilePath).ToList();
            if (drones.Any(drone => drone.Id == DALD.Id)) { throw new ObjectExistsInListException("Drone"); }
            drones.Add(DALD);
            DL.XMLTools.SaveDronesWithXElement(drones, dir + droneFilePath);
        }
        /// <summary>
        /// function add customer to the xml customer list page
        /// </summary>
        /// <param name="DALC"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomerDAL(Customer DALC)
        {
            List<Customer> customers = DL.XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath).ToList();
            if (customers.Any(customer => customer.Id == DALC.Id))
            {
                throw new ObjectExistsInListException("Customer");
            }
            customers.Add(DALC);
            DL.XMLTools.SaveListToXMLSerializer<Customer>(customers, dir + customerFilePath);
        }
        /// <summary>
        /// function add parcel to the xml station parcel page
        /// </summary>
        /// <param name="DALP"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcelDAL(Parcel DALP)
        {
            List<Parcel> parcels = DL.XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath).ToList();
            parcels.Add(DALP);
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(parcels, dir + parcelFilePath);
        }
        /// <summary>
        /// function add drone charge to the xml drone charge list page
        /// </summary>
        /// <param name="DALDC"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Charge(DroneCharge DALDC)
        {
            List<DroneCharge> drones = DL.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dir + dronesInChargeFilePath).ToList();
            drones.Add(DALDC);
            DL.XMLTools.SaveListToXMLSerializer<DroneCharge>(drones, dir + dronesInChargeFilePath);
        }
        #endregion

        #region GET OBJECT BY ID FUNCTIONS
        /// <summary>
        /// function returns station by id arg
        /// </summary>
        /// <param name="StationIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int StationIdS)
        {
            IEnumerable<Station> stations = DL.XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + stationFilePath);
            return stations.ToList().First(station => station.Id == StationIdS);
        }
        /// <summary>
        /// function returns customer by id arg
        /// </summary>
        /// <param name="CustomerIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerByID(int CustomerIdS)
        {
            IEnumerable<Customer> customers = DL.XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath);
            return customers.ToList().First(customer => customer.Id == CustomerIdS);
        }
        /// <summary>
        /// function returns employee by id arg
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Employee GetEmployee(int employeeId)
        {
            IEnumerable<Employee> employees = DL.XMLTools.LoadListFromXMLSerializer<DO.Employee>(dir + employeeFilePath);
            return employees.ToList().First(employee => employee.Id == employeeId);
        }
        /// <summary>
        /// function returns parcel by id arg
        /// </summary>
        /// <param name="ParcelIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int ParcelIdS)
        {
            IEnumerable<Parcel> parcels = DL.XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);
            return parcels.ToList().First(parcel => parcel.Id == ParcelIdS);
        }
        /// <summary>
        /// function returns parcel by drone id arg
        /// </summary>
        /// <param name="DroneIdS"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelByDroneId(int DroneIdS)
        {
            IEnumerable<Parcel> parcels = DL.XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);
            return parcels.ToList().First(parcel => parcel.DroneId == DroneIdS);
        }
        #endregion

        #region REPLACE OBJECT BY ID FUNCTIONS
        /// <summary>
        /// the function updates station by replacing it - using th eid of the given object
        /// </summary>
        /// <param name="DALS"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceStationById(Station DALS)
        {
            List<Station> stationList = DL.XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath).ToList();
            if (!stationList.Any(t => t.Id == DALS.Id))
            {
                throw new Exception("DL: station with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            stationList[stationList.ToList().IndexOf(stationList.First(p => p.Id == DALS.Id))] = DALS;
            DL.XMLTools.SaveListToXMLSerializer<Station>(stationList, dir + stationFilePath);
        }
        /// <summary>
        /// the function updates drone by replacing it - using th eid of the given object
        /// </summary>
        /// <param name="DALD"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceDroneById(Drone DALD)
        {
            List<Drone> droneList = DL.XMLTools.LoadListWithXElement(dir + droneFilePath).ToList();
            if (!droneList.Any(t => t.Id == DALD.Id))
            {
                throw new Exception("DL: cuxtomer with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            droneList[droneList.ToList().IndexOf(droneList.First(p => p.Id == DALD.Id))] = DALD;
            DL.XMLTools.SaveDronesWithXElement(droneList, dir + droneFilePath);
        }
        /// <summary>
        /// the function updates customer by replacing it - using th eid of the given object
        /// </summary>
        /// <param name="DALC"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceCustomerById(Customer DALC)
        {
            List<Customer> customerList = DL.XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath).ToList();
            if (!customerList.Any(t => t.Id == DALC.Id))
            {
                throw new Exception("DL: cuxtomer with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            customerList[customerList.ToList().IndexOf(customerList.First(p => p.Id == DALC.Id))] = DALC;
            DL.XMLTools.SaveListToXMLSerializer<Customer>(customerList, dir + customerFilePath);
        }
        /// <summary>
        /// the function updates parcel by replacing it - using th eid of the given object
        /// </summary>
        /// <param name="DALP"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReplaceParcelById(Parcel DALP)
        {
            List<Parcel> parcelsList = DL.XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath).ToList();
            if (!parcelsList.Any(t => t.Id == DALP.Id))
            {
                throw new Exception("DL: parcel with the same id not found...");
                //throw new SomeException("DL: Student with the same id not found...");
            }
            parcelsList[parcelsList.ToList().IndexOf(parcelsList.First(p => p.Id == DALP.Id))] = DALP;
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(parcelsList, dir + parcelFilePath);
        }
        #endregion

        #region GET DAL OBJECTS FUNCTIONS 
        /// <summary>
        /// function return station list 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationList()
        {
            IEnumerable<Station> stationList = DL.XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
            return from element in stationList select element;
        }
        /// <summary>
        /// function return drone list 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDroneList()
        {
            IEnumerable<Drone> droneList = DL.XMLTools.LoadListWithXElement(dir + droneFilePath);
            return from element in droneList select element;
        }
        /// <summary>
        /// function return customer list 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerList()
        {
            IEnumerable<Customer> customerList = DL.XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
            return from element in customerList select element;
        }
        /// <summary>
        /// function return employee list 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Employee> GetEmployeeList()
        {
            IEnumerable<Employee> employeeList = DL.XMLTools.LoadListFromXMLSerializer<Employee>(dir + employeeFilePath);
            return from element in employeeList select element;
        }
        /// <summary>
        /// function return parcel list 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelList()
        {
            IEnumerable<Parcel> parcelList = DL.XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            return from element in parcelList select element;
        }
        /// <summary>
        /// function return drone in charchi by id
        /// </summary>
        /// <param name="droneInChargeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneInChargeByID(int droneInChargeId)
        {
            IEnumerable<DroneCharge> droneCharges = DL.XMLTools.LoadListFromXMLSerializer<DroneCharge>(dir + dronesInChargeFilePath);
            return droneCharges.First(drone => drone.DroneId == droneInChargeId);
            // throw new NotImplementedException();
        }
        /// <summary>
        /// function return an id number to parcel
        /// </summary>
        /// <returns></returns>
        public int GetNewParcelId()
        {
            return DataSource.Config.idParcels++;
        }
        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
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

    }
}
