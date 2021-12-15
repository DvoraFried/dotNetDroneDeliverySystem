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
        public IEnumerable<ParcelDAL> ReturnNotScheduledParcel()
        {
            foreach(ParcelDAL element in DalObj.returnParcelArray()) { if (!string.IsNullOrEmpty(element.DroneId.ToString())) { yield return element; } }
        }
        public IEnumerable<StationDAL> ReturnStationWithChargeSlots()
        {
            foreach (StationDAL element in DalObj.returnStationArray()) { if (element.EmptyChargeSlots>0) { yield return element; } }
        }
        public List<DroneBL> ReturnDronesByStatusAndMaxW(int droneStatus,int droneMaxWeight)
        {
            List<DroneBL> droneUpdateList = new List<DroneBL>();
            if (droneStatus != -1 && droneMaxWeight != -1)
            {
                foreach (DroneBL element in DronesListBL) { if ((int)element.DroneStatus == droneStatus&& (int)element.MaxWeight == droneMaxWeight) { droneUpdateList.Add(element); } }
            }
            else if (droneStatus != -1)
            {
                foreach(DroneBL element in DronesListBL) { if ((int)element.DroneStatus == droneStatus) { droneUpdateList.Add(element); }}
            }
            else if (droneMaxWeight != -1)
            {
                foreach (DroneBL element in DronesListBL) { if ((int)element.MaxWeight == droneMaxWeight) { droneUpdateList.Add(element); } }
            }
            else {
                return DronesListBL;
            }
            return droneUpdateList;
        }
        public List<ParcelToList> ReturnParcelList()
        {
            List<ParcelToList> parcelsUpdateList = new List<ParcelToList>();
            foreach( ParcelDAL parcel in DalObj.returnParcelArray().ToList())
            {
                parcelsUpdateList.Add(new ParcelToList(ConvertToBL.ConvertToParcelBL(parcel)));
            }
            return parcelsUpdateList;
        }
    }
}
