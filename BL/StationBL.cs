using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public class StationBL
    {
        private int IdBL;
        public void set_id(int idS)
        {
            IEnumerable<StationBL> stations = new List<StationBL>(); //  במקום זה -> צריך למשוך מהדאטא את רשימת התחנות
            foreach (StationBL station in stations)
            {
                if(station.getIdBL() == idS)
                {
                    throw new ArgumentException("~ The ID number already exists in the system ~");
                }
            }
            IdBL = idS;
        }

        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position PositionBL { get; set; }
        public int DronesInCharging { get; set; }
    }
}