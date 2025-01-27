﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        #region CTOR
        public DroneToList(DalApi.IDal dalOB, Drone drone)
        {
            Id = drone.Id;
            Model = drone.Model;
            Weight = drone.MaxWeight;
            BatteryStatus = drone.BatteryStatus;
            DroneStatus = drone.DroneStatus;
            CurrentPosition = drone.CurrentPosition;
            ParcelNun = dalOB.GetParcelList().ToList().Any(parcel => parcel.DroneId == Id) ?
                        dalOB.GetParcelList().First(parcel => parcel.DroneId == Id).Id : 0;
        }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            string parcelNum = ParcelNun != 0 ? ParcelNun.ToString() : "not exist";
            return $"============================\nID: {Id}\nModel: {Model}\nMax Weight: {Weight}\nBattery Status: {BatteryStatus}\nDrone Status: {DroneStatus}\nPosition: {CurrentPosition.ToString()}\nParcel Number: {parcelNum}\n============================";
        }
        #endregion

        public int Id { get; set; }
        public string Model { get; set; }
        Enum.WeightCategoriesBL Weight { get; set; }
        public double BatteryStatus { get; set; }
        Enum.DroneStatusesBL DroneStatus { get; set; }
        Position CurrentPosition { get; set; }
        public int ParcelNun { get; set; }
    }
}