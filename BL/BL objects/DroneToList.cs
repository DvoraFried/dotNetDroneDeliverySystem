using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public DroneToList(DroneBL drone)
        {
            Id = drone.getIdBL();
            Model = drone.ModelBL;
            Weight = drone.MaxWeight;
            BatteryStatus = drone.BatteryStatus;
            DroneStatus = drone.DroneStatus;
            CurrentPosition = drone.CurrentPosition;
            //ParcelNun = 0; 
        }
        public override string ToString()
        {
            string parcelNum = ParcelNun != 0 ? ParcelNun.ToString() : "not exist";
            return $"ID: {Id} |^| Model: {Model} |^| Max Weight: {Weight} |^| Battery Status: {BatteryStatus} |^| Drone Status: {DroneStatus} |^| Position: {CurrentPosition.ToString()} |^| Parcel Number: {parcelNum}";
        }
        public int Id { get; set; }
        public string Model { get; set; }
        EnumBL.WeightCategoriesBL Weight { get; set; }
        public double BatteryStatus { get; set; }
        EnumBL.DroneStatusesBL DroneStatus { get; set; }
        Position CurrentPosition { get; set; }
        public int ParcelNun { get; set; }
    }
}