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
            public static StationDAL ConvertToStationDal(StationBL BLS)
            {
            StationDAL DALS = new StationDAL();
            DALS.Id = BLS.getIdBL();
            DALS.Name = BLS.NameBL;
            DALS.ChargeSlots = BLS.ChargeSlotsBL;
            DALS.Longitude = BLS.PositionBL.Longitude;
            DALS.Latitude = BLS.PositionBL.Latitude;
            return DALS;           
            }
        }
}
