using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelToList
    {
        public ParcelToList(DalApi.IDal dalOBG, Parcel parcel)
        {
            Id = parcel.Id;
            SenderName = dalOBG.returnCustomerArray().ToList().Any(customer => customer.Id == parcel.Sender.Id) ? dalOBG.returnCustomer(parcel.Sender.Id).Name : null;
            SenderId = parcel.Sender.Id;
            UstomerReceivesName = dalOBG.returnCustomerArray().ToList().Any(customer => customer.Id == parcel.Target.Id) ? dalOBG.returnCustomer(parcel.Target.Id).Name : null;
            TargetId = parcel.Target.Id;
            weight = parcel.Weight;
            priority = parcel.Priority;
            packageStatus = parcel.DeliveredBL != null ? Enum.DeliveryStatus.provided :
                            parcel.PickUpBL != null ? Enum.DeliveryStatus.collected :
                            parcel.ScheduledBL != null ? Enum.DeliveryStatus.associated :
                            Enum.DeliveryStatus.created;
        }
        public override string ToString()
        {
            return $"============================\nID: {Id}\nSender Name: {SenderName}\nCustomer Receives Name: {UstomerReceivesName}\nWeight: {weight}\nPriority: {priority}\nParcel Status: {packageStatus}\n============================";
        }
        public int Id { get; set; }
        public string SenderName { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public string UstomerReceivesName { get; set; }
        Enum.WeightCategoriesBL weight { get; set; }
        Enum.PrioritiesBL priority { get; set; }
        Enum.DeliveryStatus packageStatus { get; set; }
    }
}
