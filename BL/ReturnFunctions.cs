using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public class ReturnFunctions
        {
            public StationDAL ReturnStationById(int idS)
            {
                return DalObj.returnStationArray().ToList().First(station => station.Id == idS);
            }
            public DroneDAL ReturnDroneById(int idD)
            {
                return DalObj.returnDroneArray().ToList().First(drone => drone.Id == idD);
            }
            public CustomerDAL ReturnCustomerById(int idC)
            {
                return DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idC);
            }
            public ParcelDAL ReturnParcelById(int idP)
            {
                return DalObj.returnParcelArray().ToList().First(parcel => parcel.Id == idP);
            }
            public IEnumerable<StationDAL> returnStationsArr()
            {
                foreach (StationDAL element in DalObj.returnStationArray().ToList()) { yield return element; }
            }
            public IEnumerable<DroneDAL> returnDronesArr()
            {
                foreach (DroneDAL element in DalObj.returnDroneArray().ToList()) { yield return element; }
            }
            public IEnumerable<CustomerDAL> returncustomersArr()
            {
                foreach (CustomerDAL element in DalObj.returnCustomerArray().ToList()) { yield return element; }
            }
            public IEnumerable<ParcelDAL> returnStationArr()
            {
                foreach (ParcelDAL element in DalObj.returnParcelArray().ToList()) { yield return element; }
            }
            //String.IsNullOrEmpty(element.DroneId.ToString())-> this is a way to check if an intager is null
            public IEnumerable<ParcelDAL> ReturnNotScheduledParcel()
            {
                foreach(ParcelDAL element in DalObj.returnParcelArray()) { if (!string.IsNullOrEmpty(element.DroneId.ToString())) { yield return element; } }
            }
            public IEnumerable<StationDAL> ReturnStationWithChargeSlots()
            {
                foreach (StationDAL element in DalObj.returnStationArray()) { if (element.EmptyChargeSlots>0) { yield return element; } }
            }
            public List<DroneBL> ReturnBlDroneList()
            {
                return DronesListBL;
            }
        }
    }
}
