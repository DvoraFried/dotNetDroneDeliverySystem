using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    class DeliveryAtCustomer
    { 
        public DeliveryAtCustomer(ParcelBL parcel, int myId)
        {
            Id = parcel.IdBL;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            Status = parcel.DeliveredBL != new DateTime() ? EnumBL.DeliveryStatus.provided :
                     parcel.PickUpBL != new DateTime() ? EnumBL.DeliveryStatus.collected :
                     parcel.ScheduledBL != new DateTime() ? EnumBL.DeliveryStatus.associated :
                     EnumBL.DeliveryStatus.created;
            int idSecondCustomer = parcel.Sender.Id == myId ? parcel.Target.Id : parcel.Sender.Id;
            Customer = new CustomerOnDelivery(ConvertToBL.ConvertToCustomrtBL(DalObject.DataSource.MyCustomers.ToList().First(customer => customer.Id == idSecondCustomer)));
        }
        public override string ToString()
        {
            return $"----------------\nID: {Id}\nWeight: {Weight}\nPriotity: {Priority}\nStatus: {Status}\nAnother Customer in Parcel: {Customer.ToString()}\n----------------";
        }
        int Id { get; set; }
        WeightCategoriesBL Weight { get; set; }
        PrioritiesBL Priority { get; set; }
        DeliveryStatus Status { get; set; }     
        CustomerOnDelivery Customer { get; set; }
    }
}
