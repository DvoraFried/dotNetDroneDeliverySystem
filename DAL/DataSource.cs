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
        public static List<DroneChargeDAL> MyDroneCharges = new List<DroneChargeDAL>();

        //satandart drone speed per hour is 120 kilometers
        public class Config
        {
            public static double available = 0.0003;
            public static double carryLightWeight = 0.0005;
            public static double carrymediumWeight = 0.001;
            public static double carryHeavyWeight = 0.0015;
            public static double DroneLoadingRate = 43.3;
        }
        public static void Initialize()
        {
            Random rnd = new Random();
            for (int i = 1; i < 11; i++)
            {
                StationDAL stationDAL = new StationDAL() { Id = i, Name = "station" + i.ToString(), EmptyChargeSlots = rnd.Next(5, 15), Longitude = rnd.Next(0, 24), Latitude = rnd.Next(0, 180), DronesInCharging = 0 };
                MyBaseStations.Add(stationDAL);
            }
            for (int i = 1; i < 8; i++)
            {
                DroneDAL droneDAL = new DroneDAL() { Id = i, Model = "Model" + i.ToString(), MaxWeight = WeightCategories.light, Battery = rnd.Next(60, 100) };
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
                CustomerDAL customerDAL = new CustomerDAL() { Id = rnd.Next(100000000, 1000000000), Name = "customer " + i.ToString(), Phone = rnd.Next(5830000, 60000000).ToString(), Longitude = rnd.Next(0, 24), Latitude = rnd.Next(0, 180) };
                MyCustomers.Add(customerDAL);
            }

            for (int i = 1; i < 11; i++)
            {
                int senderId = MyCustomers[rnd.Next(0, 13)].Id;
                int targetId = MyCustomers[rnd.Next(0, 13)].Id;
                ParcelDAL parcel = new ParcelDAL() { Id = i, SenderId = senderId, TargetId = targetId, Weight = WeightCategories.light, Priority = (Priorities)rnd.Next(0, 3), DroneId = -1, Requested = DateTime.Now, Delivered = null, PickUp = null, Scheduled = null };
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

        }
    }

}