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
            Priorty = parcel.Priority;
            Status = parcel.PickUpBL == new DateTime() ? false : true;
            weight = parcel.Weight;
        }
        public override string ToString()
        {
            string status = Status ? "On the way to the destination" : "Awaiting collection";
            //Position senderPosition = new Position();
            //Position targetPosition = new Position();
            return $"ID: {Id}\nStatus: {status}\nPriority: {Priorty}\nWeight: {weight}\nSender: {SenderCustomer.ToString()}\nTarget: {GetterCustomer.ToString()}";
            //Console.WriteLine($"Collection Position: {senderPosition}\nTarget Position: {targetPosition}\nDistance: {DistanceBetweenCoordinates(senderPosition, targetPosition)}");
        }
        int Id { get; set; }
        bool Status { get; set; }
        WeightCategoriesBL weight { get; set; }
        PrioritiesBL Priorty { get; set; }
        CustomerOnDelivery SenderCustomer { get; set; }
        CustomerOnDelivery GetterCustomer { get; set; }
    }
}
