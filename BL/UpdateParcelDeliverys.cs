using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
using static BO.Exceptions;
using static BO.DistanceBetweenCoordinates;
using BO;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        IEnumerable<DO.Parcel> returnParcelWithEmergencyParcelsPriority()
        {
            lock (DalObj)
            {
                foreach (DO.Parcel element in DalObj.returnParcelArray()) { if (element.Scheduled == null && (int)element.Priority == (int)BO.Enum.PrioritiesBL.emergency) yield return element; }
            }
        }
        IEnumerable<DO.Parcel> returnParcelWithUsualParcelsPriority()
        {
            lock (DalObj)
            {
                foreach (DO.Parcel element in DalObj.returnParcelArray()) { if (element.Scheduled == null && (int)element.Priority == (int)BO.Enum.PrioritiesBL.usual) yield return element; }
            }
        }
        IEnumerable<DO.Parcel> returnParcelWithRapidlParcelsPriority()
        {
            lock (DalObj)
            {
                foreach (DO.Parcel element in DalObj.returnParcelArray()) { if (element.Scheduled == null && (int)element.Priority == (int)BO.Enum.PrioritiesBL.rapid) yield return element; }
            }
        }
        IEnumerable<DO.Parcel> returnPacelWitSuitWeight(IEnumerable<DO.Parcel> parcelArr,int droneMaxW)
        {
            lock (DalObj)
            {
                foreach (DO.Parcel element in parcelArr) { if ((int)element.Weight <= droneMaxW) yield return element; }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel returnTheClosestParcelId(IEnumerable<DO.Parcel> parcelArr, Position dronePosition)
        {
            lock (DalObj)
            {
                DO.Parcel currentParcel = parcelArr.ToArray()[0];
                foreach (DO.Parcel element in parcelArr)
                {
                    DO.Customer currentParcelSender = DalObj.returnCustomerArray().First(d => (d.Id == currentParcel.SenderId));
                    DO.Customer compairParcelSender = DalObj.returnCustomerArray().First(d => (d.Id == element.SenderId));
                    if (CalculateDistance(dronePosition, new Position(currentParcelSender.Longitude, currentParcelSender.Latitude)) > CalculateDistance(dronePosition, new Position(compairParcelSender.Longitude, compairParcelSender.Latitude)))
                    {
                        currentParcel = element;
                    }
                }
                return currentParcel;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssigningPackageToDrone(int idD)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
                BO.Drone drone = DronesListBL.First(d => (d.getIdBL() == idD));
                if (drone.DroneStatus != DroneStatusesBL.empty) { throw new DroneIsNotEmptyException(); }
                List<DO.Parcel> myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithEmergencyParcelsPriority(), (int)drone.MaxWeight).ToList();
                if (myParcelsSuitWeightArr.Count == 0)
                {
                    myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithRapidlParcelsPriority(), (int)drone.MaxWeight).ToList();
                    if (myParcelsSuitWeightArr.Count == 0)
                    {
                        myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithUsualParcelsPriority(), (int)drone.MaxWeight).ToList();
                        if (myParcelsSuitWeightArr.Count == 0) { throw new NoSuitableParcelException(idD); }
                    }
                }

                BO.Parcel theclosetParcel = ConvertToBL.ConvertToParcelBL(returnTheClosestParcelId(myParcelsSuitWeightArr, drone.CurrentPosition));
                BO.Customer sender = ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(theclosetParcel.Sender.Id));
                if (updateButteryStatus(drone, sender.Position, (int)theclosetParcel.Weight) - (DistanceBetweenCoordinates.CalculateDistance(sender.Position, findClosestStation(sender.Position)) * nonWeightPowerConsumption) <= 0)
                {
                    throw new ThereIsNotEnoughBatteryException();
                }
                drone.DroneStatus = DroneStatusesBL.Shipping;
                theclosetParcel.ScheduledBL = DateTime.Now;
                theclosetParcel.DroneIdBL = new DroneInParcel(drone);
                drone.delivery = new ParcelByTransfer(DalObj, theclosetParcel.IdBL);
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(theclosetParcel));
                DronesListBL[DronesListBL.FindIndex(d => d.getIdBL() == idD)] = drone;
                ActionDroneChanged?.Invoke(drone);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectionOfAParcelByDrone(int idD)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
                if (!DalObj.returnParcelArray().ToList().Any(parcel => parcel.DroneId == idD)) { throw new NoParcelFoundException(); }
                BO.Drone drone = DronesListBL.First(drone => drone.getIdBL() == idD);
                if (drone.delivery.IsDelivery) { throw new TheDroneHasAlreadyPickedUpTheParcel(); }
                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(drone.delivery.Id));
                Position senderPosition = ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(parcel.Sender.Id)).Position;
                parcel.PickUpBL = DateTime.Now;
                drone.BatteryStatus = updateButteryStatus(drone, senderPosition, (int)parcel.Weight);
                drone.CurrentPosition = senderPosition;
                drone.delivery = new ParcelByTransfer(DalObj, parcel.IdBL);
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
                DronesListBL[DronesListBL.FindIndex(d => d.getIdBL() == idD)] = drone;
                ActionDroneChanged?.Invoke(drone);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryOfAParcelByDrone(int idD)
        {
            lock (DalObj)
            {
                if (!DalObj.returnDroneArray().ToList().Any(drone => drone.Id == idD)) { throw new ObjectDoesntExistsInListException("drone"); }
                BO.Drone drone = DronesListBL.First(drone => drone.getIdBL() == idD);
                if (drone.DroneStatus != DroneStatusesBL.Shipping) { throw new Exception(); }
                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(drone.delivery.Id));
                if (parcel.PickUpBL == null) { throw new ThePackageHasNotYetBeenCollectedException(); }
                parcel.DeliveredBL = DateTime.Now;
                parcel.DroneIdBL = null;
                Position targetPos = ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(parcel.Target.Id)).Position;
                drone.BatteryStatus = updateButteryStatus(drone, targetPos, (int)parcel.Weight);
                drone.CurrentPosition = targetPos;
                drone.DroneStatus = DroneStatusesBL.empty;
                drone.delivery = null;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
                ActionDroneChanged?.Invoke(drone);
            }
        }
    }
}
