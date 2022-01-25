//using DalObject;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
using static BO.Exceptions;


namespace BL
{
    partial class BL : BlApi.IBL
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (DalObj.returnStationArray().ToList().Any(s => s.Id == id)) { throw new ObjectExistsInListException("Station"); };
            BO.Station station = new BO.Station(id, name, new Position(longitude, latitude), chargeSlots, DronesListBL);
            DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
        }
        public void AddDrone(int id, string model, BO.Enum.WeightCategoriesBL maxWeight, int stationId)
        {
            if (DalObj.returnDroneArray().ToList().Any(d => d.Id == id)) { throw new ObjectExistsInListException("drone"); };
            if (!DalObj.returnStationArray().ToList().Any(s => s.Id == stationId)) { throw new ObjectDoesntExistsInListException("station"); };
            DO.Station s = DalObj.returnStationArray().ToList().Find(d => d.Id == stationId);
            s.DronesInCharging += 1;
            s.EmptyChargeSlots -= 1;
            DalObj.ReplaceStationById(s);
            BO.Drone drone = new BO.Drone(DalObj, id, model, maxWeight, DroneStatusesBL.maintenance, new Position(s.Longitude, s.Latitude), stationId);
            DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
            DronesListBL.Add(drone);
        }
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            BO.Customer customer = customer = new BO.Customer(DalObj, id, name, phone, new Position(longitude, latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()));
            if (DalObj.returnCustomerArray().Any(c => c.Id == id))
            {
                if (DalObj.returnCustomer(id).isActive) { throw new ObjectExistsInListException("customer"); }
                DalObj.ReplaceCustomerById(ConvertToDal.ConvertToCustomerDal(customer));
            }
            else
            {
                DalObj.AddCustomerDAL(ConvertToDal.ConvertToCustomerDal(customer));
            }
        }
        public void AddParcel(int idSender, int idTarget, BO.Enum.WeightCategoriesBL weight, BO.Enum.PrioritiesBL priority)
        {
            if (!DalObj.returnCustomerArray().Any(c => c.Id == idSender)) { throw new ObjectDoesntExistsInListException("sender customer"); }
            if (!DalObj.returnCustomerArray().Any(c => c.Id == idTarget)) { throw new ObjectDoesntExistsInListException("target customer"); }
            BO.Parcel parcel = new BO.Parcel(DalObj, idSender, idTarget, (int)weight, (int)priority);
            DalObj.AddParcelDAL(ConvertToDal.ConvertToParcelDal(parcel));
        }
    }
}