using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    public partial class BL
    {
        public class ReturnObjectById
        {
            public StationDAL ReturnStationById(int idS)
            {
                return DalObj.returnStationArray().ToList().First(station => station.Id == idS);
            }
            public DroneDAL ReturnDroneById(int idD)
            {
                return DalObj.returnDroneArray().ToList().First(drone => drone.Id == idD);
            }
            public CustomerDAL ReturnCustomerById(int idC)
            {
                return DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idC);
            }
            public ParcelDAL ReturnParcelById(int idP)
            {
                return DalObj.returnParcelArray().ToList().First(parcel => parcel.Id == idP);
            }
        }
    }
}
