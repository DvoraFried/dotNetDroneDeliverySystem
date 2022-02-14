using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        #region CTOR
        public CustomerToList(DalApi.IDal dALOB,Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Phone = customer.Phone;
            NumOfPackagesSentAndDelivered = (from P in dALOB.GetParcelList() where (P.SenderId == Id && P.Delivered != null) select P).ToList().Count;
            NumOfPackagesSentButNotYetDelivered = (from P in dALOB.GetParcelList() where (P.SenderId == Id && P.Delivered == null) select P).ToList().Count;
            NumOfPackagesHeReceived = (from P in dALOB.GetParcelList() where (P.TargetId == Id && P.Delivered != null) select P).ToList().Count;
            NumOfPackagesOnTheWay = (from P in dALOB.GetParcelList() where (P.TargetId == Id && P.Delivered == null) select P).ToList().Count;
        }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            return $"============================\nID: {Id}\nName: {Name}\nPhone: {Phone}\nNumber Of Parcels He Sent And Delivered: {NumOfPackagesSentAndDelivered}\nNumber Of Parcels He Sent But Not Yet Delivered {NumOfPackagesSentButNotYetDelivered}\nNumber Of Parcels He Received: {NumOfPackagesHeReceived}\nNumber Of Parcels On The Way: {NumOfPackagesOnTheWay}\n============================";
        }
        #endregion

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int NumOfPackagesSentAndDelivered { get; set; }
        public int NumOfPackagesSentButNotYetDelivered { get; set; }
        public int NumOfPackagesHeReceived { get; set; }
        public int NumOfPackagesOnTheWay { get; set; }
    }
}
