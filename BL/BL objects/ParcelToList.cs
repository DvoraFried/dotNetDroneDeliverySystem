using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public ParcelToList(DalApi.IDAL dalOBG, ParcelBL parcel)
        {
            Id = parcel.IdBL;
            SenderName = dalOBG.returnCustomerArray().ToList().First(customer => customer.Id == parcel.Sender.Id).Name;
            UstomerReceivesName = dalOBG.returnCustomerArray().ToList().First(customer => customer.Id == parcel.Target.Id).Name;
            weight = parcel.Weight;
            priority = parcel.Priority;
            PackageStatus = parcel.DeliveredBL != null ? EnumBL.DeliveryStatus.provided :
                            parcel.PickUpBL != null ? EnumBL.DeliveryStatus.collected :
                            parcel.ScheduledBL != null ? EnumBL.DeliveryStatus.associated :
                            EnumBL.DeliveryStatus.created;
        }
        public override string ToString()
        {
            return $"============================\nID: {Id}\nSender Name: {SenderName}\nCustomer Receives Name: {UstomerReceivesName}\nWeight: {weight}\nPriority: {priority}\nParcel Status: {PackageStatus}\n============================";
        }
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string UstomerReceivesName { get; set; }
        EnumBL.WeightCategoriesBL weight { get; set; }
        EnumBL.PrioritiesBL priority { get; set; }
        EnumBL.DeliveryStatus PackageStatus { get; set; }
    }
}
