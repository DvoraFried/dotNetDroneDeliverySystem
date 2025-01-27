﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class DroneInCharge_pl
    {
        public DroneInCharge_pl(Drone_pl drone)
        {
            Id = drone.Id;
            BatteryStatus = drone.BatteryStatus;
            enterTime = DateTime.Now;
        }
        public DroneInCharge_pl(BO.DroneInCharge drone)
        {
            Id = drone.Id;
            BatteryStatus = drone.BatteryStatus;
            enterTime = drone.EnterTime;
        }
        public override string ToString()
        {
            return $"\nID: {Id}\nBattery Status: {BatteryStatus}\n";
        }
        public DateTime enterTime;
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
    }
}

