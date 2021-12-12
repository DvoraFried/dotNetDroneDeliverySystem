using DalObject;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void DisplayStatoin(int idS)
        {
            /// Drones in Carges: {???}
            StationDAL station = DalObj.returnStation(idS);
            Console.WriteLine($"~ station data ~ \nID: {idS} \nName: {station.Name}\n Position - \nLongitude: {station.Longitude}, Latitude: {station.Latitude}");
        }
        public void DisplayDrone(int idD)
        {
            /// Parcel in Drone: {???}
            DroneBL droneBL = DronesListBL.First(drone => drone.getIdBL() == idD);
            Console.WriteLine($"~ drone data ~ \nID: {idD} \nModel: {droneBL.ModelBL}\n Max Weight: {droneBL.MaxWeight}\nButtery Status: {droneBL.BatteryStatus}\nDrone Status: {droneBL.DroneStatus}\nPosition - \nLongitude: {droneBL.CurrentPosition.Longitude}, Latitude: {droneBL.CurrentPosition.Latitude}");
        }
        public void DisplayCustomer(int idC)
        {
            Console.WriteLine(ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(idC)).ToString());
            Console.WriteLine("Parcels sent by this customer: ");
            foreach(ParcelDAL parcel in DalObj.returnParcelArray()) {
                if(parcel.SenderId == idC) {
                    Console.WriteLine(new ParcelToList(ConvertToBL.ConvertToParcelBL(parcel)).ToString());
                }
            }
            Console.WriteLine("Parcels that this customer receives: ");
            foreach (ParcelDAL parcel in DalObj.returnParcelArray()) {
                if (parcel.TargetId == idC) {
                    Console.WriteLine(new ParcelToList(ConvertToBL.ConvertToParcelBL(parcel)).ToString());
                }
            }
        }
        public void DisplayParcel(int idP)
        {
            /// in Drone: {???}
            ParcelDAL parcel = DalObj.returnParcel(idP);
            Console.WriteLine($"~ parcel data ~ \nID: {parcel.Id}\nSender ID: {parcel.SenderId}\nTarget ID: {parcel.TargetId}\nWeight: {parcel.Weight}\nPriority: {parcel.Priority}\nRequested Time: {parcel.Requested}");
        }
        public void DisplayStatoinList()
        {
            foreach(StationDAL station in DalObj.returnStationArray())
            {
                DisplayStatoin(station.Id);
            }
        }
        public void DisplayDroneList()
        {
            foreach(DroneBL drone in DronesListBL)
            {
                DisplayDrone(drone.getIdBL());
            }
        }

        public void DisplayCustomerList()
        {
            foreach (CustomerDAL customer in DalObj.returnCustomerArray())
            {
                DisplayCustomer(customer.Id);
            }
        }

        public void DisplayParcelList()
        {
            foreach(ParcelDAL parcel in DalObj.returnParcelArray())
            {
                DisplayParcel(parcel.Id);
            }
        }

    }
}
