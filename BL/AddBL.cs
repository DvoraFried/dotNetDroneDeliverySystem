using DalObject;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Exceptions;


namespace BL
{
    public partial class BL:IBL.IBL
    {
            public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
            {
                if ((DalObj.returnStationArray().ToList().Any(s => s.Id == id)))   { throw new ObjectExistsInListException("Station");};
                StationBL station = new StationBL(id, name, new Position( longitude, latitude), chargeSlots, DronesListBL);
                DalObj.AddStationDAL(ConvertToDal.ConvertToStationDal(station));
            }
            public void AddDrone(int id, string model, EnumBL.WeightCategoriesBL maxWeight, int stationId)
            {
                if ((DalObj.returnDroneArray().ToList().Any(d => d.Id == id)))   { throw new ObjectExistsInListException("drone"); };
                if (!(DalObj.returnStationArray().ToList().Any(s => s.Id == stationId))) { throw new ObjectDoesntExistsInListException("station"); };
                StationDAL s = ((DalObj.returnStationArray().ToList().Find(d => d.Id == stationId)));;
                s.DronesInCharging += 1;
                s.EmptyChargeSlots -= 1;
                DalObj.ReplaceStationById(s);
                DroneBL drone = new DroneBL(id, model, maxWeight, DroneStatusesBL.maintenance, new Position( s.Longitude, s.Latitude),stationId);
                DalObj.AddDroneDAL(ConvertToDal.ConvertToDroneDal(drone));
                DronesListBL.Add(drone);
            }
            public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
            {
                if ((DalObj.returnCustomerArray().Any(c => c.Id == id)))   { throw new ObjectExistsInListException("customer"); }
                CustomerBL customer = new CustomerBL(id, name, phone, new Position(longitude, latitude), ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()));
                DalObj.AddCustomerDAL(ConvertToDal.ConvertToCustomerDal(customer));
            }
            public void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority)
            {
                if (!(DataSource.MyCustomers.Any(c => c.Id == idSender)))  {throw new ObjectDoesntExistsInListException("sender customer"); }
                if (!(DataSource.MyCustomers.Any(c => c.Id == idTarget)))  {throw new ObjectDoesntExistsInListException("target customer"); }
                ParcelBL parcel = new ParcelBL(idSender, idTarget, (int)weight, (int)priority);
                DalObj.AddParcelDAL(ConvertToDal.ConvertToParcelDal(parcel));
            }
     }
}