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
            public static void AddCustomer()
            {
                int idx = DataSource.Config.CustomersIndex;
                DataSource.Config.CustomersIndex++;
                DataSource.MyCustomers[idx].Id = DataSource.Config.CustomersIndex;//customer id number
                DataSource.MyCustomers[idx].Name = "customer" + (DataSource.Config.CustomersIndex).ToString();//customer name
                DataSource.MyCustomers[idx].Phone = rnd.Next(5000000, 60000000).ToString();//customner phone
                DataSource.MyCustomers[idx].Longitude = rnd.Next(0, 24);//the customer location
                DataSource.MyCustomers[idx].Latitude = rnd.Next(0, 180);//the customer location
                DataSource.Config.CustomersIndex++;//updating index

            }
//=====================================================================
//the function addparcel render information for one parcel
//=====================================================================
            public static void AddParcel(WeightCategories weightS,Priorities priorityS)
            {
                int idx = DataSource.Config.ParcelIndex;
                DataSource.MyParcel[idx].id = DataSource.Config.ParcelIndex;//parcel id number
                DataSource.MyParcel[idx].SenderId = rnd.Next(1, (DataSource.Config.CustomersIndex) + 1);//render a sender id 
                DataSource.MyParcel[idx].TargetId = rnd.Next(1, (DataSource.Config.CustomersIndex) + 1);//render a sender id
                DataSource.MyParcel[idx].Weight = weightS;//the weight
                DataSource.MyParcel[idx].Priority = priorityS;//the priority
                DataSource.MyParcel[idx].DroneId = -1;//i stil have no idea what is it
                DataSource.Config.ParcelIndex++;

            }
        }
    }
    
}
