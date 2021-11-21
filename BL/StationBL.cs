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
        public void SetId(int idS)
        {
                if(idS<=0)
                {
                    throw new UnValidIdException(idS,"station");
                }
            IdBL = idS;
        }

        public int GetIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public int ChargeSlotsBL { get; set; }
        public Position Position { get; set; }
        public int DronesInCharging { get; set; }
    }
}