using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.IO;
using static DalFacade.DalApi.Exeptions.Exceptions;

namespace Dal
{
    sealed class DalXml : IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        string customerFilePath = @"customerList.xml";
        string stationFilePath = @"stationList.xml";
        string droneFilePath = @"droneList.xml";
        string parcelFilePath = @"parcelList.xml";
        string employeeFilePath = @"employeeList.xml";
        public DalXml()
        {
            if (!File.Exists(dir + customerFilePath))
                DL.XMLTools.SaveListToXMLSerializer<Customer>(DataSource.MyCustomers, dir + customerFilePath);

            if (!File.Exists(dir + stationFilePath))
                DL.XMLTools.SaveListToXMLSerializer<Station>(DataSource.MyBaseStations, dir + stationFilePath);

            if (!File.Exists(dir + droneFilePath))
                DL.XMLTools.SaveListToXMLSerializer<Drone>(DataSource.MyDrones, dir + droneFilePath);

            if (!File.Exists(dir + parcelFilePath))
                DL.XMLTools.SaveListToXMLSerializer<Parcel>(DataSource.MyParcels, dir + parcelFilePath);
            
            if (!File.Exists(dir + employeeFilePath))
                DL.XMLTools.SaveListToXMLSerializer<Employee>(DataSource.MyEmployees, dir + employeeFilePath);
        }

        public void AddStationDAL(Station DALS)
        {
            IEnumerable<Station> stations = DL.XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + stationFilePath);
            if(!stations.Any(station => station.Id == DALS.Id))
            {
                throw new ObjectExistsInListException("Station");
            }
            stations.ToList().Add(DALS);
            DL.XMLTools.SaveListToXMLSerializer<Station>(stations, dir + stationFilePath);
        }
        public void AddDroneDAL(Drone DALD)
        {
            IEnumerable<Drone> drones = DL.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + droneFilePath);
            if (!drones.Any(drone => drone.Id == DALD.Id))
            {
                throw new ObjectExistsInListException("Drone");
            }
            drones.ToList().Add(DALD);
            DL.XMLTools.SaveListToXMLSerializer<Drone>(drones, dir + droneFilePath);
        }
        public void AddCustomerDAL(Customer DALC)
        {
            IEnumerable<Customer> customers = DL.XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath);
            if (!customers.Any(customer => customer.Id == DALC.Id))
            {
                throw new ObjectExistsInListException("Customer");
            }
            customers.ToList().Add(DALC);
            DL.XMLTools.SaveListToXMLSerializer<Customer>(customers, dir + customerFilePath);
        }
        public void AddParcelDAL(Parcel DALP)
        {
            IEnumerable<Parcel> parcels = DL.XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);
            parcels.ToList().Add(DALP);
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(parcels, dir + parcels);
        }
        public Station returnStation(int StationIdS)
        {
            IEnumerable<Station> stations = DL.XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + stationFilePath);
            return stations.ToList().First(station => station.Id == StationIdS);
        }
        public Drone returnDrone(int DroneIdS)
        {
            IEnumerable<Drone> drones = DL.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + droneFilePath);
            return drones.ToList().First(drone => drone.Id == DroneIdS);
        }
        public Customer returnCustomer(int CustomerIdS)
        {
            IEnumerable<Customer> customers = DL.XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath);
            return customers.ToList().First(customer => customer.Id == CustomerIdS);
        }
        public Employee returnEmployee(int idE)
        {
            IEnumerable<Employee> employees = DL.XMLTools.LoadListFromXMLSerializer<DO.Employee>(dir + employeeFilePath);
            return employees.ToList().First(employee => employee.Id == idE);
        }
        public Parcel returnParcel(int ParcelIdS)
        {
            IEnumerable<Parcel> parcels = DL.XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);
            return parcels.ToList().First(parcel => parcel.Id == ParcelIdS);
        }
        public Parcel returnParcelByDroneId(int DroneIdS)
        {
            IEnumerable<Parcel> parcels = DL.XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);
            return parcels.ToList().First(parcel => parcel.DroneId == DroneIdS);
        }
    }
}
