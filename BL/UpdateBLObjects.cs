using DalObject;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;

namespace BL
{
    public partial class BL : IBL.IBL
    {
            public void UpDateDroneName(int id, string newModelName)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id)))      { throw new ObjectDoesntExistsInListException("drone"); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                DroneBL drone = DronesListBL[droneBLIndex];
                drone.ModelBL = newModelName;
                DronesListBL[droneBLIndex] = drone;
                DalObj.ReplaceDroneById(ConvertToDal.ConvertToDroneDal(drone));
            }

            public void UpDateStationData(int id, string name = null, int chargeslots = -1)
            {
                if (!DataSource.MyBaseStations.Any(s => (s.Id == id)))      {throw new ObjectDoesntExistsInListException("station"); }
                StationDAL station= (DalObj.returnStationArray().ToList().First(s => (s.Id == id)));
                string currentName = name != null ? name : station.Name;
                int currentChargeLots = chargeslots != -1 ? chargeslots : station.EmptyChargeSlots;
                StationBL replaceStation = new StationBL(id, currentName, new Position(station.Longitude, station.Latitude), currentChargeLots);
                DalObj.ReplaceStationById( ConvertToDal.ConvertToStationDal(replaceStation));
            }

            public void UpDateCustomerData(int id, string name = null, string newPhone = null)
            {
                if(!DataSource.MyCustomers.Any(c => (c.Id == id)))       {  throw new ObjectDoesntExistsInListException("customer"); }
                CustomerDAL currentCustomer =DalObj.returnCustomerArray().ToList().First(c => (c.Id == id));
                string currentName = name != null ? name : currentCustomer.Name;
                string currentPhone = newPhone != null ? newPhone : currentCustomer.Phone;
                CustomerBL replaceCustomer = new CustomerBL(id, currentName, currentPhone, new Position(currentCustomer.Latitude, currentCustomer.Longitude));
                DalObj.ReplaceCustomerById(ConvertToDal.ConvertToCustomerDal(replaceCustomer));
            }
    }
}
