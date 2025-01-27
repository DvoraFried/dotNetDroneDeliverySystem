﻿//using DalObject;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static BO.Enum;
using static BO.Exceptions;

namespace BO
{
    public class Drone
    {
        #region CTOR
        public Drone(DalApi.IDal dalOB, int id,string model,WeightCategoriesBL maxW, DroneStatusesBL status,Position p,int stationId, bool active = true)
        {
            Random rnd = new Random();
            Id = id;
            Model = model;
            MaxWeight = maxW;
            CurrentPosition = p;
            BatteryStatus = rnd.Next(20, 41);
            DroneStatus = status;
            isActive = active;
            delivery = dalOB.GetParcelList().ToList().Any(parcel => parcel.DroneId == this.id) ?
                       new ParcelByTransfer(dalOB,  this.id)
                       : null;
            if (delivery != null) { DroneStatus = DroneStatusesBL.Shipping; }
        }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            if (delivery != null)
            {
                return $"ID: {Id}\nModel: {Model}\nMax Weight: {MaxWeight}\nBattery Status: {BatteryStatus+"%"}\nDrone Status: {DroneStatus}\nDelivery by Transfer: {delivery.Id}\nPosition {getFormattedLocationInDegree(CurrentPosition.Latitude, CurrentPosition.Longitude)}";
            }
            return $"ID: {Id}\nModel: {Model}\nMax Weight: {MaxWeight}\nBattery Status: {BatteryStatus + "%"}\nDrone Status: {DroneStatus}\nDelivery by Transfer:  Non Deliveries by Transfer\nPosition {getFormattedLocationInDegree(CurrentPosition.Latitude,CurrentPosition.Longitude)}";
        }
        #endregion

        private int id;
        #region SET-GET_ID
        public int Id 
        {
            get { return id; }
            set {
                if (value <= 0) { throw new UnValidIdException(value, "drone"); }
                id = value;
            }
        }
        #endregion

        public string Model { get; set; }
        public WeightCategoriesBL MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatusesBL DroneStatus { get; set; }
        public ParcelByTransfer delivery { get; set; }
        public Position CurrentPosition { get; set; }
        public bool isActive { get; set; }

    }
}
