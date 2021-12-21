using DalObject;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static IBL.BO.EnumBL;
using static IBL.BO.Exceptions;

namespace IBL.BO
{
    public class DroneBL
    {

        public DroneBL(DalApi.IDAL dalOB, int id,string model,WeightCategoriesBL maxW, DroneStatusesBL status,Position p,int stationId)
        {
            Random rnd = new Random();
            setIdBL(id);
            ModelBL = model;
            MaxWeight = maxW;
            CurrentPosition = p;
            BatteryStatus = rnd.Next(20, 41);
            DroneStatus = status;
            delivery = dalOB.returnParcelArray().ToList().Any(parcel => parcel.DroneId == idBL)?
                       new ParcelByTransfer(dalOB, ConvertToBL.ConvertToParcelBL(dalOB.returnParcelArray().ToList().First(parcel => parcel.DroneId == idBL)))
                       : null;
            if (delivery != null) { DroneStatus = DroneStatusesBL.Shipping; }
        }
        public override string ToString()
        {
            if (delivery != null)
            {
                return $"ID: {getIdBL()}\nModel: {ModelBL}\nMax Weight: {MaxWeight}\nBattery Status: {BatteryStatus+"%"}\nDrone Status: {DroneStatus}\nDelivery by Transfer: {delivery.ToString()}\nPosition {getFormattedLocationInDegree(CurrentPosition.Latitude, CurrentPosition.Longitude)}";
            }
            return $"ID: {getIdBL()}\nModel: {ModelBL}\nMax Weight: {MaxWeight}\nBattery Status: {BatteryStatus + "%"}\nDrone Status: {DroneStatus}\nDelivery by Transfer:  Non Deliveries by Transfer\nPosition {getFormattedLocationInDegree(CurrentPosition.Latitude,CurrentPosition.Longitude)}";
        }
        private int idBL;
        public void setIdBL(int idD) 
        {
            if (idD <=0) {throw new UnValidIdException(idD, "drone");}
            idBL = idD;
        }
        public int getIdBL() { return idBL; }
        public string ModelBL { get; set; }
        public WeightCategoriesBL MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatusesBL DroneStatus { get; set; }
        public ParcelByTransfer delivery { get; set; }
        public Position CurrentPosition { get; set; }

    }
}
