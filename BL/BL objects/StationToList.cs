﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationToList
    {
        #region CTOR
        public StationToList(Station station)
        {
            Id = station.Id;
            Name = station.Name;
            ChargingStationsAreOccupied = station.DronesInCharging.Count;
            AvailableChargingStations = station.ChargeSlotsBL - ChargingStationsAreOccupied;
        }
        #endregion

        #region TOSRING
        public override string ToString()
        {
            return $"---------------\nID: {Id}\nName: {Name}\nAvailable Charging Stations: {AvailableChargingStations}\nCharging Stations Are Occupied: {ChargingStationsAreOccupied}\n---------------";
        }
        #endregion

        public int Id { get; set; }
        public string  Name { get; set; }
        public int AvailableChargingStations { get; set; }
        public int ChargingStationsAreOccupied { get; set; }
    }
}
