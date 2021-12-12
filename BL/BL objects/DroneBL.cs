using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Exceptions;

namespace IBL.BO
{
    public class DroneBL
    {
        public DroneBL(int id,string model,WeightCategoriesBL maxW, DroneStatusesBL status,Position p,int stationId)
        {
            Random rnd = new Random();
            this.setIdBL(id);
            this.ModelBL = model;
            this.MaxWeight = maxW;
            this.CurrentPosition = p;
            this.BatteryStatus = rnd.Next(20, 41);
            this.DroneStatus = status;
            //this.delivery = 

        }
        public override string ToString()
        {
            return $"ID: {getIdBL()}\nModel: {ModelBL}\nMax Weight: {MaxWeight}\nBattery Status: {BatteryStatus}\nDrone Status: {DroneStatus}\nDelivery by Transfer: {delivery.ToString()}\nPosition -  Longitude: {CurrentPosition.Longitude}, Latitude: {CurrentPosition.Latitude}";
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
        ParcelByTransfer delivery { get; set; }
        public Position CurrentPosition { get; set; }

    }
}
