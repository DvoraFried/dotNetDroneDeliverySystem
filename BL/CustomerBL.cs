using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public class CustomerBL
    {
        public CustomerBL(int id, string name, string phone, double longitude, double latitude)
        {
            setIdBL(id);
            NameBL = name;
            PhoneBL = phone;
            Position = new Position(latitude, longitude);
        }

        private int IdBL;
        public void setIdBL(int idC) 
        {
            if(idC < 99999999 || idC > 999999999) {
                throw new UnValidIdException(idC, "customer");
            }
            IdBL = idC;
        }
        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public string PhoneBL { get; set; }
        public Position Position { get; set; }
    }
}
