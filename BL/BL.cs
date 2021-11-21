using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public class BL
    {
        public static List<DroneBL> DronesListBL = new List<DroneBL>();

        static IDAL.DO.IDAL DalObj = DALFactory.factory();
        public static Position ReturnPosition(double latitude, double longitude)
        {
            Position P = new Position(longitude, latitude);
            return P;
        }
        public class Add
        {
            public static void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
            {
                if ((DataSource.MyBaseStations.Find(s => s.Id == id)).Id!= 0){ throw new ObjectExistsInListException("station"); };
                StationBL station = new StationBL();
                station.SetId(id);
                station.NameBL = name;
                station.Position = ReturnPosition(latitude,longitude);
                station.ChargeSlotsBL = chargeSlots;
                station.DronesInCharging = 0;
                DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
            }
            public static void AddDrone(int id, string model, EnumBL.WeightCategoriesBL weight, int stationId)
            {
                if ((DataSource.MyDrones.Find(d => d.Id == id)).Id != 0) { throw new ObjectExistsInListException("drone"); };
                DroneBL drone = new DroneBL();
                Random rnd = new Random();
                drone.setIdBL(id);
                drone.ModelBL = model;
                drone.MaxWeight = weight;
                drone.setCurrentPosition(stationId);
                drone.BatteryStatus = rnd.Next(20, 41);
                //the status of the drone is missing
                DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL.Add(drone);
            }
            public static void AddCustomer(int id, string name, string phone, double longitude, double latitude)
            {
                if ((DataSource.MyCustomers.Find(c => c.Id == id)).Id != 0) { throw new ObjectExistsInListException("customer"); };
                CustomerBL customer = new CustomerBL();
                customer.setIdBL(id);
                customer.NameBL = name;
                customer.PhoneBL = phone;
                customer.Position = ReturnPosition(latitude, longitude);
                DalObj.AddCustomerDAL(ConvertToDal.ConvertToCustomerDal(customer));
            }
            public static void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority)
            {
                if ((DataSource.MyCustomers.Find(c => c.Id == idSender)).Id != 0) { throw new ObjectDoesntExistsInListException("sender"); };
                if ((DataSource.MyCustomers.Find(c => c.Id == idTarget)).Id != 0) { throw new ObjectDoesntExistsInListException("target"); };
                ParcelBL parcel = new ParcelBL();
                parcel.SetParcelId(parcel.GetParcelId() + 1);
                parcel.IdBL = parcel.GetParcelId();
                parcel.SenderIdBL = idSender;
                parcel.TargetIdBL = idTarget;
                parcel.Weight = weight;
                parcel.Priority = priority;
                parcel.ScheduledBL = new DateTime();
                parcel.PickUpBL = new DateTime();
                parcel.DeliveredBL = new DateTime();
                parcel.RequestedBL = DateTime.Now;
                parcel.DroneIdBL = null;
                DalObj.AddParcelDAL(ConvertToDal.ConvertToParcelDal(parcel));
            }
        }
    }
}