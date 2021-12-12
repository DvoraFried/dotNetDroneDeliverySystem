using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    class ParcelByTransfer
    {
        public ParcelByTransfer(ParcelBL parcel)
        {
            Id = parcel.IdBL;
            DeliveryStatus = parcel.PickUpBL != new DateTime();
            Priority = parcel.Priority;
            Weight = parcel.Weight;
            //Sender = new CustomerOnDelivery(ConvertToBL.ConvertToCustomrtBL());
            //Target = new CustomerOnDelivery(ConvertToBL.ConvertToCustomrtBL());
            //CollectionLocation = ConvertToBL.ConvertToCustomrtBL().Position;
            //DeliveryDestinationLocation = ConvertToBL.ConvertToCustomrtBL().Position;
            Distance = DistanceBetweenCoordinates.CalculateDistance(CollectionLocation, DeliveryDestinationLocation);
        }
        public override string ToString()
        {
            string status = DeliveryStatus ? "On the way to the destination" : "Awaiting collection";
            return $"ID: {Id} |^| Status: {status}\nPriority: {Priority}\nWeight: {Weight}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nCollection Location: {CollectionLocation}\nTarget Location: {DeliveryDestinationLocation}\nDistance: {Distance}";
        }
        int Id { get; set; }
        WeightCategoriesBL Weight { get; set; }
        PrioritiesBL Priority { get; set; }
        bool DeliveryStatus { get; set; }
        Position CollectionLocation { get; set; }
        Position DeliveryDestinationLocation { get; set; }
        CustomerOnDelivery Sender { get; set; }
        CustomerOnDelivery Target { get; set; }
        double Distance { get; set; }
    }
}
