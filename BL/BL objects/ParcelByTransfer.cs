using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static BO.Enum;

namespace BO
{
    public class ParcelByTransfer
    {
        public ParcelByTransfer(DalApi.IDal dalOB, Parcel parcel)
        {
            Id = parcel.IdBL;
            IsDelivery = parcel.PickUpBL != null;
            Priority = parcel.Priority;
            Weight = parcel.Weight;
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
        public WeightCategoriesBL Weight { get; set; }
        public PrioritiesBL Priority { get; set; }
        public bool IsDelivery { get; set; }
        public Position CollectionLocation { get; set; }
        public Position DeliveryDestinationLocation { get; set; }
        public CustomerOnDelivery Sender { get; set; }
        public CustomerOnDelivery Target { get; set; }
        public double Distance { get; set; }
    }
}