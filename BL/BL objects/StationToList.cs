using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class StationToList
    {
        public StationToList(StationBL station)
        {
            Id = station.GetIdBL();
            Name = station.NameBL;
            ChargingStationsAreOccupied = station.DronesInCharging == null ? 0 : station.DronesInCharging.Count;
            AvailableChargingStations = station.ChargeSlotsBL - ChargingStationsAreOccupied;
        }
        public override string ToString()
        {
            return $"---------------\nID: {Id}\nName: {Name}\nAvailable Charging Stations: {AvailableChargingStations}\nCharging Stations Are Occupied: {ChargingStationsAreOccupied}\n---------------";
        }
        public int Id { get; set; }
        public string  Name { get; set; }
        public int AvailableChargingStations { get; set; }
        public int ChargingStationsAreOccupied { get; set; }
    }
}
