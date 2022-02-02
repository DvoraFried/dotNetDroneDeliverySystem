using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enum_pl;

namespace PO
{
    public class ParcelByTransfer_pl
    {
        public ParcelByTransfer_pl(ParcelByTransfer blObj)
        {
            this.Id = blObj.Id;
            this.Weight = (WeightCategories)blObj.Weight;
            this.Priority = (Priorities)blObj.Priority;
            this.IsDelivery = blObj.IsDelivery;
            this.CollectionLocation = blObj.CollectionLocation;
            this.DeliveryDestinationLocation = blObj.DeliveryDestinationLocation;
            this.Sender = blObj.Sender;
            this.Target = blObj.Target;
            this.Distance = blObj.Distance;

        }
        public override string ToString()
        {
            string status = IsDelivery ? "On the way to the destination" : "Awaiting collection";
            return $"--------------\nID: {Id}\nStatus: {status}\nPriority: {Priority}\nWeight: {Weight}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nCollection Location: {CollectionLocation}\nTarget Location: {DeliveryDestinationLocation}\nDistance: {Distance}\n--------------";
        }
        public int Id { get; set; }
        WeightCategories Weight { get; set; }
        Priorities Priority { get; set; }
        public bool IsDelivery { get; set; }
        Position CollectionLocation { get; set; }
        Position DeliveryDestinationLocation { get; set; }
        CustomerOnDelivery Sender { get; set; }
        CustomerOnDelivery Target { get; set; }
        double Distance { get; set; }
    }
}

