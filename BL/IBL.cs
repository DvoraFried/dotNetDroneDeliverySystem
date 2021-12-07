using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBL
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AddDrone(int id, string model, EnumBL.WeightCategoriesBL weight, int stationId);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority);
        public void UpDateDroneName(int id, string newModelName);
        public void UpDateStationData(int id, string name = null, int chargeslots = -1);
        public void UpDateCustomerData(int id, string name = null, string newPhone = null);
        public void SendDroneToCharge(int id);
        public void ReleaseDroneFromCharging(int id, double timeInCharge);
        public void AssigningPackageToDrone(int idD);
        public void CollectionOfAParcelByDrone(int idD);
        public void DeliveryOfAParcelByDrone(int idD);





    }
}
