using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerBL
    {
        private int IdBL;
        public void setIdBL(int idC) 
        {
            IEnumerable<CustomerBL> customers = new List<CustomerBL>(); //  במקום זה -> צריך למשוך מהדאטא את רשימת המשתמשים
            foreach (CustomerBL customer in customers)
            {
                if (customer.getIdBL() == idC) {
                    throw new ArgumentException("~ The ID number already exists in the system ~");
                }
            }
            if(idC < 99999999 || idC > 999999999) {
                throw new ArgumentException("~ An ID number should contain exactly 9 digits ~");
            }
            IdBL = idC;
        }
        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public string PhoneBL { get; set; }
        public Position position { get; set; }
    }
}
