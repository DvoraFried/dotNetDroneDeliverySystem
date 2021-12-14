using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;

namespace IBL.BO
{
    public class CustomerBL
    {
        public CustomerBL(int id, string name, string phone, Position p)
        {
            setIdBL(id);
            NameBL = name;
            PhoneBL = phone;
            Position = p;
        }

        private int IdBL;
        public void setIdBL(int idC) 
        {
            if(idC < 99999999 || idC > 999999999) {
                throw new UnValidIdException(idC, "customer");
            }
            IdBL = idC;
        }
        public override string ToString()
        {
            return $"ID: {getIdBL()}\nName: {NameBL}\nPhone: {PhoneBL}\nPosition - {Position.ToString()}";
        }
        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public string PhoneBL { get; set; }
        public Position Position { get; set; }
    }
}
