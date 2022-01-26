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
        public void UpDateDroneName(int id, string newModelName)
        {
            if (!DronesListBL.Any(d => (d.getIdBL() == id)))      { throw new ObjectDoesntExistsInListException("drone"); }
            int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
            BO.Drone drone = DronesListBL[droneBLIndex];
            drone.ModelBL = newModelName;
            DronesListBL[droneBLIndex] = drone;
            DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
        }

        public void UpDateStationData(int id, string name = null, int chargeslots = -1)
        {
            if (!DalObj.returnStationArray().Any(s => (s.Id == id)))      {throw new ObjectDoesntExistsInListException("station"); }
            DO.Station station= (DalObj.returnStationArray().ToList().First(s => (s.Id == id)));
            string currentName = name != null ? name : station.Name;
            int currentChargeLots = chargeslots != -1 ? chargeslots : station.EmptyChargeSlots;
            BO.Station replaceStation = new BO.Station(id, currentName, new Position(station.Longitude, station.Latitude), currentChargeLots, DronesListBL);
            DalObj.ReplaceStationById( ConvertToDal.ConvertToStationDal(replaceStation));
        }

        public void UpDateCustomerData(int id, string name = null, string newPhone = null)
        {
            if(!DalObj.returnCustomerArray().Any(c => (c.Id == id))) {  throw new ObjectDoesntExistsInListException("customer"); }
            DO.Customer currentCustomer = DalObj.returnCustomerArray().ToList().First(c => (c.Id == id));
            string currentName = name != null ? name : currentCustomer.Name;
            string currentPhone = newPhone != null ? newPhone : currentCustomer.Phone;
            BO.Customer replaceCustomer = new BO.Customer(DalObj, id, currentName, currentPhone, new Position(currentCustomer.Longitude, currentCustomer.Latitude),ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()));
            DalObj.ReplaceCustomerById(ConvertToDal.ConvertToCustomerDal(replaceCustomer));
        }
    }
}