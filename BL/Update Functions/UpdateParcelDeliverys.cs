﻿using System.Runtime.CompilerServices;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
using static BO.Exceptions;
using BO;

namespace BL
{
    public partial class BL : BlApi.IBL
    { 
        #region Internal auxiliary methods
        IEnumerable <DO.Parcel> returnParcelByCondition(Predicate<DO.Parcel> condition)
        {
            lock (DalObj) {
                return from P in DalObj.GetParcelList() where (condition(P) && P.Scheduled == null) select P;
            }
        }

        IEnumerable<DO.Parcel> returnPacelsWitSuitWeight(IEnumerable<DO.Parcel> parcelArr, int droneMaxW)
        {
            lock (DalObj) {
                return from P in parcelArr where ((int)P.Weight <= droneMaxW) select P;
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
                    if (dronePosition.CalculateDistanceFor(new Position(currentParcelSender.Longitude, currentParcelSender.Latitude)) > dronePosition.CalculateDistanceFor(new Position(compairParcelSender.Longitude, compairParcelSender.Latitude)))
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

            double power = drone.CurrentPosition.CalculateDistanceFor(sender.Position) * nonWeightPowerConsumption;
            power += sender.Position.CalculateDistanceFor(target.Position) * weightPower;
            power += target.Position.CalculateDistanceFor(findClosestStation(target.Position).Position) * nonWeightPowerConsumption;

            return power < drone.BatteryStatus;
        }

        internal List<DO.Parcel> getSuitWeightArr(BO.Drone drone)
        {
            List<DO.Parcel> myParcelsSuitWeightArr = returnPacelsWitSuitWeight(returnParcelByCondition(P => (int)P.Priority == (int)BO.Enum.PrioritiesBL.emergency), (int)drone.MaxWeight).ToList();
            if (myParcelsSuitWeightArr.Count == 0) {
                myParcelsSuitWeightArr = returnPacelsWitSuitWeight(returnParcelByCondition(P => (int)P.Priority == (int)BO.Enum.PrioritiesBL.rapid), (int)drone.MaxWeight).ToList();
                if (myParcelsSuitWeightArr.Count == 0) {
                    myParcelsSuitWeightArr = returnPacelsWitSuitWeight(returnParcelByCondition(P => (int)P.Priority == (int)BO.Enum.PrioritiesBL.usual), (int)drone.MaxWeight).ToList();
                }
            }
            return myParcelsSuitWeightArr;
        }
        #endregion

        /// <summary>
        /// function Assigning Parcel To Drone by drone id 
        /// </summary>
        /// <param name="idD"></param>
        /// <param name="simulation"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssigningPackageToDrone(int idD, bool simulation = false)
        {
            lock (DalObj)
            {
                if (!getDroneListWithoutDeletedDrones().Any(d => (d.Id == idD))) {
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
                    // If the drones list has not been opened since the start of the run,
                    // the delegate 'ActionUpdateList' will be empty and an error will occur.
                    // (It is possible that the user reached the drone through a package,
                    // and not through the drones list):
                    if (ActionUpdateList != null) ActionUpdateList?.Invoke(true);
                    
                }
            }
        }

        /// <summary>
        /// function collect parcel frome charge by the drone id
        /// </summary>
        /// <param name="idD"></param>
        /// <param name="simulation"></param>
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

                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.GetParcelByCondition(p => p.Id == drone.delivery.Id));
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
                    // If the drones list has not been opened since the start of the run,
                    // the delegate 'ActionUpdateList' will be empty and an error will occur.
                    // (It is possible that the user reached the drone through a package,
                    // and not through the drones list):
                    if (ActionUpdateList != null) ActionUpdateList?.Invoke(true);
                }
            }
        }
        /// <summary>
        /// the function celiver a parcel to a drone and update the relevent dields
        /// </summary>
        /// <param name="idD"></param>
        /// <param name="simulation"></param>
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

                BO.Parcel parcel = ConvertToBL.ConvertToParcelBL(DalObj.GetParcelByCondition(p => p.Id == drone.delivery.Id));
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
                    // If the drones list has not been opened since the start of the run,
                    // the delegate 'ActionUpdateList' will be empty and an error will occur.
                    // (It is possible that the user reached the drone through a package,
                    // and not through the drones list):
                    if (ActionUpdateList != null) ActionUpdateList?.Invoke(true);
                }
            }
        }
    }
}