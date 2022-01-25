using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
        public DroneToList(DalApi.IDAL dalOB, Drone drone)
        {
            Id = drone.getIdBL();
            Model = drone.ModelBL;
            Weight = drone.MaxWeight;
            BatteryStatus = drone.BatteryStatus;
            DroneStatus = drone.DroneStatus;
            CurrentPosition = drone.CurrentPosition;
            ParcelNun = dalOB.returnParcelArray().ToList().Any(parcel => parcel.DroneId == Id) ?
                        dalOB.returnParcelArray().First(parcel => parcel.DroneId == Id).Id : 0;
        }
        public override string ToString()
        {
            string parcelNum = ParcelNun != 0 ? ParcelNun.ToString() : "not exist";
            return $"============================\nID: {Id}\nModel: {Model}\nMax Weight: {Weight}\nBattery Status: {BatteryStatus}\nDrone Status: {DroneStatus}\nPosition: {CurrentPosition.ToString()}\nParcel Number: {parcelNum}\n============================";
        }
        public int Id { get; set; }
        public string Model { get; set; }
        Enum.WeightCategoriesBL Weight { get; set; }
        public double BatteryStatus { get; set; }
        Enum.DroneStatusesBL DroneStatus { get; set; }
        Position CurrentPosition { get; set; }
        public int ParcelNun { get; set; }
    }
}