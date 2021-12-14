using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    public class ParcelBL
    {
        public ParcelBL(int idSender, int idTarget, int weight, int priority, int id = -1, DateTime requested = new DateTime(), DateTime scheduled = new DateTime(), DateTime pickUp = new DateTime(), DateTime delivered = new DateTime() )
        {
            parcelId++;
            IdBL = id >= 0 ? id : parcelId;
            Weight = (WeightCategoriesBL)weight;
            Priority = (PrioritiesBL)priority;
            ScheduledBL = scheduled;
            PickUpBL = pickUp;
            DeliveredBL = delivered;
            RequestedBL = requested == new DateTime()? DateTime.Now : requested;
            DroneIdBL = null;
            Sender = new CustomerOnDelivery(ConvertToBL.ConvertToCustomrtBL(DalObject.DataSource.MyCustomers.ToList().First(customer => customer.Id == idSender)));
            Target = new CustomerOnDelivery(ConvertToBL.ConvertToCustomrtBL(DalObject.DataSource.MyCustomers.ToList().First(customer => customer.Id == idTarget)));
        }
        public override string ToString()
        {
            Console.WriteLine($"ID: {IdBL}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nWeight: {Weight}\nPriority: {Priority}\nRequested Time: {RequestedBL}");
            if (ScheduledBL != new DateTime()) { Console.WriteLine($"Scheduled Time: {ScheduledBL}"); }
            if (PickUpBL != new DateTime()) { Console.WriteLine($"PickUp Time: {PickUpBL}"); }
            if (DeliveredBL != new DateTime()) { Console.WriteLine($"Delivered Time: {DeliveredBL}"); }
            return "";
        }

        private static int parcelId = 9;
        public int GetParcelId() { return parcelId; }
        public void SetParcelId(int pId) {  parcelId=pId; }
        public int IdBL { get; set; }
        public WeightCategoriesBL Weight { get; set; }
        public PrioritiesBL Priority { get; set; }
        public DroneInParcel DroneIdBL { get; set; }
        public DateTime RequestedBL { get; set; }//יצירת חבילה למשלוח
        public DateTime ScheduledBL { get; set; }//שיוך חבילה לרחפן
        public DateTime PickUpBL { get; set; }//איסוף חבילה מלקוח
        public DateTime DeliveredBL { get; set; }//זמן הגעת חבילה למקבל
        public CustomerOnDelivery Sender { get; set; }
        public CustomerOnDelivery Target { get; set; }

    }
}
