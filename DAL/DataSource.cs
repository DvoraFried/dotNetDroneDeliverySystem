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
        static internal Station[] MyBaseStations = new Station[5];
        static internal Drone[] MyDrones = new Drone[10];
        static internal Customer[] MyCustomers = new Customer[100];
        static internal Parcel[] MyParcel = new Parcel[1000];
        internal class Config
        {
            static internal int StationsIndex = 0;
            static internal int DronesIndex=0;
            static internal int CustomersIndex = 0;
            static internal int ParcelIndex = 0;
            static internal int IdNumber;
        }

        static void Initialize()
        {
            for (int i = 0; i < 2;i++)
            {
                DalObject.Add.AddStation();
            }

            for (int i = 0; i < 5; i++)
            {
                DalObject.Add.AddDrone();
            }

            for (int i = 0; i < 10; i++)
            {
                DalObject.Add.AddCustomer();
            }

            for (int i = 0; i < 10; i++)
            {
                DalObject.Add.AddParcel();


            }
            
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