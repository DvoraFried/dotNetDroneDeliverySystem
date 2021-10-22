using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        static Random rnd = new Random();
//=====================================================================
//                     1. class add - add function
//=====================================================================
        public class Add
        {
            //=====================================================================
            //the function addstation render information for one station
            //=====================================================================
            public static void AddStation()
            {
                
                int idx = DataSource.Config.StationsIndex;
                DataSource.MyBaseStations[idx].Id = DataSource.Config.StationsIndex ;//station id number
                DataSource.MyBaseStations[idx].Name = "station" + (DataSource.Config.StationsIndex ).ToString();//station name 
                DataSource.MyBaseStations[idx].ChargeSlots = DalObject.rnd.Next(2, 5);//number of charge slots for the station
                DataSource.MyBaseStations[idx].Longitude = DalObject.rnd.Next(0, 24);//the station location 
                DataSource.MyBaseStations[idx].Latitude = DalObject.rnd.Next(0, 180);//the station location
                DataSource.Config.StationsIndex++;//update the index
            }

            //=====================================================================
            //the function adddrone render information for one drone 
            //=====================================================================
            public static void AddDrone(WeightCategories weightS)
            {
                int idx = DataSource.Config.DronesIndex;
                DataSource.MyDrones[idx].Id = DataSource.Config.DronesIndex;//drone id
                DataSource.MyDrones[idx].Model = "model " + (DataSource.Config.DronesIndex).ToString();//drone model
                int num = rnd.Next(0, 3);
                DataSource.MyDrones[idx].Weight = WeightCategories.light;//weight category
                DataSource.MyDrones[idx].Battery = 100;//the battery will be 100%
                DataSource.MyDrones[idx].Status = DroneStatuses.empty;//and the drone is in "empty" status 
                DataSource.Config.DronesIndex++;//update the index
            }

            //=====================================================================
            //the function addcustomer render information for one customer
            //=====================================================================
            public static void AddCustomer(int idS,string nameS,string phoneS,double longitudeS,double latitudeS)
            {
                int idx = DataSource.Config.CustomersIndex;
                DataSource.Config.CustomersIndex++;
                DataSource.MyCustomers[idx].Id = idS;//customer id number
                DataSource.MyCustomers[idx].Name = nameS;//customer name
                DataSource.MyCustomers[idx].Phone = phoneS;//customner phone
                DataSource.MyCustomers[idx].Longitude = longitudeS;//the customer location
                DataSource.MyCustomers[idx].Latitude = latitudeS;//the customer location
                DataSource.Config.CustomersIndex++;//updating index

            }
            //=====================================================================
            //the function addparcel render information for one parcel
            //=====================================================================
            public static void AddParcel(int senderIdS,int targetIdS,WeightCategories weightS,Priorities priorityS)
            {
                int idx = DataSource.Config.ParcelIndex;
                DataSource.MyParcel[idx].Id = DataSource.Config.ParcelIndex;//parcel id number
                DataSource.MyParcel[idx].SenderId = senderIdS;//render a sender id 
                DataSource.MyParcel[idx].TargetId = targetIdS;//render a sender id
                DataSource.MyParcel[idx].Weight = weightS;//the weight
                DataSource.MyParcel[idx].Priority = priorityS;//the priority
                DataSource.MyParcel[idx].Requested = DateTime.Now;
                DataSource.MyParcel[idx].DroneId = -1;//i stil have no idea what is it
                DataSource.Config.ParcelIndex++;

            }
        }
//=====================================================================
//                     2. class update - update functions 
//=====================================================================
    }
    public class Update
    {
        //=====================================================================
        //the function Schedule drone to parcel
        //=====================================================================
        public static void Scheduled(int parcelIdS)
        {
            WeightCategories parcelWeight;
            for (int i = 0; i < DataSource.Config.ParcelIndex; i++)
            {
                if (DataSource.MyParcel[i].Id== parcelIdS)//finding the wanton parcel
                {
                    parcelWeight = DataSource.MyParcel[i].Weight;
                    for(int j = 0; j < DataSource.Config.DronesIndex; i++)//finding an empty and suited drone to use 
                    {
                        if (DataSource.MyDrones[j].MaxWeight == parcelWeight&& DataSource.MyDrones[j].Status != DroneStatuses.Shipping){
                            DataSource.MyDrones[j].Status = DroneStatuses.Shipping;
                            DataSource.MyParcel[i].DroneId = DataSource.MyDrones[j].Id;
                            DataSource.MyParcel[i].Scheduled= DateTime.Now;
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("there is no empty drone you can use :(");
        }
        //=====================================================================
        //the function 
        //=====================================================================
        public static void Scheduled()
        {

            
        }


    }

}
