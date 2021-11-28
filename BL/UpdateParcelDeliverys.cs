using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public partial class BL
    {
        public partial class UpDate
        {
            public IEnumerable<ParcelDAL> returnParcelWithHeighestPriority()
            {
                int maxP = 0;
                foreach(ParcelDAL element in DalObj.returnParcelArray()) { if ((int)element.Priority > maxP){ maxP = (int)element.Weight; } }
                foreach (ParcelDAL element in DalObj.returnParcelArray()) { if ((int)element.Priority == maxP) yield return element; }
            }
            public IEnumerable<ParcelDAL> returnPacelWitSuitWeight(IEnumerable<ParcelDAL> parcelArr,int droneMaxW)
            {
                foreach (ParcelDAL element in parcelArr) { if ((int)element.Weight <= droneMaxW) yield return element; }
            }
            public ParcelDAL returnTheClosestParcelId(IEnumerable<ParcelDAL> parcelArr)
            {
                ParcelDAL currentParcel= parcelArr.ToArray()[0];

                foreach (ParcelDAL element in parcelArr ) {
                    CustomerDAL currentParcelSender = DalObj.returnCustomerArray().First(d => (d.Id== currentParcel.SenderId));
                    Position currentParcelPosition = new Position(currentParcelSender.Longitude, currentParcelSender.Latitude);
                    CustomerDAL compairParcelSender = DalObj.returnCustomerArray().First(d => (d.Id == element.SenderId));
                    Position compairParcelPosition = new Position(compairParcelSender.Longitude, compairParcelSender.Latitude);
                    if () {
                        currentParcel = element;
                    } 
                }
                return currentParcel;
            }
            public void AssigningPackageToDrone(int idD)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == idD)));
                DroneBL drone = DronesListBL[droneBLIndex];
                if (drone.DroneStatus != DroneStatusesBL.empty)
                {
                    throw new DroneIsNotEmptyException();
                }
                IEnumerable<ParcelDAL> MyParcels = returnPacelWitSuitWeight(returnParcelWithHeighestPriority(),(int)drone.MaxWeight);
                
                //function for the right priority
            //the closet location
        }
            public void CollectionOfAParcelByDrone(int idD)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                DroneBL drone = DronesListBL[droneBLIndex];
                if (drone.DroneStatus != DroneStatusesBL.empty)
                {
                    throw new DroneIsNotEmptyException();
                }
                int parcelIndex = DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.DroneId == idD));
                if (parcelIndex == -1)
                {
                    throw new NoParcelFoundException();
                }
                int senderId = DataSource.MyParcels[parcelIndex].SenderId;
                Position senderPosition = new Position(DataSource.MyCustomers.First(c => (c.Id == senderId)).Longitude, DataSource.MyCustomers.First(c => (c.Id == senderId)).Latitude)
                DataSource.MyParcels[parcelIndex].PickUp = DateTime.Now;
                drone.BatteryStatus = updateButteryStatus(drone, senderPosition);
                drone.CurrentPosition = senderPosition;
                DataSource.MyDrones[droneIndex] = ConvertToDal.ConvertToDroneDal(drone);
                DronesListBL[droneIndex] = drone;
            }

            public void DeliveryOfAParcelByDrone(int idD)
            {
                int droneIndex = DronesListBL.IndexOf(DronesListBL.First(d => d.getIdBL() == idD));
                if (droneIndex == -1)
                {
                    throw new ObjectDoesntExistsInListException("drone");
                }
                DroneBL drone = DronesListBL[droneIndex];
                if (drone.DroneStatus != EnumBL.DroneStatusesBL.maintenance)
                {
                    throw new NoDeliveryInTransferExcepyion();
                }
            }
        }
    }
}
