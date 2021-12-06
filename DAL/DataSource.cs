using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
  
    {
        public static List<StationDAL> MyBaseStations = new List<StationDAL>();
        public static List<DroneDAL> MyDrones = new List<DroneDAL>();
        public static List<CustomerDAL> MyCustomers = new List<CustomerDAL>();
        public static List<ParcelDAL> MyParcels = new List<ParcelDAL>();
        public static List<DroneChargeDAL> MyDroneCharges=new List<DroneChargeDAL> ();

        //satandart drone speed per hour is 120 kilometers
        public class Config {
            public static double available=0.03;
            public static double carryLightWeight=0.05;
            public static double carrymediumWeight=0.1;
            public static double carryHeavyWeight=0.15;
            public static double DroneLoadingRate=50;
        }
/*        public static void Initialize()
        {
            Random rnd = new Random();
            for (int i = 0; i < 2;i++) {
                DalObject.AddStationDAL(rnd.Next(0,24), rnd.Next(0, 180), rnd.Next(2, 5));
            }

            for (int i = 0; i < 5; i++) {
                int num = rnd.Next(0, 3);
                switch (num) {
                    case 0:
                        DalObject.AddDrone(WeightCategories.light);
                        break;
                    case 1:
                        DalObject.AddDrone(WeightCategories.medium);
                        break;
                    case 2:
                        DalObject.AddDrone(WeightCategories.heavy);
                        break;
                }                
            }

            for (int i = 0; i < 10; i++) {
                DalObject.Add.AddCustomer(MyCustomers.Count, "customer" + (MyCustomers.Count).ToString(),
                rnd.Next(5000000, 60000000).ToString(), rnd.Next(0, 24), rnd.Next(0, 180));
            }

            for (int i = 0; i < 10; i++) {
                WeightCategories weightS;
                int num = rnd.Next(0, 3);
                switch (num) {
                    case 0:
                        weightS = WeightCategories.light;
                        break;
                    case 1:
                        weightS = WeightCategories.medium;
                        break;
                    case 2:
                        weightS = WeightCategories.heavy;
                        break;
                }

                Priorities priorityS;
                num = rnd.Next(0, 3);
                switch (num) {
                    case 0:
                        DalObject.AddParcel(MyCustomers[rnd.Next(1, (MyCustomers.Count) + 1)].Id,
                        MyCustomers[rnd.Next(1, (MyCustomers.Count) + 1)].Id, WeightCategories.medium, Priorities.emergency);         
                        break;
                    case 1:
                        DalObject.AddParcel(MyCustomers[rnd.Next(1, (MyCustomers.Count) + 1)].Id,
                        MyCustomers[rnd.Next(1, (MyCustomers.Count) + 1)].Id, WeightCategories.medium, Priorities.rapid);
                        break;
                    case 2:
                        DalObject.AddParcel(MyCustomers[rnd.Next(1, (MyCustomers.Count) + 1)].Id,
                        MyCustomers[rnd.Next(1, (MyCustomers.Count) + 1)].Id, WeightCategories.medium, Priorities.usual);
                        break;
                }
            }
            
        }*/
    }

}

















































/*
static void Initialize()
{
    Random rnd = new Random();
    //=====================================================================
    //rendering information for 2 Stations
    //=====================================================================
    for (int i = 0; i < 2; i++)
    {
        Config.StationsIndex++;
        MyBaseStations[i].Id = Config.StationsIndex + 1;
        MyBaseStations[i].Name = "station" + (Config.StationsIndex + 1).ToString();
        MyBaseStations[i].ChargeSlots = rnd.Next(2, 4);
        MyBaseStations[i].Longitude = rnd.Next(0, 24);
        MyBaseStations[i].Latitude = rnd.Next(0, 180);
    }
    //=====================================================================           
    //rendering information for five drones
    //=====================================================================
    for (int i = 0; i < 5; i++)
    {
        Config.DronesIndex++;
        MyDrones[i].Id = Config.DronesIndex;
        MyDrones[i].Model = "model " + (Config.DronesIndex).ToString();
        int num = rnd.Next(0, 3);
        switch (num)
        {
            case 0:
                MyDrones[i].Weight = WeightCategories.light;
                break;
            case 1:
                MyDrones[i].Weight = WeightCategories.medium;
                break;
            case 2:
                MyDrones[i].Weight = WeightCategories.heavy;
                break;
        }

        MyDrones[i].Battery = rnd.Next(10, 101);

        num = rnd.Next(0, 3);
        switch (num)
        {
            case 0:
                MyDrones[i].Status = DroneStatuses.empty;
                break;
            case 1:
                MyDrones[i].Status = DroneStatuses.maintenance;
                break;
            case 2:
                MyDrones[i].Status = DroneStatuses.Shipping;
                break;
        }
    }
    //=====================================================================            
    //rendering information for ten customers 
    //=====================================================================
    for (int i = 0; i < 10; i++)
    {
        Config.CustomersIndex++;
        MyCustomers[i].Id = Config.CustomersIndex;
        MyCustomers[i].Name = "customer" + (Config.CustomersIndex).ToString();
        MyCustomers[i].Phone = rnd.Next(5000000, 60000000).ToString();
        MyCustomers[i].Longitude = rnd.Next(0, 24);
        MyCustomers[i].Latitude = rnd.Next(0, 180);
    }
    //=====================================================================            
    //rendering information for 10 packages
    //=====================================================================
    for (int i = 0; i < 10; i++)
    {
        Config.ParcelIndex++;
        MyParcel[i].id = Config.ParcelIndex;
        MyParcel[i].SenderId = rnd.Next(1, (Config.CustomersIndex) + 1);
        MyParcel[i].TargetId = rnd.Next(1, (Config.CustomersIndex) + 1);
        int num = rnd.Next(0, 3);
        switch (num)
        {
            case 0:
                MyParcel[i].Weight = WeightCategories.light;
                break;
            case 1:
                MyParcel[i].Weight = WeightCategories.medium;
                break;
            case 2:
                MyParcel[i].Weight = WeightCategories.heavy;
                break;
        }

        num = rnd.Next(0, 3);
        switch (num)
        {
            case 0:
                MyParcel[i].Priority = Priorities.emergency;
                break;
            case 1:
                MyParcel[i].Priority = Priorities.rapid;
                break;
            case 2:
                MyParcel[i].Priority = Priorities.usual;
                break;
        }
        MyParcel[i].DroneId = 0;
        //~~~~~~~~~~~~~~~~~~~~~~~`there is times to write here at this line but i'm not sure what to do:(~~~~~~~~~~~~~~~
    }*/