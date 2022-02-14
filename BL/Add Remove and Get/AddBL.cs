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
        /// create an bl obj and add is to the station list in dal. exception will be thrown in case of a station with the same id in 
        /// station list.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="chargeSlots"></param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            lock (DalObj)
            {
                if (DalObj.GetStationList().Any(s => s.Id == id)) { throw new ObjectExistsInListException("Station"); };
                BO.Station station = new BO.Station(id, name, new Position(longitude, latitude), chargeSlots, DronesListBL);
                DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
            }
        }
        /// <summary>
        /// the function gets pararmeters acording to the station constructor+id station
        /// create an bl obj and add is to the drone list in dal, and add an intance of dronecharg
        /// exception will be thrown in case of a station with the same drone id
        /// drone list/ there is no station with the given id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationId"></param>
        public void AddDrone(int id, string model, BO.Enum.WeightCategoriesBL maxWeight, int stationId)
        {
            lock (DalObj)
            {
                if (DalObj.GetDroneList().Any(d => d.Id == id)) {
                    throw new ObjectExistsInListException("drone"); };

                if (!DalObj.GetStationList().Any(s => s.Id == stationId)) {
                    throw new ObjectDoesntExistsInListException("station"); };

                DO.Station s = DalObj.GetStationList().ToList().Find(d => d.Id == stationId);
                s.DronesInCharging += 1;
                s.EmptyChargeSlots -= 1;
                
                DalObj.ReplaceStationById(s);
                BO.Drone drone = new BO.Drone(DalObj, id, model, maxWeight, DroneStatusesBL.maintenance, new Position(s.Longitude, s.Latitude), stationId);
                DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL.Add(drone);
                DalObj.Charge(ConvertToDal.ConvertToDroneChargeDal(new BO.DroneInCharge(drone), s.Id));

                ActionUpdateList?.Invoke(true);
            }
        }
        /// <summary>
        /// function gets param according to the customer -ctor .if the customer is not active he will turn to be active with the uptades info
        /// if there is no customer with the id- function add customer to customer list,exception will be thrown in case of a active customer ation with the same id 
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
        /// function create an bl obj with the give, params ,create an bl obj and ad it to parcel list
        /// exception will be thrown in case of a parcel with the same  id
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