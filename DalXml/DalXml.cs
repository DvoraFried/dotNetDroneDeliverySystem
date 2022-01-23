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
        public void Scheduled(int parcelIdS)
        {
            Parcel upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            Drone setD = DataSource.MyDrones.First(drone => drone.MaxWeight >= upP.Weight);
            upP.DroneId = setD.Id;
            upP.Scheduled = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void PickUp(int parcelIdS)
        {
            Parcel upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            upP.PickUp = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void Delivered(int parcelIdS)
        {
            Parcel upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            upP.Delivered = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }
        public void Charge(DroneCharge DALDC)
        {
            DataSource.MyDroneCharges.Add(DALDC);
        }
        public void releaseCharge(DroneCharge Drone)
        {
            DataSource.MyDroneCharges.Remove(Drone);
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
