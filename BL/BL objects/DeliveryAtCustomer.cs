using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    public class DeliveryAtCustomer
    { 
        public DeliveryAtCustomer(int id, int weight, int priority, int myId, int senderID,int targetID, DateTime? Scheduled, DateTime? PickUp, DateTime? Delivered)
        {
            Id = id;
            Weight = (WeightCategoriesBL)weight;
            Priority = (PrioritiesBL)priority;
            Status = Delivered != null ? EnumBL.DeliveryStatus.provided :
                     PickUp != null ? EnumBL.DeliveryStatus.collected :
                     Scheduled != null ? EnumBL.DeliveryStatus.associated :
                     EnumBL.DeliveryStatus.created;
            int secID = myId == senderID ? targetID : senderID;
            Customer = new CustomerOnDelivery(ConvertToBL.ConvertToCustomrtBL(DalObject.DataSource.MyCustomers.ToList().First(customer => customer.Id == secID)));
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
