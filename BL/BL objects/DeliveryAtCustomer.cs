using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    class DeliveryAtCustomer
    { 
        public DeliveryAtCustomer(ParcelBL parcel)
        {
            Id = parcel.IdBL;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            Status = parcel.DeliveredBL != new DateTime() ? EnumBL.DeliveryStatus.provided :
                     parcel.PickUpBL != new DateTime() ? EnumBL.DeliveryStatus.collected :
                     parcel.ScheduledBL != new DateTime() ? EnumBL.DeliveryStatus.associated :
                     EnumBL.DeliveryStatus.created;
            //Customer = new CustomerOnDelivery();
        }
        public override string ToString()
        {
            return $"ID: {Id} |^| Weight: {Weight} |^| Priotity: {Priority} |^| Status: {Status} |^| Another Customer in Parcel: {Customer.ToString()} ";
        }
        int Id { get; set; }
        WeightCategoriesBL Weight { get; set; }
        PrioritiesBL Priority { get; set; }
        DeliveryStatus Status { get; set; }     
        CustomerOnDelivery Customer { get; set; }
    }
}
