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
                int cS= DalObject.rnd.Next(2, 5);
                int idx = DataSource.Config.StationsIndex;
                DataSource.MyBaseStations[idx].Id = DataSource.Config.StationsIndex;//station id number
                DataSource.MyBaseStations[idx].Name = "station" + (DataSource.Config.StationsIndex).ToString();//station name 
                DataSource.MyBaseStations[idx].ChargeSlots = cS; //number of charge slots for the station
/*                for(int i=DataSource.Config.ChargeSlotsIndex;i< DataSource.Config.ChargeSlotsIndex + cS;)
                {
                    DataSource.MyChargeSlots[i].StationId = DataSource.MyBaseStations[idx].Id;
                }*/
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
                DataSource.MyDrones[idx].MaxWeight = weightS;//weight category
                DataSource.MyDrones[idx].Battery = 100;//the battery will be 100%
                DataSource.MyDrones[idx].Status = DroneStatuses.empty;//and the drone is in "empty" status 
                DataSource.Config.DronesIndex++;//update the index
            }

            //=====================================================================
            //the function addcustomer render information for one customer
            //=====================================================================
            public static void AddCustomer(int idS, string nameS, string phoneS, double longitudeS, double latitudeS)
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
            public static void AddParcel(int senderIdS, int targetIdS, WeightCategories weightS, Priorities priorityS)
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
                    if (DataSource.MyParcel[i].Id == parcelIdS)//finding the wanton parcel
                    {
                        parcelWeight = DataSource.MyParcel[i].Weight;
                        for (int j = 0; j < DataSource.Config.DronesIndex; i++)//finding an empty and suited drone to use 
                        {
                            if (DataSource.MyDrones[j].MaxWeight == parcelWeight && DataSource.MyDrones[j].Status == DroneStatuses.empty)
                            {
                                DataSource.MyDrones[j].Status = DroneStatuses.maintenance;
                                DataSource.MyParcel[i].DroneId = DataSource.MyDrones[j].Id;
                                DataSource.MyParcel[i].Scheduled = DateTime.Now;
                                return;
                            }
                        }
                    }
                }
            }
            //=====================================================================
            //the function 
            //=====================================================================
            public static void PickUp(int parcelIdS, int SenderlIdS)
            {
                for (int i = 0; i < DataSource.Config.ParcelIndex; i++)
                {
                    if (DataSource.MyParcel[i].Id == parcelIdS && DataSource.MyParcel[i].SenderId == SenderlIdS)
                    {
                        for (int j = 0; j < DataSource.Config.DronesIndex; i++)
                        {
                            if (DataSource.MyParcel[i].DroneId == DataSource.MyDrones[j].Id)
                            {

                                DataSource.MyDrones[j].Status = DroneStatuses.Shipping;
                                DataSource.MyParcel[i].PickUp = DateTime.Now;
                                return;
                            }
                        }
                    }
                }
            }
            //=====================================================================
            //the function 
            //=====================================================================
            public static void Delivered(int parcelIdS, int TargetIdS)
            {
                for (int i = 0; i < DataSource.Config.ParcelIndex; i++)
                {
                    if (DataSource.MyParcel[i].Id == parcelIdS && DataSource.MyParcel[i].TargetId == TargetIdS)
                    {
                        for (int j = 0; j < DataSource.Config.DronesIndex; i++)
                        {
                            if (DataSource.MyParcel[i].DroneId == DataSource.MyDrones[j].Id)
                            {
                                DataSource.MyDrones[j].Status = DroneStatuses.empty;
                                DataSource.MyParcel[i].Delivered = DateTime.Now;
                                return;
                            }
                        }
                    }
                }
            }
            //=====================================================================
            //the function 
            //=====================================================================
            public static void Charge(int DroneIdS,int StationIdS)
            {
                for (int i = 0; i < DataSource.Config.DronesIndex; i++)
                {
                    if (DataSource.MyDrones[i].Id==DroneIdS)
                    {
                        DataSource.MyDrones[i].Status = DroneStatuses.maintenance;
                        for(int j= 0; j < DataSource.Config.StationsIndex; i++)
                        {
                            if (DataSource.MyBaseStations[j].Id == StationIdS)
                            {
                                while (DataSource.MyBaseStations[j].ChargeSlots == 0)
                                {
                                    Console.WriteLine("there are no empty charch slot in this station' please type another station id");
                                    int input = Convert.ToInt32(Console.ReadLine());
                                    StationIdS =input;
                                }
                                DataSource.MyChargeSlots[DataSource.Config.ChargeSlotsIndex].StationId = DataSource.MyBaseStations[j].Id;
                                DataSource.MyChargeSlots[DataSource.Config.ChargeSlotsIndex].DroneId = DroneIdS;
                                DataSource.Config.ChargeSlotsIndex++;
                                DataSource.MyBaseStations[j].ChargeSlots--;
                            }
                        }
                    
                    }
                }
            }
            //=====================================================================
            //the function 
            //=====================================================================
            public static void releaseCharge(int DroneIdS)
            {
                for(int i = 0; i < DataSource.Config.ChargeSlotsIndex; i++)
                {
                    if (DataSource.MyChargeSlots[i].DroneId == DroneIdS)
                    {
                        for(int j = 0; j < DataSource.Config.StationsIndex; j++)
                        {
                            if(DataSource.MyBaseStations[j].Id== DataSource.MyChargeSlots[i].StationId)
                            {
                                DataSource.MyBaseStations[j].ChargeSlots++;
                                for(int k = 0; k < DataSource.Config.DronesIndex; k++)
                                {
                                    if(DataSource.MyDrones[k].Id == DroneIdS)
                                    {
                                        DataSource.MyDrones[k].Status = DroneStatuses.empty;
                                    }
                                }
                                
                            }
                        }
                    }
                }

            }
        }
        //=====================================================================
        //                     3. class returnObject - return functions 
        //=====================================================================
        public class returnObject
        {
            //=====================================================================
            //returns station or 0
            //=====================================================================
            public static IDAL.DO.Station returnStation<Station>(int StationIdS)
            {
                for (int i = 0; i < DataSource.Config.StationsIndex; i++)
                {
                    if (DataSource.MyBaseStations[i].Id == StationIdS)
                    {
                        return DataSource.MyBaseStations[i];
                    }
                }
                var defaultVal = default(IDAL.DO.Station);
                return defaultVal;
            }
            //=====================================================================
            //returns drone or 0
            //=====================================================================
            public static IDAL.DO.Drone returnDrone<Drone>(int DroneIdS)
            {
                for (int i = 0; i < DataSource.Config.DronesIndex; i++)
                {
                    if (DataSource.MyDrones[i].Id == DroneIdS)
                    {
                        return DataSource.MyDrones[i];
                    }
                }
                var defaultVal = default(IDAL.DO.Drone);
                return defaultVal;
            }
            //=====================================================================
            //returns customer or 0
            //=====================================================================
            public static IDAL.DO.Customer returnCustomer<Customer>(int CustomerIdS)
            {
                for (int i = 0; i < DataSource.Config.CustomersIndex; i++)
                {
                    if (DataSource.MyCustomers[i].Id == CustomerIdS)
                    {
                        return DataSource.MyCustomers[i];
                    }
                }
                var defaultVal = default(IDAL.DO.Customer);
                return defaultVal;
            }
            //=====================================================================
            //returns parcel or 0
            //=====================================================================
            public static IDAL.DO.Parcel returnParcel<Parcel>(int ParcelIdS)
            {
                for (int i = 0; i < DataSource.Config.ParcelIndex; i++)
                {
                    if (DataSource.MyParcel[i].Id == ParcelIdS)
                    {
                        return DataSource.MyParcel[i];
                    }
                }
                var defaultVal = default(IDAL.DO.Parcel);
                return defaultVal;
            }
        }
        //=====================================================================
        //             4. class returnArrayObject - return array
        //=====================================================================
        public class returnArrayObject
        {
            //=====================================================================
            //returns stations list
            //=====================================================================
            public static Station[] returnStationArray()
            {
                Station[] returnBaseStations = new Station[DataSource.Config.StationsIndex];
                for(int i=0;i< DataSource.Config.StationsIndex; i++)
                {
                    returnBaseStations[i] = DataSource.MyBaseStations[i];
                }
                return returnBaseStations;
            }
            //=====================================================================
            //returns drones list
            //=====================================================================
            public static Drone[] returnDroneArray()
            {
                Drone[] returnDrones = new Drone[DataSource.Config.DronesIndex];
                for (int i = 0; i < DataSource.Config.DronesIndex; i++)
                {
                    returnDrones[i] = DataSource.MyDrones[i];
                }
                return returnDrones;
            }
            //=====================================================================
            //returns customer list
            //=====================================================================
            public static Customer[] returnCustomerArray()
            {
                Customer[] returnCustomers = new Customer[DataSource.Config.CustomersIndex];
                for (int i = 0; i < DataSource.Config.CustomersIndex; i++)
                {
                    returnCustomers[i] = DataSource.MyCustomers[i];
                }
                return returnCustomers;
            }
            //=====================================================================
            //returns parcels list
            //=====================================================================
            public static Parcel[] returnParcelArray()
            {
                Parcel[] returnParcels = new Parcel[DataSource.Config.ParcelIndex];
                for (int i = 0; i < DataSource.Config.ParcelIndex; i++)
                {
                    returnParcels[i] = DataSource.MyParcel[i];
                }
                return returnParcels;
            }
            //=====================================================================
            //returns a list of not scheduled parcels
            //=====================================================================
            public static List<Parcel> returnNotScheduledParcel()
            {
                int idx = 0;
                List<Parcel> notScheduledParcel = new List<Parcel>();
                for (int i = 0; i < DataSource.Config.ParcelIndex; i++)
                {
                    if (DataSource.MyParcel[i].DroneId == null)
                    {
                        notScheduledParcel[idx] = DataSource.MyParcel[i];
                        idx++;
                    }
                }
                return notScheduledParcel;
            }
            //=====================================================================
            //returns a list of station with empty cherge slots
            //=====================================================================
            public static List<Station> returnStationWithChargeSlots()
            {
                int idx = 0;
                List<Station> stationWithChargeSlots = new List<Station>();
                for (int i = 0; i < DataSource.Config.StationsIndex; i++)
                {
                    if (DataSource.MyBaseStations[i].ChargeSlots>0)
                    {
                        stationWithChargeSlots[idx] = DataSource.MyBaseStations[i];
                        idx++;
                    }
                }
                return stationWithChargeSlots;
            }
        }
    }
}
