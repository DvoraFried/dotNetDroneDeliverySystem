using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PO.Enum_pl;

namespace PO
{
    public class Parcel_pl: DependencyObject
    {
        DalApi.IDal DalObj;
        public Parcel_pl(BlApi.IBL blObj, Parcel parcelBL)
        {
            Id = parcelBL.GetParcelId();
            Weight = (Enum_pl.WeightCategories)parcelBL.Weight;
            Priority = (Enum_pl.Priorities)parcelBL.Priority;
            DroneInParcel = new DroneInParcel_pl(parcelBL.DroneIdBL);
            Requested = parcelBL.RequestedBL;
            Scheduled = parcelBL.ScheduledBL;
            PickUp = parcelBL.PickUpBL;
            Delivered = parcelBL.DeliveredBL;
            Sender = new CustomerOnDelivery_pl(parcelBL.Sender);
            Target = new CustomerOnDelivery_pl(parcelBL.Target);
        }
/*        public override string ToString()
        {
            Console.WriteLine($"ID: {IdBL}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nWeight: {Weight}\nPriority: {Priority}\nRequested Time: {RequestedBL}");
            if (ScheduledBL != null) { Console.WriteLine($"Scheduled Time: {ScheduledBL}"); }
            if (PickUpBL != null) { Console.WriteLine($"PickUp Time: {PickUpBL}"); }
            if (DeliveredBL != null) { Console.WriteLine($"Delivered Time: {DeliveredBL}"); }
            return "";
        }*/

        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel_pl DroneInParcel { get; set; }
        public DateTime? Requested { get; set; }//יצירת חבילה למשלוח
        public DateTime? Scheduled { get; set; }//שיוך חבילה לרחפן
        public DateTime? PickUp { get; set; }//איסוף חבילה מלקוח
        public DateTime? Delivered { get; set; }//זמן הגעת חבילה למקבל
        public CustomerOnDelivery_pl Sender { get; set; }
        public CustomerOnDelivery_pl Target { get; set; }
    }
}

