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
        public StationBL(int id, string name, Position p, int chargS)
        {
            this.SetId(id);
            this.NameBL = name;
            this.Position = p;
            this.ChargeSlotsBL = chargS;
        }
        private int idBL;
        public void SetId(int idS)
        {
            if (idS <= 0)
            {
                throw new UnValidIdException(idS, "station");
            }
            idBL = idS;
        }

        public int GetIdBL() { return idBL; }
        public string NameBL { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position Position { get; set; }
        //to check what is the error here!!!
        public List<DroneInChargeBL> DronesInCharging = new List<DroneInChargeBL>();

    }
}