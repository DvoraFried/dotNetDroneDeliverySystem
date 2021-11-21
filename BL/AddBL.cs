using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Excptions;


namespace IBL.BO
{
    public partial class BL
    {
        public static Position ReturnPosition(double latitude, double longitude)
        {
            Position P = new Position(longitude, latitude);
            return P;
        }

        public class Add
        {
            public static void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
            {
                if ((DataSource.MyBaseStations.Find(s => s.Id == id)).Id != 0)
                {
                    throw new ObjectExistsInListException("station");
                };
                StationBL station = new StationBL(id, name, ReturnPosition(latitude, longitude), chargeSlots, 0);
                DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
            }
            public static void AddDrone(int id, string model, EnumBL.WeightCategoriesBL weight, int stationId)
            {
                if ((DataSource.MyDrones.Find(d => d.Id == id)).Id != 0)
                {
                    throw new ObjectExistsInListException("drone");
                };
                StationDAL s = ((DataSource.MyBaseStations.Find(d => d.Id == stationId)));
                if (s.Name == null)
                {
                    throw new ObjectExistsInListException("station");
                }
                DroneBL drone = new DroneBL(id, model, weight, DroneStatusesBL.maintenance, ReturnPosition(s.Latitude, s.Longitude));
                DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL.Add(drone);
            }
            public static void AddCustomer(int id, string name, string phone, double longitude, double latitude)
            {
                if ((DataSource.MyCustomers.Find(c => c.Id == id)).Id != 0)
                {
                    throw new ObjectExistsInListException("customer");
                }
                CustomerBL customer = new CustomerBL(id, name, phone, ReturnPosition(latitude, longitude));
                DalObj.AddCustomerDAL(ConvertToDal.ConvertToCustomerDal(customer));
            }

            public static void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority)
            {
                if ((DataSource.MyCustomers.Find(c => c.Id == idSender)).Id != 0)
                {
                    throw new ObjectDoesntExistsInListException("sender");
                }
                if ((DataSource.MyCustomers.Find(c => c.Id == idTarget)).Id != 0)
                {
                    throw new ObjectDoesntExistsInListException("target");
                }
                ParcelBL parcel = new ParcelBL(idSender, idTarget, weight, priority);
                DalObj.AddParcelDAL(ConvertToDal.ConvertToParcelDal(parcel));
            }
        }
    }
}