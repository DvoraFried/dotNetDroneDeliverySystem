using DalObject;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Exceptions;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public static void displayStatoin(int idS)
        {
            /// Drones in Carges: {???}
            StationDAL station = DalObj.returnStation(idS);
            Console.WriteLine($"~ station data ~ \nID: {idS} \nName: {station.Name}\n Position - \nLongitude: {station.Longitude}, Latitude: {station.Latitude}");
        }
        public static void displayDrone(int idD)
        {
            /// Parcel in Drone: {???}
            DroneBL droneBL = DronesListBL.First(drone => drone.getIdBL() == idD);
            Console.WriteLine($"~ drone data ~ \nID: {idD} \nModel: {droneBL.ModelBL}\n Max Weight: {droneBL.MaxWeight}\nButtery Status: {droneBL.BatteryStatus}\nDrone Status: {droneBL.DroneStatus}\nPosition - \nLongitude: {droneBL.CurrentPosition.Longitude}, Latitude: {droneBL.CurrentPosition.Latitude}");
        }
        public static void displayCustomer(int idC)
        {

        }
        public static void displayParcel(int idP)
        {

        }
    }
}
