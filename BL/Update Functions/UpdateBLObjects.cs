using System.Runtime.CompilerServices;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        /// <summary>
        /// delegates that are used to active function in pl
        /// </summary>
        public Action<BO.Drone> ActionDroneChanged { get ; set ; }
        public Action<BO.Parcel,bool> ActionParcelChanged { get; set; }
        public Action <BO.Customer> ActionCustomerChanged { get; set; }
        public Action<bool> ActionUpdateList { get; set; }

        #region UPDATE FUNCTIONS
        /// <summary>
        /// function updates the dron name in dal 
        /// excaption will b thrown if there is no drone in dal list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newModelName"></param>
        /// <param name="simulation"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDateDroneName(int id, string newModelName, bool simulation = false)
        {
            if (!DronesListBL.Any(d => (d.Id == id))) {
                throw new ObjectDoesntExistsInListException("drone"); }

            BO.Drone drone = DronesListBL.First(d => (d.Id == id));
            drone.ModelBL = newModelName;
            DronesListBL[DronesListBL.FindIndex(d => d.Id == id)] = drone;
            lock (DalObj)
            {
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
                
                if(!simulation)
                    ActionDroneChanged?.Invoke(drone);
            }
        }
        /// <summary>
        /// function updates station data in dal 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeslots"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDateStationData(int id, string name = null, int chargeslots = -1)
        {
            lock (DalObj)
            {
                if (!DalObj.GetStationList().Any(s => (s.Id == id))) {
                    throw new ObjectDoesntExistsInListException("station"); }

                DO.Station station = (DalObj.GetStationList().ToList().First(s => (s.Id == id)));
                string currentName = name != null ? name : station.Name;
                int currentChargeLots = chargeslots != -1 ? chargeslots : station.EmptyChargeSlots;
                BO.Station replaceStation = new BO.Station(id, currentName, new Position(station.Longitude, station.Latitude), currentChargeLots, DronesListBL);
                
                DalObj.ReplaceStationById(ConvertToDal.ConvertToStationDal(replaceStation));
            }
        }
        /// <summary>
        /// function updates customer data in dal 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="newPhone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDateCustomerData(int id, string name = null, string newPhone = null)
        {
            lock (DalObj)
            {
                if (!DalObj.GetCustomerList().Any(c => (c.Id == id))) {
                    throw new ObjectDoesntExistsInListException("customer"); }

                DO.Customer currentCustomer = DalObj.GetCustomerList().ToList().First(c => (c.Id == id));
                string currentName = name != null ? name : currentCustomer.Name;
                string currentPhone = newPhone != null ? newPhone : currentCustomer.Phone;
                BO.Customer replaceCustomer = new BO.Customer(DalObj, id, currentName, currentPhone, new Position(currentCustomer.Longitude, currentCustomer.Latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.GetParcelList().ToList()));
                
                DalObj.ReplaceCustomerById(ConvertToDal.ConvertToCustomerDal(replaceCustomer));
                ActionCustomerChanged?.Invoke(replaceCustomer);
            }
        }
        #endregion
    }

}