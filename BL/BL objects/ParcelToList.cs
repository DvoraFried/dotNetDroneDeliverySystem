using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelToList
    {
        public ParcelToList(ParcelBL parcel)
        {
            Id = parcel.IdBL;
        }
        public override string ToString()
        {
            return $"ID: {Id}\nSender Name: {SenderName}\nCustomer Receives Name: {UstomerReceivesName}\nWeight: {weight}\nPriority: {priority}\nParcel Status: {PackageStatus}";
        }
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string UstomerReceivesName { get; set; }
        EnumBL.WeightCategoriesBL weight { get; set; }
        EnumBL.PrioritiesBL priority { get; set; }
        EnumBL.DroneStatusesBL PackageStatus { get; set; }
    }
}
