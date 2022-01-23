using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.IO;

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

        public void ReplaceStationById(Station DALS)
        {
            IEnumerable<Station> stationList = DL.XMLTools.LoadListFromXMLSerializer<Station>(dir + stationFilePath);
            if (!stationList.Any(t => t.Id == DALS.Id))
            {
                throw new Exception("DL: station with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            stationList.ToList()[DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(p => p.Id == DALS.Id))] = DALS;
            DL.XMLTools.SaveListToXMLSerializer<Station>(stationList, dir + stationFilePath);
        }
        public void ReplaceDroneById(Drone DALD)
        {
            IEnumerable<Drone> droneList = DL.XMLTools.LoadListFromXMLSerializer<Drone>(dir + droneFilePath);
            if (!droneList.Any(t => t.Id == DALD.Id))
            {
                throw new Exception("DL: cuxtomer with the same id not found...");
                //throw new SomeException("DL: cuxtomer with the same id not found...");
            }
            droneList.ToList()[DataSource.MyDrones.IndexOf(DataSource.MyDrones.First(p => p.Id == DALD.Id))] = DALD;
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
            customerList.ToList()[DataSource.MyCustomers.IndexOf(DataSource.MyCustomers.First(p => p.Id == DALC.Id))] = DALC;
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
            parcelsList.ToList()[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.Id == DALP.Id))] = DALP;
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

    }
}
