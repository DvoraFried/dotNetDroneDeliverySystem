using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;

namespace BO
{
    public class Station
    {
        #region CTOR
        public Station(int id, string name, Position p, int chargS, List<Drone> drones)
        {
            Id = id;
            Name = name;
            Position = p;
            ChargeSlotsBL = chargS;
            DronesInCharging = new List<DroneInCharge>();
            foreach (Drone drone in drones)
            {
                if (drone.CurrentPosition.Longitude == p.Longitude && drone.CurrentPosition.Latitude == p.Latitude && drone.DroneStatus == Enum.DroneStatusesBL.maintenance) {
                    DronesInCharging.Add(new DroneInCharge(drone));
                }
            }
        }
        #endregion

        private int id;
        #region SET-GET_ID
        public int Id
        {
            get { return id; }
            set
            {
                if (value <= 0)
                {
                    throw new UnValidIdException(value, "station");
                }
                id = value;
            }
        }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            if (DronesInCharging != null)
            {
                Console.WriteLine("DronesInCharging: ");
                foreach (DroneInCharge drone in DronesInCharging)
                {
                    Console.WriteLine(drone.ToString());
                }
                return $"ID: {Id}\nName: {Name}\nPosition - {Position.ToString()}";
            }
            return $"ID: {Id}\nName: {Name}\nPosition - {Position.ToString()}\nDrones in Charging: No Drones";
        }
        #endregion

        public string Name { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position Position { get; set; }
        public List<DroneInCharge> DronesInCharging;
    }
}