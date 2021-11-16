using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
        public class ConvertToDal
        {
            public static StationDAL ConvertToStationDal(StationBL stationBl)
            {
            StationDAL stationDal = new StationDAL();
            stationDal.Id = stationBl.getIdBL();
            stationDal.Name = stationBl.NameBL;
            stationDal.ChargeSlots = stationBl.ChargeSlotsBL;
            stationDal.Longitude = stationBl.PositionBL.Longitude;
            stationDal.Latitude = stationBl.PositionBL.Latitude;
            return stationDal;           
            }
        }
}
