using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum_pl;

namespace PO
{
    class Parcel_pl
    {
        DalApi.IDal DalObj;
        public Parcel_pl(DalApi.IDal dalOB, int idSender, int idTarget, int weight, int priority, bool IsActive = true, int id = -1, DateTime? requested = null, DateTime? scheduled = null, DateTime? pickUp = null, DateTime? delivered = null)
        {
            DalObj = dalOB;
            parcelId++;
            IdBL = id > 0 ? id : parcelId;
            Weight = (WeightCategories)weight;
            Priority = (Priorities)priority;
            ScheduledBL = scheduled;
            PickUpBL = pickUp;
            DeliveredBL = delivered;
            RequestedBL = requested == null ? DateTime.Now : requested;
            int droneId = id != -1 ? dalOB.returnParcelArray().ToList().First(parcel => parcel.Id == id).DroneId : id;
            //@@@@@@@ dvora!! DronesListPl deosnt exist yet, have fun tring to figuer out what to do, just kidding, you need to a class with list for pl  .goodluck!
            DroneIdBL = droneId != -1 ? new DroneInParcel_pl(DronesListPl.First(drone => drone.getIdBL() == droneId)) : null;
            DO.Customer customer = DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idSender);
            Sender = customer.isActive ? new CustomerOnDelivery_pl(customer.Id, customer.Name) : new CustomerOnDelivery_pl(customer.Id, customer.Name, false);
            customer = DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idTarget);
            Target = customer.isActive ? new CustomerOnDelivery_pl(customer.Id, customer.Name) : new CustomerOnDelivery_pl(customer.Id, customer.Name, false);
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
        public void SetParcelId(int pId) { parcelId = pId; }
        public int IdBL { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel_pl DroneIdBL { get; set; }
        public DateTime? RequestedBL { get; set; }//יצירת חבילה למשלוח
        public DateTime? ScheduledBL { get; set; }//שיוך חבילה לרחפן
        public DateTime? PickUpBL { get; set; }//איסוף חבילה מלקוח
        public DateTime? DeliveredBL { get; set; }//זמן הגעת חבילה למקבל
        public CustomerOnDelivery_pl Sender { get; set; }
        public CustomerOnDelivery_pl Target { get; set; }
        public bool isActive { get; set; }
    }
}

