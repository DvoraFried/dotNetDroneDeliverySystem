using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Station_pl
    {
        public Station_pl(int id, string name, Position_pl p, int chargS, List<Drone_pl> drones)
        {
            SetId(id);
            NameBL = name;
            Position = p;
            ChargeSlotsBL = chargS;
            DronesInCharging = new List<DroneInCharge_pl>();
            foreach (Drone_pl drone in drones)
            {
                if (drone.CurrentPosition.Longitude == p.Longitude && drone.CurrentPosition.Latitude == p.Latitude && drone.DroneStatus == Enum.DroneStatuses.maintenance)
                {
                    DronesInCharging.Add(new DroneInCharge_pl(drone));
                }
            }
        }
        private int idBL;
        public void SetId(int idS)
        {
            idBL = idS;
        }
        public override string ToString()
        {
            if (DronesInCharging != null)
            {
                Console.WriteLine("DronesInCharging: ");
                //@@@@@@@ dvora!! DronesListPl deosnt exist yet, have fun tring to figuer out what to do, just kidding, you need to a class with list for pl  .goodluck!
                foreach (DroneInCharge_pl drone in DronesInChargingPl)
                {
                    Console.WriteLine(drone.ToString());
                }
                return $"ID: {GetIdBL()}\nName: {NameBL}\nPosition - {Position.ToString()}";
            }
            return $"ID: {GetIdBL()}\nName: {NameBL}\nPosition - {Position.ToString()}\nDrones in Charging: No Drones";
        }

        public int GetIdBL() { return idBL; }
        public string NameBL { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position_pl Position { get; set; }
        public List<DroneInCharge_pl> DronesInCharging;
    }
}
}
