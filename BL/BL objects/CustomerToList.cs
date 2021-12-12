using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public CustomerToList(CustomerBL customer)
        {
            Id = customer.getIdBL();
            Name = customer.NameBL;
            Phone = customer.PhoneBL;
            NumOfPackagesSentAndDelivered = DalObject.DataSource.MyParcels.ToList().FindAll(parcel => parcel.SenderId == Id && parcel.Delivered != new DateTime()).Count;
            NumOfPackagesSentButNotYetDelivered = DalObject.DataSource.MyParcels.ToList().FindAll(parcel => parcel.SenderId == Id && parcel.Delivered == new DateTime()).Count;
            NumOfPackagesHeReceived = DalObject.DataSource.MyParcels.ToList().FindAll(parcel => parcel.TargetId == Id && parcel.Delivered != new DateTime()).Count;
            NumOfPackagesOnTheWay = DalObject.DataSource.MyParcels.ToList().FindAll(parcel => parcel.TargetId == Id && parcel.Delivered == new DateTime()).Count;
        }
        public override string ToString()
        {
            return $"ID: {Id} |^| Name: {Name} |^| Phone: {Phone} |^| Number Of Parcels He Sent And Delivered: {NumOfPackagesSentAndDelivered} |^| Number Of Parcels He Sent But Not Yet Delivered {NumOfPackagesSentButNotYetDelivered} |^| Number Of Parcels He Received: {NumOfPackagesHeReceived} |^| Number Of Parcels On The Way: {NumOfPackagesOnTheWay}";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int NumOfPackagesSentAndDelivered { get; set; }
        public int NumOfPackagesSentButNotYetDelivered { get; set; }
        public int NumOfPackagesHeReceived { get; set; }
        public int NumOfPackagesOnTheWay { get; set; }
    }
}
