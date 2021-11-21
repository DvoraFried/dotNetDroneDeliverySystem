using DalObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public partial class BL
    {
        public class UpDate
        {
            public void UpDateDroneName(int id, string newModelName)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id))) {
                    throw new ObjectDoesntExistsInListException("drone");
                }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                DroneBL drone = DronesListBL[droneBLIndex];
                drone.ModelBL = newModelName;
                DronesListBL[droneBLIndex] = drone;
                DataSource.MyDrones[droneBLIndex] = ConvertToDal.ConvertToDroneDal(drone);
            }

            public void UpDateStationData(int id, string name = null, int chargeslots = -1)
            {
                if (!DataSource.MyBaseStations.Any(s => (s.Id == id))) {
                    throw new ObjectDoesntExistsInListException("station");
                }
                int stationIndex = DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(s => (s.Id == id)));
                string currentName = name != null ? name : DataSource.MyBaseStations[stationIndex].Name;
                int currentChargeLots = chargeslots != -1 ? chargeslots : DataSource.MyBaseStations[stationIndex].ChargeSlots;
                StationBL station = new StationBL(id, currentName, ReturnPosition(DataSource.MyBaseStations[stationIndex].Longitude, DataSource.MyBaseStations[stationIndex].Latitude), currentChargeLots, DataSource.MyBaseStations[stationIndex].DronesInCharging);
                DataSource.MyBaseStations[stationIndex] = ConvertToDal.ConvertToStationDal(station);
            }

            public void UpDateCustomerData(int id, string name = null, string newPhone = null)
            {
                if(!DataSource.MyCustomers.Any(c => (c.Id == id))) { 
                    throw new ObjectDoesntExistsInListException("customer"); 
                }
                int customerIndex = DataSource.MyCustomers.IndexOf(DataSource.MyCustomers.First(c => (c.Id == id)));
                string currentName = name != null ? name : DataSource.MyCustomers[customerIndex].Name;
                string currentPhone = newPhone != null ? newPhone : DataSource.MyCustomers[customerIndex].Phone;
                CustomerBL customer = new CustomerBL(id, currentName, currentPhone, ReturnPosition(DataSource.MyBaseStations[customerIndex].Latitude, DataSource.MyBaseStations[customerIndex].Longitude));
            }

            public void SendDroneToCharge(int id)
            {

            }


        }
    }
}
