﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static BO.EnumBL;

namespace BO
{
    public class ParcelBL
    {
        DalApi.IDAL DalObj;
        public ParcelBL(DalApi.IDAL dalOB, int idSender, int idTarget, int weight, int priority, bool IsActive = true, int id = -1, DateTime? requested = null, DateTime? scheduled = null, DateTime? pickUp = null, DateTime? delivered = null )
        {
            DalObj = dalOB;
            parcelId++;
            IdBL = id > 0 ? id : parcelId;
            Weight = (WeightCategoriesBL)weight;
            Priority = (PrioritiesBL)priority;
            ScheduledBL = scheduled;
            PickUpBL = pickUp;
            DeliveredBL = delivered;
            RequestedBL = requested == null? DateTime.Now : requested;
            int droneId = id != -1 ? dalOB.returnParcelArray().ToList().Any(parcel => parcel.Id == id) ? dalOB.returnParcel(id).DroneId : dalOB.returnParcelWithOutTargetArray().ToList().First(parcel => parcel.Id == id).DroneId : id;
            DroneIdBL = droneId != -1 ? new DroneInParcel(DronesListBL.First(drone => drone.getIdBL() == droneId)) : null;
            DO.CustomerDAL customer = idSender!=-1 ? DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idSender) : new DO.CustomerDAL();
            Sender = idSender != -1 ? new CustomerOnDelivery(customer.Id, customer.Name) : new CustomerOnDelivery();
            customer = idTarget!=-1 ? DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idTarget) : new DO.CustomerDAL();
            Target = idTarget!=-1 ? new CustomerOnDelivery(customer.Id, customer.Name): new CustomerOnDelivery();
            isActive = IsActive;
        }
        public override string ToString()
        {
            Console.WriteLine($"ID: {IdBL}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nWeight: {Weight}\nPriority: {Priority}\nRequested Time: {RequestedBL}");
            if (ScheduledBL != null) { Console.WriteLine($"Scheduled Time: {ScheduledBL}"); }
            if (PickUpBL != null) { Console.WriteLine($"PickUp Time: {PickUpBL}"); }
            if (DeliveredBL != null) { Console.WriteLine($"Delivered Time: {DeliveredBL}"); }
            return "";
        }

        private static int parcelId = 0;
        public int GetParcelId() { return parcelId; }
        public void SetParcelId(int pId) {  parcelId=pId; }
        public int IdBL { get; set; }
        public WeightCategoriesBL Weight { get; set; }
        public PrioritiesBL Priority { get; set; }
        public DroneInParcel DroneIdBL { get; set; }
        public DateTime? RequestedBL { get; set; }//יצירת חבילה למשלוח
        public DateTime? ScheduledBL { get; set; }//שיוך חבילה לרחפן
        public DateTime? PickUpBL { get; set; }//איסוף חבילה מלקוח
        public DateTime? DeliveredBL { get; set; }//זמן הגעת חבילה למקבל
        public CustomerOnDelivery Sender { get; set; }
        public CustomerOnDelivery Target { get; set; }
        public bool isActive { get; set; }
    }
}
