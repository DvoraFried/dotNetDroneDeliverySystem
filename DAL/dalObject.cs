﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;

namespace DalObject
{
    public class DalObject : IDAL.DO.IDAL
    {
        DalObject() { /*DataSource.Initialize();*/ }
        static Random rnd = new Random();
        //=====================================================================
        //                     1. class add - add function
        //=====================================================================

        //=====================================================================
        //the function addstation render information for one station
        //=====================================================================
        public void AddStation(double LongitudeS, double LatitudeS, int ChargeSlotsS = 2)
        {
            StationDAL addS = new StationDAL();
            addS.Id = DataSource.MyBaseStations.Count;
            addS.Name = "station" + DataSource.MyBaseStations.Count.ToString();
            addS.ChargeSlots = ChargeSlotsS;
            addS.Longitude = LongitudeS;
            addS.Latitude = LongitudeS;
            DataSource.MyBaseStations.Add(addS);
        }

        //=====================================================================
        //the function adddrone render information for one drone 
        //=====================================================================
        public void AddDrone(WeightCategories weightS)
        {
            DroneDAL addD = new DroneDAL();
            addD.Id = DataSource.MyDrones.Count;
            addD.Model = "model " + DataSource.MyDrones.Count.ToString();
            addD.MaxWeight = weightS;
            DataSource.MyDrones.Add(addD);
        }

        //=====================================================================
        //the function addcustomer render information for one customer
        //=====================================================================
        public void AddCustomer(int idS, string nameS, string phoneS, double longitudeS, double latitudeS)
        {
            CustomerDAL addC = new CustomerDAL();
            addC.Id = idS;
            addC.Name = nameS;
            addC.Phone = phoneS;
            addC.Longitude = longitudeS;
            addC.Latitude = latitudeS;
            DataSource.MyCustomers.Add(addC);
        }
        //=====================================================================
        //the function addparcel render information for one parcel
        //=====================================================================
        public void AddParcel(int senderIdS, int targetIdS, WeightCategories weightS, Priorities priorityS)
        {
            ParcelDAL addP = new ParcelDAL();
            addP.Id = DataSource.MyParcel.Count;
            addP.SenderId = senderIdS;
            addP.TargetId = targetIdS;
            addP.Weight = weightS;
            addP.Priority = priorityS;
            addP.Requested = DateTime.Now;
            addP.DroneId = -1;
            DataSource.MyParcel.Add(addP);
        }
        //=====================================================================
        //                     2. class update - update functions 
        //=====================================================================
        public void Scheduled(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
            DroneDAL setD = DataSource.MyDrones.First(drone => drone.MaxWeight >= upP.Weight);
            upP.DroneId = setD.Id;
            upP.Scheduled = DateTime.Now;
            DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void PickUp(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
            upP.PickUp = DateTime.Now;
            DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void Delivered(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS);
            upP.Delivered = DateTime.Now;
            DataSource.MyParcel[DataSource.MyParcel.IndexOf(DataSource.MyParcel.First(parcel => parcel.Id == parcelIdS))] = upP;
        }
        //=====================================================================
        //the function is not requrierd in this targil. yai!
        //=====================================================================
        public void Charge(int DroneIdS, int StationIdS)
        {
        }
        //=====================================================================
        //the function is not requrierd in this targil. yai!
        //=====================================================================
        public void releaseCharge(int DroneIdS)
        {
        }
        //=====================================================================
        //                     3. class returnObject - return functions 
        //=====================================================================

        public StationDAL returnStation(int StationIdS)
        {
            return DataSource.MyBaseStations.First(station => station.Id == StationIdS);
        }

        public DroneDAL returnDrone(int DroneIdS)
        {
            return DataSource.MyDrones.First(drone => drone.Id == DroneIdS);
        }

        public CustomerDAL returnCustomer(int CustomerIdS)
        {
            return DataSource.MyCustomers.First(customer => customer.Id == CustomerIdS);
        }

        public ParcelDAL returnParcel(int ParcelIdS)
        {
            return DataSource.MyParcel.First(parcel => parcel.Id == ParcelIdS);
        }
        //=====================================================================
        //             4. class returnArrayObject - return array
        //=====================================================================

        public IEnumerable<StationDAL> returnStationArray()
        {
            foreach (StationDAL element in DataSource.MyBaseStations) { yield return element; }
        }

        public IEnumerable<DroneDAL> returnDroneArray()
        {
            foreach (DroneDAL element in DataSource.MyDrones) { yield return element; }
        }

        public IEnumerable<CustomerDAL> returnCustomerArray()
        {
            foreach (CustomerDAL element in DataSource.MyCustomers) { yield return element; }
        }

        public IEnumerable<ParcelDAL> returnParcelArray()
        {
            foreach (ParcelDAL element in DataSource.MyParcel) { yield return element; }
        }
        //=====================================================================
        //returns a list of not scheduled parcels
        //=====================================================================
        public IEnumerable<ParcelDAL> returnNotScheduledParcel()
        {
            foreach (ParcelDAL element in DataSource.MyParcel) { if (element.DroneId == -1) yield return element; }
        }
        //=====================================================================
        //returns a list of station with empty cherge slots
        //=====================================================================
        public IEnumerable<StationDAL> returnStationWithChargeSlots()
        {
            foreach (StationDAL element in DataSource.MyBaseStations) { if (element.ChargeSlots > 0) yield return element; }
        }

        public double[] powerRequest()
        {
            double[] arr = new double[5];
            arr[0] = ((double)IDAL.DO.DroneStatuses.empty);
            arr[1] = ((double)IDAL.DO.WeightCategories.light);
            arr[2] = ((double)IDAL.DO.WeightCategories.medium);
            arr[3] = ((double)IDAL.DO.WeightCategories.heavy);
            //arr[4] = //קצב טעינה - ראו בהמשך
            return arr;
        }
    }
}
