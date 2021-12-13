﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;

namespace IBL.BO
{
    public class StationBL
    {
        public StationBL(int id, string name, Position p, int chargS, List<DroneBL> drones)
        {
            SetId(id);
            NameBL = name;
            Position = p;
            ChargeSlotsBL = chargS;
            foreach (DroneBL drone in drones)
            {
                if (drone.CurrentPosition.Longitude == p.Longitude && drone.CurrentPosition.Latitude == p.Latitude && drone.DroneStatus == EnumBL.DroneStatusesBL.maintenance) {
                    DroneInChargeBL d = new DroneInChargeBL(drone);
                    DronesInCharging.Add(d);
                }
            }
        }
        private int idBL;
        public void SetId(int idS)
        {
            if (idS <= 0)
            {
                throw new UnValidIdException(idS, "station");
            }
            idBL = idS;
        }
        public override string ToString()
        {
            if (DronesInCharging != null)
            {
                Console.WriteLine("DronesInCharging: ");
                foreach (DroneInChargeBL drone in DronesInCharging)
                {
                    Console.WriteLine(drone.ToString());
                }
                return $"ID: {GetIdBL()}\nName: {NameBL}\nPosition - {Position.ToString()}\nDrones in Charging: {DronesInCharging.ToString()}";
            }
            return $"ID: {GetIdBL()}\nName: {NameBL}\nPosition - {Position.ToString()}\nDrones in Charging: No Drones";
        }

        public int GetIdBL() { return idBL; }
        public string NameBL { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position Position { get; set; }
        public List<DroneInChargeBL> DronesInCharging { get; set; }
    }
}