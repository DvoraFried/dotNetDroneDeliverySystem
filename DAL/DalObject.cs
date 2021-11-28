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

        static DalObject DOBJ;

        public static DalObject GetDOBJ
        {
            get
            {
                if (DOBJ == null)
                    DOBJ = new DalObject();
                return DOBJ;
            }
        }
        //=====================================================================
        //                     1. class add - add function
        //=====================================================================
        public void AddStationDAL(StationDAL DALS)
        {
            DataSource.MyBaseStations.Add(DALS);
        }
        public void AddDroneDAL(DroneDAL DALD)
        {
            DataSource.MyDrones.Add(DALD);
        }
        public void AddCustomerDAL(CustomerDAL DALC)
        {
            DataSource.MyCustomers.Add(DALC);
        }
        public void AddParcelDAL(ParcelDAL DALP)
        {
            DataSource.MyParcels.Add(DALP);
        }
        //=====================================================================
        //                     2. class update - update functions 
        //=====================================================================
        public void Scheduled(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            DroneDAL setD = DataSource.MyDrones.First(drone => drone.MaxWeight >= upP.Weight);
            upP.DroneId = setD.Id;
            upP.Scheduled = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void PickUp(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            upP.PickUp = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }

        public void Delivered(int parcelIdS)
        {
            ParcelDAL upP = DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS);
            upP.Delivered = DateTime.Now;
            DataSource.MyParcels[DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(parcel => parcel.Id == parcelIdS))] = upP;
        }
        public void Charge(DroneChargeDAL DALDC)

        {
            DataSource.MyDroneCharges.Add(DALDC);
        }
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
            return DataSource.MyParcels.First(parcel => parcel.Id == ParcelIdS);
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
            foreach (ParcelDAL element in DataSource.MyParcels) { yield return element; }
        }
        //=====================================================================
        //returns a list of not scheduled parcels
        //=====================================================================
        public IEnumerable<ParcelDAL> returnNotScheduledParcel()
        {
            //String.IsNullOrEmpty(element.DroneId.ToString())
            foreach (ParcelDAL element in DataSource.MyParcels) { if (element.DroneId == -1) yield return element; }
        }
        //=====================================================================
        //returns a list of station with empty cherge slots
        //=====================================================================
        public IEnumerable<StationDAL> returnStationWithChargeSlots()
        {
            foreach (StationDAL element in DataSource.MyBaseStations) { if (element.EmptyChargeSlots > 0) yield return element; }
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
        //=====================================================================
        //replace object in index
        //=====================================================================
        public void ReplaceStationByIndex(StationDAL DALS, int idx)
        {
            DataSource.MyBaseStations[idx] = DALS;
        }
        public void ReplaceDroneByIndex(DroneDAL DALD, int idx)
        {
            DataSource.MyDrones[idx] = DALD;
        }
        public void ReplaceCustomerByIndex(CustomerDAL DALC, int idx)
        {
            DataSource.MyCustomers[idx] = DALC;
        }
        public void ReplaceParcelByIndex(ParcelDAL DALP, int idx)
        {
            DataSource.MyParcels[idx] = DALP;
        }
    }
}
