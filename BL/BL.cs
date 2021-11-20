using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BL
    {
        static IDAL.DO.IDAL DalObj = DALFactory.factory();

        public class Add
        {
            public bool AddStation(int id, string name, Position position, int chargeSlots)
            {
                StationBL station = new StationBL();
                try
                {
                    station.set_id(id);
                    station.NameBL = name;
                    station.Position = position;
                    station.ChargeSlotsBL = chargeSlots;
                    station.DronesInCharging = 0;
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
                return true;
            }
            public bool AddDrone(int id, string model, EnumBL.WeightCategoriesBL weight, int stationId)
            {
                DroneBL drone = new DroneBL();
                try
                {
                    Random rnd = new Random();
                    drone.setIdBL(id);
                    drone.ModelBL = model;
                    drone.MaxWeight = weight;
                    drone.setCurrentPosition(stationId);
                    drone.BatteryStatus = rnd.Next(20, 41);
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
                return true;
            }
            public void AddCustomer(int id, string name, string phone, Position position)
            {
                CustomerBL customer = new CustomerBL();
                try
                {
                    customer.setIdBL(id);
                    customer.NameBL = name;
                    customer.PhoneBL = phone;
                    customer.position = position;
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                DalObj.AddCustomerDAL(ConvertToDal.ConvertToCustomerDal(customer));
            }
            public void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority)
            {
                ParcelBL parcel = new ParcelBL();
                try
                {
                    //parcel.setIdBL(id); לא כתוב מה לגבי מספר מזהה של החבילה עצמה
                    parcel.setSenderIdBL(idSender);
                    parcel.setTargetIdBL(idTarget);
                    parcel.Weight = weight;
                    parcel.Priority = priority;
                    parcel.ScheduledBL = new DateTime();
                    parcel.PickUpBL = new DateTime();
                    parcel.DeliveredBL = new DateTime();
                    parcel.RequestedBL = DateTime.Now;
                    parcel.DroneIdBL = null;
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                DalObj.AddParcelDAL(ConvertToDal.ConvertToParcelDal(parcel)); 
            }
        }
    }
}
