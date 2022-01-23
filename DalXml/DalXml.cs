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
    }
}
