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
        #region Internal auxiliary methods
        IEnumerable<DO.Parcel> returnParcelWithEmergencyParcelsPriority()
        {
            lock (DalObj)
            {
                return from P in DalObj.GetParcelList() 
                       where (P.Scheduled == null && (int)P.Priority == (int)BO.Enum.PrioritiesBL.emergency) 
                       select P;
            }
        }
        IEnumerable<DO.Parcel> returnParcelWithUsualParcelsPriority()
        {
            lock (DalObj)
            {
                return from P in DalObj.GetParcelList()
                       where (P.Scheduled == null && (int)P.Priority == (int)BO.Enum.PrioritiesBL.usual)
                       select P;
            }
        }
        IEnumerable<DO.Parcel> returnParcelWithRapidlParcelsPriority()
        {
            lock (DalObj)
            {
                return from P in DalObj.GetParcelList()
                       where (P.Scheduled == null && (int)P.Priority == (int)BO.Enum.PrioritiesBL.rapid)
                       select P;
            }
        }
        IEnumerable<DO.Parcel> returnPacelWitSuitWeight(IEnumerable<DO.Parcel> parcelArr,int droneMaxW)
        {
            lock (DalObj)
            {
                return from P in parcelArr
                       where ((int)P.Weight <= droneMaxW)
                       select P;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel returnTheClosestParcelId(IEnumerable<DO.Parcel> parcelArr, Position dronePosition)
        {
            lock (DalObj)
            {
                DO.Parcel currentParcel = parcelArr.ToArray()[0];
                foreach (DO.Parcel element in parcelArr)
                {
                    DO.Customer currentParcelSender = DalObj.GetCustomerList().First(d => (d.Id == currentParcel.SenderId));
                    DO.Customer compairParcelSender = DalObj.GetCustomerList().First(d => (d.Id == element.SenderId));
                    if (CalculateDistance(dronePosition, new Position(currentParcelSender.Longitude, currentParcelSender.Latitude)) > CalculateDistance(dronePosition, new Position(compairParcelSender.Longitude, compairParcelSender.Latitude)))
                    {
                        currentParcel = element;
                    }
                }
                return ConvertToBL.ConvertToParcelBL(currentParcel);
            }
        }

        internal bool thereIsBattery(BO.Drone drone, BO.Parcel parcel)
        {
            BO.Customer sender = ConvertToBL.ConvertToCustomrtBL(DalObj.GetCustomerByID(parcel.Sender.Id));
            BO.Customer target = ConvertToBL.ConvertToCustomrtBL(DalObj.GetCustomerByID(parcel.Target.Id));

            double weightPower =  parcel.Weight == WeightCategoriesBL.light ? lightWeightPowerConsumption :
                             parcel.Weight == WeightCategoriesBL.medium ? mediumWeightPowerConsumption :
                             heavyWeightPowerConsumption;

            double power = CalculateDistance(drone.CurrentPosition, sender.Position) * nonWeightPowerConsumption;
            power += CalculateDistance(sender.Position, target.Position) * weightPower;
            power += CalculateDistance(target.Position, findClosestStation(target.Position).Position) * nonWeightPowerConsumption;

            return power < drone.BatteryStatus;
        }
        internal List<DO.Parcel> getSuitWeightArr(BO.Drone drone)
        {
            List<DO.Parcel> myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithEmergencyParcelsPriority(), (int)drone.MaxWeight).ToList();
            if (myParcelsSuitWeightArr.Count == 0) {
                myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithRapidlParcelsPriority(), (int)drone.MaxWeight).ToList();
                if (myParcelsSuitWeightArr.Count == 0) {
                    myParcelsSuitWeightArr = returnPacelWitSuitWeight(returnParcelWithUsualParcelsPriority(), (int)drone.MaxWeight).ToList();
                }
            }
            return myParcelsSuitWeightArr;
        }
        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssigningPackageToDrone(int idD, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!ReturnDroneListWithoutDeletedDrones().Any(d => (d.Id == idD))) {
                    throw new ObjectDoesntExistsInListException("drone"); }

                BO.Drone drone = DronesListBL.First(d => (d.Id == idD));
                if (drone.DroneStatus != DroneStatusesBL.empty) {
                    throw new DroneIsNotEmptyException(); }

                List<DO.Parcel> myParcelsSuitWeightArr = getSuitWeightArr(drone);
                if (myParcelsSuitWeightArr.Count == 0) {
                    throw new NoSuitableParcelException(idD);
                }

                BO.Parcel theclosetParcel = returnTheClosestParcelId(myParcelsSuitWeightArr, drone.CurrentPosition);
                if(!thereIsBattery(drone, theclosetParcel)) {
                    throw new ThereIsNotEnoughBatteryException();
                }

                drone.DroneStatus = DroneStatusesBL.Shipping;
                theclosetParcel.ScheduledBL = DateTime.Now;
                theclosetParcel.DroneIdBL = new DroneInParcel(drone);
                drone.delivery = new ParcelByTransfer(DalObj, theclosetParcel.Id);
                
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(theclosetParcel));
                DronesListBL[DronesListBL.FindIndex(d => d.Id == idD)] = drone;
                
                if (!simulation)
                {
                    ActionDroneChanged?.Invoke(drone);
                    ActionParcelChanged?.Invoke(theclosetParcel,true);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectionOfAParcelByDrone(int idD, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!DronesListBL.Any(d => (d.Id == idD))) {
                    throw new ObjectDoesntExistsInListException("drone"); }

                if (!DalObj.GetParcelList().ToList().Any(parcel => parcel.DroneId == idD)) {
                    throw new NoParcelFoundException(); }

                BO.Drone drone = DronesListBL.First(drone => drone.Id == idD);
                if (drone.delivery.IsDelivery) {
                    throw new TheDroneHasAlreadyPickedUpTheParcel(); }

                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.GetParcel(drone.delivery.Id));
                Position senderPosition = ConvertToBL.ConvertToCustomrtBL(DalObj.GetCustomerByID(parcel.Sender.Id)).Position;
                
                parcel.PickUpBL = DateTime.Now;
                drone.BatteryStatus = updateButteryStatus(drone, senderPosition, 0);
                drone.CurrentPosition = senderPosition;
                drone.delivery = new ParcelByTransfer(DalObj, parcel.Id);
                
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
                DronesListBL[DronesListBL.FindIndex(d => d.Id == idD)] = drone;
                
                if (!simulation)
                {
                    ActionDroneChanged?.Invoke(drone);
                    ActionParcelChanged?.Invoke(parcel,true);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryOfAParcelByDrone(int idD, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!DalObj.GetDroneList().ToList().Any(drone => drone.Id == idD)) {
                    throw new ObjectDoesntExistsInListException("drone"); }

                BO.Drone drone = DronesListBL.First(drone => drone.Id == idD);
                if (drone.DroneStatus != DroneStatusesBL.Shipping) {
                    throw new NoParcelFoundException(); }

                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.GetParcel(drone.delivery.Id));
                if (parcel.PickUpBL == null) { 
                    throw new ThePackageHasNotYetBeenCollectedException(); }
                
                Position targetPos_ = ConvertToBL.ConvertToCustomrtBL(DalObj.GetCustomerByID(parcel.Target.Id)).Position;
                parcel.DeliveredBL = DateTime.Now;
                parcel.DroneIdBL = null;
                drone.BatteryStatus = updateButteryStatus(drone, targetPos_, (int)parcel.Weight);
                drone.CurrentPosition = targetPos_;
                drone.DroneStatus = DroneStatusesBL.empty;
                drone.delivery = null;
                
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
                
                if (!simulation)
                {
                    ActionDroneChanged?.Invoke(drone);
                    ActionParcelChanged?.Invoke(parcel,true);
                }
            }
        }
    }
}