using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ConvertToBL
    {
        public static List<DroneBL> ConvertToDroneArrayBL(List<DroneDAL> droneDalArray)
        {
            List<DroneBL> droneArrayBl = new List<DroneBL>();
            foreach(DroneDAL drone in droneDalArray)
            {
                droneArrayBl.Add(new DroneBL(drone.Id, drone.Model, (EnumBL.WeightCategoriesBL)(int)drone.MaxWeight, 0, null, 0));
            };
            return droneArrayBl;
        }
        public static CustomerBL ConvertToCustomrtBL(CustomerDAL customerDal)
        {
            CustomerBL customerBL = new CustomerBL(customerDal.Id, customerDal.Name, customerDal.Phone, new Position(customerDal.Longitude, customerDal.Latitude));
            return customerBL;
        }
        public static ParcelBL ConvertToParcelBL(ParcelDAL parcelDal)
        {
            ParcelBL parcelBL = new ParcelBL(parcelDal.SenderId, parcelDal.TargetId, (int)parcelDal.Weight, (int)parcelDal.Priority);
            return parcelBL;
        }
        public static List<ParcelBL> ConvertToParcelArrayBL(List<ParcelDAL> parcelsDal)
        {
            List<ParcelBL> parcelsBl = new List<ParcelBL>();
            foreach(ParcelDAL parcelDal in parcelsDal)
            {
                parcelsBl.Add(ConvertToBL.ConvertToParcelBL(parcelDal));
            }
            return parcelsBl;
        }
    }
}
