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
        public StationBL(int id,string name,Position p, int chargS,int DronesInChargingS)
        {
            this.SetId(id);
            this.NameBL = name;
            this.Position = p;
            this.ChargeSlotsBL = chargS;
            this.DronesInCharging = DronesInChargingS;
        }
        private int idBL;
        public void SetId(int idS)
        {
                if(idS<=0)
                {
                    throw new UnValidIdException(idS,"station");
                }
            idBL = idS;
        }

        public int GetIdBL() { return idBL; }
        public string NameBL { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position Position { get; set; }
        public int DronesInCharging { get; set; }
    }
}