﻿using System;
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
    internal sealed class DalXml : IDal
    {
        internal static DalXml instance = null;
        private static readonly object padLock = new object();

       // static readonly IDal instance = new DalXml();
        //public static IDal Instance { get => instance; }
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
        public DalXml()
        {
            DataSource.Initialize();

            if (!File.Exists(dir + customerFilePath))
                DL.XMLTools.SaveListToXMLSerializer<Customer>(DataSource.MyCustomers, dir + customerFilePath);
            
            if (!File.Exists(dir + dronesInChargeFilePath))
                DL.XMLTools.SaveListToXMLSerializer<DroneCharge>(DataSource.MyDroneCharges, dir + dronesInChargeFilePath);
            
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
            List<Station> stations = DL.XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + stationFilePath).ToList();
            if(stations.Any(station => station.Id == DALS.Id))
            {
                throw new ObjectExistsInListException("Station");
            }
            stations.Add(DALS);
            DL.XMLTools.SaveListToXMLSerializer<Station>(stations, dir + stationFilePath);
        }
        public void AddDroneDAL(Drone DALD)
        {
            IEnumerable<Drone> drones = DL.XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + droneFilePath);
            if (drones.Any(drone => drone.Id == DALD.Id))
            {
                throw new ObjectExistsInListException("Drone");
            }
            drones.ToList().Add(DALD);
            DL.XMLTools.SaveListToXMLSerializer<Drone>(drones, dir + droneFilePath);
        }
        public void AddCustomerDAL(Customer DALC)
        {
            IEnumerable<Customer> customers = DL.XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath);
            if (customers.Any(customer => customer.Id == DALC.Id))
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
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(parcels, dir + parcelFilePath);
        }
        public void Charge(DroneCharge DALDC)
        {
            IEnumerable<DroneCharge> drones = DL.XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dir + dronesInChargeFilePath);
            drones.ToList().Add(DALDC);
            DL.XMLTools.SaveListToXMLSerializer<DroneCharge>(drones, dir + dronesInChargeFilePath);
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
        public void ReplaceStationById(Station DALS)
        {
            IEnumerable<Station> stationList = DL.XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
            if (!stationList.Any(t => t.Id == DALS.Id))
            {
                throw new Exception("DL: station with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            stationList.ToList()[stationList.ToList().IndexOf(stationList.First(p => p.Id == DALS.Id))] = DALS;
            DL.XMLTools.SaveListToXMLSerializer<Station>(stationList, dir + stationFilePath);
        }
        public IEnumerable<Station> returnStationArray()
        {
            IEnumerable<Station> stationList = DL.XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
            foreach (Station element in stationList) { yield return element; }
        }
        public IEnumerable<Drone> returnDroneArray()
        {
            IEnumerable<Drone> droneList = DL.XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
            foreach (Drone element in droneList) { yield return element; }
        }
        public IEnumerable<Customer> returnCustomerArray()
        {
            IEnumerable<Customer> customerList = DL.XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
            foreach (Customer element in customerList) { yield return element; }
        }
        public IEnumerable<Employee> returnEmployeeArray()
        {
            IEnumerable<Employee> employeeList = DL.XMLTools.LoadListFromXMLSerializer<Employee>(dir + employeeFilePath);
            foreach (Employee element in employeeList) { yield return element; }
        }
        public IEnumerable<Parcel> returnParcelArray()
        {
            IEnumerable<Parcel> parcelList = DL.XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            foreach (Parcel element in parcelList) { yield return element; }
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
        public void ReplaceDroneById(Drone DALD)
        {
            IEnumerable<Drone> droneList = DL.XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
            if (!droneList.Any(t => t.Id == DALD.Id))
            {
                throw new Exception("DL: cuxtomer with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            droneList.ToList()[droneList.ToList().IndexOf(droneList.First(p => p.Id == DALD.Id))] = DALD;
            DL.XMLTools.SaveListToXMLSerializer<Drone>(droneList, dir + droneFilePath);
        }
        public void ReplaceCustomerById(Customer DALC)
        {
            IEnumerable<Customer> customerList = DL.XMLTools.LoadListFromXMLSerializer<Customer>(dir + customerFilePath);
            if (!customerList.Any(t => t.Id == DALC.Id))
            {
                throw new Exception("DL: cuxtomer with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            customerList.ToList()[customerList.ToList().IndexOf(customerList.First(p => p.Id == DALC.Id))] = DALC;
            DL.XMLTools.SaveListToXMLSerializer<Customer>(customerList, dir + customerFilePath);
        }
        public void ReplaceParcelById(Parcel DALP)
        {
            IEnumerable<Parcel> parcelsList = DL.XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            if (!parcelsList.Any(t => t.Id == DALP.Id))
            {
                throw new Exception("DL: parcel with the same id not found...");
                //throw new SomeException("DL: Student with the same id not found...");
            }
            parcelsList.ToList()[parcelsList.ToList().IndexOf(parcelsList.First(p => p.Id == DALP.Id))] = DALP;
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(parcelsList, dir + parcelFilePath);
        }
        public void DeleteObjFromDroneCharges(int id)
        {
            throw new NotImplementedException();
        }
        public void RemoveParcelById(Parcel DALP)
        {
            IEnumerable<Parcel> parcelsList = DL.XMLTools.LoadListFromXMLSerializer<Parcel>(dir + parcelFilePath);
            if (parcelsList.Any(t => t.Id == DALP.Id))
            {
                throw new Exception("DL: parcel with the same id not found...");
                //throw new SomeException("DL: Student with the same id not found...");
            }
            Parcel parcel = parcelsList.First(t => t.Id == DALP.Id);
            parcelsList.ToList().Remove(parcel);
            DL.XMLTools.SaveListToXMLSerializer<Parcel>(parcelsList, dir + parcelFilePath);
        }

        public DroneCharge returnDroneInCharge(int idDC)
        {
            throw new NotImplementedException();
        }
    }
}
