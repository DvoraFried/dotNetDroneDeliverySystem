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
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));
                if (droneBLIndex == -1) {
                    throw new ObjectDoesntExistsInListException("drone"); 
                }
                DroneBL drone = DronesListBL[droneBLIndex];
                drone.ModelBL = newModelName;
                DronesListBL[droneBLIndex] = drone;
                DataSource.MyDrones[droneBLIndex] = ConvertToDal.ConvertToDroneDal(drone);
            }

            public void UpDateStationData(int id, string name, int chargeSlots)
            {
                int stationIndex = DataSource.MyBaseStations.IndexOf(DataSource.MyBaseStations.First(s => (s.Id == id)));
                if (stationIndex == -1) {
                    throw new ObjectDoesntExistsInListException("station");
                }

            }
        }
    }
}
