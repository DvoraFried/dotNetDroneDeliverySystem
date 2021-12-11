﻿using DalObject;
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
            public ParcelDAL returnTheClosestParcelId(IEnumerable<ParcelDAL> parcelArr,int droneIdx)
            {
                ParcelDAL currentParcel= parcelArr.ToArray()[0];
                Position dronePosition = DronesListBL[droneIdx].CurrentPosition;
                foreach (ParcelDAL element in parcelArr ) {
                    CustomerDAL currentParcelSender = DalObj.returnCustomerArray().First(d => (d.Id== currentParcel.SenderId));
                    Position currentParcelPosition = new Position(currentParcelSender.Longitude, currentParcelSender.Latitude);
                    CustomerDAL compairParcelSender = DalObj.returnCustomerArray().First(d => (d.Id == element.SenderId));
                    Position compairParcelPosition = new Position(compairParcelSender.Longitude, compairParcelSender.Latitude);
                    if (CalculateDistance(dronePosition, currentParcelPosition)> CalculateDistance(dronePosition, compairParcelPosition)) {
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
                IEnumerable<ParcelDAL> myParcelsArr = returnPacelWitSuitWeight(returnParcelWithHeighestPriority(),(int)drone.MaxWeight);
                if (myParcelsArr == null) { throw new NoSuitableParcelException(droneBLIndex); }
                ParcelDAL theclosetParcel = returnTheClosestParcelId(myParcelsArr, droneBLIndex);
                theclosetParcel.DroneId = drone.getIdBL();
                theclosetParcel.Scheduled = DateTime.Now;
                drone.DroneStatus = DroneStatusesBL.Shipping;
                DalObj.ReplaceParcelById(theclosetParcel);
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL[droneBLIndex] = drone;
            }
            public void CollectionOfAParcelByDrone(int idD)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == idD))) { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == idD)));
                DroneBL drone = DronesListBL[droneBLIndex];
                if (drone.DroneStatus != DroneStatusesBL.empty) {
                    throw new DroneIsNotEmptyException(); }
                int parcelIndex = DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.DroneId == idD));
                if (parcelIndex == -1) {
                    throw new NoParcelFoundException(); }
                int senderId = DataSource.MyParcels[parcelIndex].SenderId;
                Position senderPosition = new Position(DataSource.MyCustomers.First(c => (c.Id == senderId)).Longitude, DataSource.MyCustomers.First(c => (c.Id == senderId)).Latitude);
                ParcelBL parcel = new ParcelBL(DataSource.MyParcels[parcelIndex].SenderId, DataSource.MyParcels[parcelIndex].TargetId, (int)DalObj.returnParcelArray().ToList()[parcelIndex].Weight, (int)DalObj.returnParcelArray().ToList()[parcelIndex].Priority);
                parcel.PickUpBL = DateTime.Now;
                DataSource.MyParcels[parcelIndex] = ConvertToDal.ConvertToParcelDal(parcel);
                drone.BatteryStatus = updateButteryStatus(drone, senderPosition,(int)DalObj.returnParcelArray().ToList()[parcelIndex].Weight);
                drone.CurrentPosition = senderPosition;
                DataSource.MyDrones[droneBLIndex] = ConvertToDal.ConvertToDroneDal(drone);
                DronesListBL[droneBLIndex] = drone;
            }

            public void DeliveryOfAParcelByDrone(int idD)
            {
                int droneIndex = DronesListBL.IndexOf(DronesListBL.First(d => d.getIdBL() == idD));
                if (droneIndex == -1) {
                    throw new ObjectDoesntExistsInListException("drone"); }
                DroneBL drone = DronesListBL[droneIndex];
                if (drone.DroneStatus != EnumBL.DroneStatusesBL.maintenance) {
                    throw new NoDeliveryInTransferExcepyion(); }
                int parcelIndex = DataSource.MyParcels.IndexOf(DataSource.MyParcels.First(p => p.DroneId == idD));
                ParcelBL parcel = new ParcelBL(DataSource.MyParcels[parcelIndex].SenderId, DalObj.returnParcelArray().ToList()[parcelIndex].TargetId, (int)DalObj.returnParcelArray().ToList()[parcelIndex].Weight, (int)DalObj.returnParcelArray().ToList()[parcelIndex].Priority);
                parcel.DeliveredBL = DateTime.Now;
                DataSource.MyParcels[parcelIndex] = ConvertToDal.ConvertToParcelDal(parcel);
                Position targetPosition = new Position(DataSource.MyCustomers.First(c => (c.Id == DataSource.MyParcels[parcelIndex].TargetId)).Longitude, DataSource.MyCustomers.First(c => (c.Id == DataSource.MyParcels[parcelIndex].TargetId)).Latitude);
                drone.BatteryStatus = updateButteryStatus(drone, targetPosition, (int)DalObj.returnParcelArray().ToList()[parcelIndex].Weight);
                drone.CurrentPosition = targetPosition;
                drone.DroneStatus = EnumBL.DroneStatusesBL.empty;
                DataSource.MyDrones[droneIndex] = ConvertToDal.ConvertToDroneDal(drone);
                DronesListBL[droneIndex] = drone;
            }
        }
}