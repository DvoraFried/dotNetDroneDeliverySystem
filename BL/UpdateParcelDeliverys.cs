using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Exceptions;
using static IBL.BO.DistanceBetweenCoordinates;
using IBL.BO;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public IEnumerable<ParcelDAL> returnParcelWithEmergencyParcelsPriority()
        {
            foreach (ParcelDAL element in DalObj.returnParcelArray()) { if (element.Scheduled == null && (int)element.Priority == (int)EnumBL.PrioritiesBL.emergency) yield return element; }
        }
        public IEnumerable<ParcelDAL> returnParcelWithUsualParcelsPriority()
        {
            foreach (ParcelDAL element in DalObj.returnParcelArray()) { if (element.Scheduled == null && (int)element.Priority == (int)EnumBL.PrioritiesBL.usual) yield return element; }
        }
        public IEnumerable<ParcelDAL> returnParcelWithRapidlParcelsPriority()
        {
            foreach (ParcelDAL element in DalObj.returnParcelArray()) { if (element.Scheduled == null && (int)element.Priority == (int)EnumBL.PrioritiesBL.rapid) yield return element; }
        }
        public IEnumerable<ParcelDAL> returnPacelWitSuitWeight(IEnumerable<ParcelDAL> parcelArr,int droneMaxW)
        {
            foreach (ParcelDAL element in parcelArr) { if ((int)element.Weight <= droneMaxW) yield return element; }
        }
        public ParcelDAL returnTheClosestParcelId(IEnumerable<ParcelDAL> parcelArr, Position dronePosition)
        {
            ParcelDAL currentParcel= parcelArr.ToArray()[0];
            foreach (ParcelDAL element in parcelArr ) {
                CustomerDAL currentParcelSender = DalObj.returnCustomerArray().First(d => (d.Id== currentParcel.SenderId));
                CustomerDAL compairParcelSender = DalObj.returnCustomerArray().First(d => (d.Id == element.SenderId));
                if (CalculateDistance(dronePosition, new Position(currentParcelSender.Longitude, currentParcelSender.Latitude))> CalculateDistance(dronePosition, new Position(compairParcelSender.Longitude, compairParcelSender.Latitude)))
                {
                    currentParcel = element;
                } 
            }
            return currentParcel;
        }
        public void AssigningPackageToDrone(int idD)
        {
            if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
            DroneBL drone = DronesListBL.First(d => (d.getIdBL() == idD));
            if(drone.DroneStatus != DroneStatusesBL.empty) { throw new DroneIsNotEmptyException(); }
            List<ParcelDAL> myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithEmergencyParcelsPriority(),(int)drone.MaxWeight).ToList();
            if(myParcelsSuitWeightArr.Count == 0) {
                myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithRapidlParcelsPriority(), (int)drone.MaxWeight).ToList();
                if (myParcelsSuitWeightArr.Count == 0) {
                    myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithUsualParcelsPriority(), (int)drone.MaxWeight).ToList();
                    if(myParcelsSuitWeightArr.Count == 0) { throw new NoSuitableParcelException(idD); }
                }
            }
            ParcelBL theclosetParcel = ConvertToBL.ConvertToParcelBL(returnTheClosestParcelId(myParcelsSuitWeightArr, drone.CurrentPosition));
            CustomerBL sender = ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(theclosetParcel.Sender.Id));
            if (updateButteryStatus(drone, sender.Position, (int)theclosetParcel.Weight) - (DistanceBetweenCoordinates.CalculateDistance(sender.Position, findClosestStation(sender.Position)) * nonWeightPowerConsumption) <= 0)
            { 
                throw new ThereIsNotEnoughBatteryException(); 
            }
            drone.DroneStatus = DroneStatusesBL.Shipping;
            theclosetParcel.ScheduledBL = DateTime.Now;
            theclosetParcel.DroneIdBL = new DroneInParcel(drone);
            drone.delivery = new ParcelByTransfer(theclosetParcel);
            DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
            DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(theclosetParcel));
            DronesListBL[DronesListBL.FindIndex(d => d.getIdBL() == idD)] = drone;
        }
        public void CollectionOfAParcelByDrone(int idD)
        {
            if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
            if(!DalObj.returnParcelArray().ToList().Any(parcel => parcel.DroneId == idD)) { throw new NoParcelFoundException();}
            DroneBL drone = DronesListBL.First(drone => drone.getIdBL() == idD);
            if(drone.delivery.IsDelivery) { throw new TheDroneHasAlreadyPickedUpTheParcel(); }
            ParcelBL parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(drone.delivery.Id));
            Position senderPosition = ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(parcel.Sender.Id)).Position;
            parcel.PickUpBL = DateTime.Now;
            drone.BatteryStatus = updateButteryStatus(drone, senderPosition,(int)parcel.Weight);
            drone.CurrentPosition = senderPosition;
            drone.delivery = new ParcelByTransfer(parcel);
            DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
            DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
            DronesListBL[DronesListBL.FindIndex(d => d.getIdBL() == idD)] = drone;
        }
        public void DeliveryOfAParcelByDrone(int idD)
        {
            if(!DalObj.returnDroneArray().ToList().Any(drone => drone.Id == idD)) { throw new ObjectDoesntExistsInListException("drone"); }
            DroneBL drone = DronesListBL.First(drone => drone.getIdBL() == idD);
            if(drone.DroneStatus != DroneStatusesBL.Shipping) { throw new NoDeliveryInTransferExcepyion(); }
            ParcelBL parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(drone.delivery.Id));
            if(parcel.PickUpBL == null) { throw new ThePackageHasNotYetBeenCollectedException(); }
            parcel.DeliveredBL = DateTime.Now;
            parcel.DroneIdBL = null;
            Position targetPos = ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(parcel.Target.Id)).Position;
            drone.BatteryStatus = updateButteryStatus(drone, targetPos, (int)parcel.Weight);
            drone.CurrentPosition = targetPos;
            drone.DroneStatus = DroneStatusesBL.empty;
            drone.delivery = null;
            DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
            DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
        }
    }
}
