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
        public ParcelByTransfer_pl(DalApi.IDal dalOB, Parcel parcel)
        {
            Id = parcel.IdBL;
            IsDelivery = parcel.PickUpBL != null;
            Priority = (Priorities)parcel.Priority;
            Weight = (WeightCategories)parcel.Weight;
            Sender = parcel.Sender;
            Target = parcel.Target;
            DO.Customer dalobj = dalOB.returnCustomerArray().ToList().First(customer => customer.Id == parcel.Sender.Id);
            CollectionLocation = new Position(dalobj.Longitude, dalobj.Latitude);
            dalobj = dalOB.returnCustomerArray().ToList().First(customer => customer.Id == parcel.Target.Id);
            DeliveryDestinationLocation = new Position(dalobj.Longitude, dalobj.Latitude);
            Distance = DistanceBetweenCoordinates.CalculateDistance(CollectionLocation, DeliveryDestinationLocation);
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

