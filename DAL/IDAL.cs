﻿using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IDAL
{
        public interface IDAL
        {
            public void AddStationDAL(StationDAL DALS);
            public void AddDroneDAL(DroneDAL DALD);
            public  void AddCustomerDAL(CustomerDAL DALC);
            public  void AddParcelDAL(ParcelDAL DALP);

            public void Scheduled(int parcelIdS);
            public void PickUp(int parcelIdS);
            public void Delivered(int parcelIdS);
            public void Charge(DroneChargeDAL DALDC);

            public StationDAL returnStation(int StationIdS);
            public DroneDAL returnDrone(int DroneIdS);
            public CustomerDAL returnCustomer(int CustomerIdS);
            public ParcelDAL returnParcel(int ParcelIdS);
            public ParcelDAL returnParcelByDroneId(int DroneIdS);


            public IEnumerable<StationDAL> returnStationArray();
            public IEnumerable<DroneDAL> returnDroneArray();
            public IEnumerable<CustomerDAL> returnCustomerArray();
            public IEnumerable<ParcelDAL> returnParcelArray();

            public double[] powerRequest();

            public void ReplaceStationById(StationDAL DALS);
            public void ReplaceDroneById(DroneDAL DALD);
            public void ReplaceCustomerById(CustomerDAL DALC);
            public void ReplaceParcelById(ParcelDAL DALP);

            public void DeleteObjFromDroneCharges(int id);
        }
}