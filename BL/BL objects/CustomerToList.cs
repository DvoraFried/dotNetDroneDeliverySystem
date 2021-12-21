using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        public CustomerToList(DalApi.IDAL dALOB,CustomerBL customer)
        {
            Id = customer.getIdBL();
            Name = customer.NameBL;
            Phone = customer.PhoneBL;
            NumOfPackagesSentAndDelivered = dALOB.returnParcelArray().ToList().FindAll(parcel => parcel.SenderId == Id && parcel.Delivered != null).Count;
            NumOfPackagesSentButNotYetDelivered = dALOB.returnParcelArray().ToList().FindAll(parcel => parcel.SenderId == Id && parcel.Delivered == null).Count;
            NumOfPackagesHeReceived = dALOB.returnParcelArray().ToList().FindAll(parcel => parcel.TargetId == Id && parcel.Delivered != null).Count;
            NumOfPackagesOnTheWay = dALOB.returnParcelArray().ToList().FindAll(parcel => parcel.TargetId == Id && parcel.Delivered == null).Count;
        }
        public override string ToString()
        {
            return $"============================\nID: {Id}\nName: {Name}\nPhone: {Phone}\nNumber Of Parcels He Sent And Delivered: {NumOfPackagesSentAndDelivered}\nNumber Of Parcels He Sent But Not Yet Delivered {NumOfPackagesSentButNotYetDelivered}\nNumber Of Parcels He Received: {NumOfPackagesHeReceived}\nNumber Of Parcels On The Way: {NumOfPackagesOnTheWay}\n============================";
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
