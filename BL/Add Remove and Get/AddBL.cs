//using DalObject;
using System.Runtime.CompilerServices;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
using static BO.Exceptions;
using Drone = DO.Drone;

namespace BL
{
   
    partial class BL : BlApi.IBL
    {
        #region ADD FUNCTIONS
        /// <summary>
        /// the function gets pararmeters acording to the station constructor
        /// create an BO station obj and add is to the station list in dal.
        /// exception will be thrown in case of a station with the same id in station list.
        /// </summary>
        /// <param name="id">station id</param>
        /// <param name="name">station name</param>
        /// <param name="longitude">station longitude</param>
        /// <param name="latitude">station latitude</param>
        /// <param name="chargeSlots">station chargea lots</param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            lock (DalObj)
            {
                if (DalObj.GetStationList().Any(s => s.Id == id)) {
                    throw new ObjectExistsInListException("Station"); };

                BO.Station station = new BO.Station(id, name, new Position(longitude, latitude), chargeSlots, DronesListBL);
                DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
            }
        }

        /// <summary>
        /// the function gets pararmeters acording to the drone constructor + id station
        /// create an BO drone obj and add is to the drone list in dal as 'drone in maintenance',
        /// and update the the station respectively.
        /// The function refers to the state of adding an existing but inactive drone. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationId"></param>
        public void AddDrone(int id, string model, BO.Enum.WeightCategoriesBL maxWeight, int stationId)
        {
            lock (DalObj)
            {
                if (!DalObj.GetStationList().Any(s => s.Id == stationId)) {
                    throw new ObjectDoesntExistsInListException("station"); };

                BO.Drone drone = new BO.Drone(DalObj, id, model, maxWeight, DroneStatusesBL.maintenance, new Position(s.Longitude, s.Latitude), stationId);

                if (DronesListBL.Any(d => d.Id == id)) {
                    // If there is an active drone with the same ID:
                    if (DronesListBL.First(d => d.Id == id).isActive) { 
                        throw new ObjectExistsInListException("drone"); }
                    // If there is an inactive drone with the same ID:
                    DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                    DronesListBL[DronesListBL.FindIndex(d => d.Id == id)] = drone;
                }
                else
                {
                    DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
                    DronesListBL.Add(drone);
                }

                DO.Station s = DalObj.GetStationList().ToList().Find(d => d.Id == stationId);
                s.DronesInCharging += 1;
                s.EmptyChargeSlots -= 1;
                
                DalObj.ReplaceStationById(s);
                // add the drone to 'drones in charge' list:
                DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new BO.DroneInCharge(drone), s.Id));
                
                // dalegate to update the drones list in PL:
                ActionUpdateList?.Invoke(true);
            }
        }

        /// <summary>
        /// function gets param according to the customer-ctor,
        /// if the customer is not active he will turn to be active with the uptades info,
        /// if there is no customer with the id - the function add customer to customer list.
        /// exception will be thrown in case of an active customer with the same id already exist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            lock (DalObj)
            {
                BO.Customer customer = customer = new BO.Customer(DalObj, id, name, phone, new Position(longitude, latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.GetParcelList()));
                if (DalObj.GetCustomerList().Any(c => c.Id == id))
                {
                    if (DalObj.GetCustomerByID(id).IsActive) { throw new ObjectExistsInListException("customer"); }
                    DalObj.ReplaceCustomerById(ConvertToDal.ConvertToCustomerDal(customer));
                }
                else
                {
                    DalObj.AddCustomerDAL(ConvertToDal.ConvertToCustomerDal(customer));
                }
            }
        }

        /// <summary>
        /// function create an BO parcel obj with the given params,
        /// and aad it to parcel list.
        /// </summary>
        /// <param name="idSender"></param>
        /// <param name="idTarget"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(int idSender, int idTarget, BO.Enum.WeightCategoriesBL weight, BO.Enum.PrioritiesBL priority)
        {
            lock (DalObj)
            {
                if (!DalObj.GetCustomerList().Any(c => c.Id == idSender)) {
                    throw new ObjectDoesntExistsInListException("sender customer"); }
                
                if (!DalObj.GetCustomerList().Any(c => c.Id == idTarget)) {
                    throw new ObjectDoesntExistsInListException("target customer"); }
                
                BO.Parcel parcel = new BO.Parcel(DalObj, idSender, idTarget, (int)weight, (int)priority);
                DalObj.AddParcelDAL(ConvertToDal.ConvertToParcelDal(parcel));
            }
        }
        #endregion
    }
}