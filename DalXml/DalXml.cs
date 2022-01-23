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

    }
}
