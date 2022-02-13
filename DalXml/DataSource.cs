using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    public class DataSource
    {
        public static List<Station> MyBaseStations = new List<Station>();
        public static List<Drone> MyDrones = new List<Drone>();
        public static List<Customer> MyCustomers = new List<Customer>();
        public static List<Employee> MyEmployees = new List<Employee>();
        public static List<Parcel> MyParcels = new List<Parcel>();
        public static List<DroneCharge> MyDroneCharges=new List<DroneCharge>();

        public class Config {
            public static double available=0.0003;
            public static double carryLightWeight=0.0005;
            public static double carrymediumWeight=0.001;
            public static double carryHeavyWeight=0.0015;
            public static double DroneLoadingRate=43.3;
        }
        public static void Initialize()
        {
            Random rnd = new Random();
            for (int i = 1; i < 11; i++)
            {
                Station stationDAL = new Station() { Id = i, Name = "station" + i.ToString(), EmptyChargeSlots = rnd.Next(5, 15), Longitude = rnd.Next(0, 24), Latitude = rnd.Next(0, 180), DronesInCharging = 0 };
                MyBaseStations.Add(stationDAL);
            }
            for (int i = 1; i < 8; i++)
            {
                Drone droneDAL = new Drone() { Id = i, Model = "Model" + i.ToString(), MaxWeight = WeightCategories.light, Battery = rnd.Next(60, 100), isActive = true };
                int num = rnd.Next(0, 3);
                switch (num)
                {
                    case 1:
                        droneDAL.MaxWeight = WeightCategories.medium;
                        break;
                    case 2:
                        droneDAL.MaxWeight = WeightCategories.heavy;
                        break;
                }
                MyDrones.Add(droneDAL);
            }

            for (int i = 0; i < 13; i++)
            {
                Customer customerDAL = new Customer() { Id = rnd.Next(100000000, 1000000000), Name = "customer " + i.ToString(), Phone = rnd.Next(5830000, 60000000).ToString(), Longitude = rnd.Next(0, 24), Latitude = rnd.Next(0, 180), isActive = true };
                MyCustomers.Add(customerDAL);
            }

            for (int i = 1; i < 11; i++)
            {
                int senderId = MyCustomers[rnd.Next(0, 13)].Id;
                int targetId;
                do {
                    targetId = MyCustomers[rnd.Next(0, 13)].Id;
                } while (targetId == senderId);

                Parcel parcel = new Parcel() { Id = i, SenderId = senderId, TargetId = targetId, Weight = WeightCategories.light, Priority = (Priorities)rnd.Next(0, 3), DroneId = -1, isActive = true, Requested = DateTime.Now, Delivered = null, PickUp = null, Scheduled = null };
                int num = rnd.Next(0, 3);
                switch (num)
                {
                    case 1:
                        parcel.Weight = WeightCategories.medium;
                        break;
                    case 2:
                        parcel.Weight = WeightCategories.heavy;
                        break;
                }
                MyParcels.Add(parcel);
            }

            Employee manager1 = new Employee() { Id = 213570302, Name = "hadas", Manager = true };
            MyEmployees.Add(manager1);
            Employee manager2 = new Employee() { Id = 212628721, Name = "dvora", Manager = true };
            MyEmployees.Add(manager2);
        }
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