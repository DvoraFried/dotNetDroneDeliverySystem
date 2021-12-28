using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static BO.EnumBL;

namespace BO
{
    public class DeliveryAtCustomer
    { 
        public DeliveryAtCustomer(DalApi.IDAL dalOBG,ParcelBL parcel, int myId)
        {
            Id = parcel.IdBL;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            Status = parcel.DeliveredBL != null ? EnumBL.DeliveryStatus.provided :
                     parcel.PickUpBL != null ? EnumBL.DeliveryStatus.collected :
                     parcel.ScheduledBL != null ? EnumBL.DeliveryStatus.associated :
                     EnumBL.DeliveryStatus.created;
            int idSecondCustomer = parcel.Sender.Id == myId ? parcel.Target.Id : parcel.Sender.Id;
            DO.CustomerDAL customer = dalOBG.returnCustomerArray().ToList().First(customer => customer.Id == idSecondCustomer);
            Customer = new CustomerOnDelivery(customer.Id, customer.Name);
        }
        public override string ToString()
        {
            return $"----------------\nID: {Id}\nWeight: {Weight}\nPriotity: {Priority}\nStatus: {Status}\nAnother Customer in Parcel: {Customer.ToString()}\n----------------";
        }
        public int Id { get; set; }
        WeightCategoriesBL Weight { get; set; }
        PrioritiesBL Priority { get; set; }
        DeliveryStatus Status { get; set; }     
        CustomerOnDelivery Customer { get; set; }
    }
}
