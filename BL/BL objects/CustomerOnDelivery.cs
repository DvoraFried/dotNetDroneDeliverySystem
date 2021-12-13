using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerOnDelivery
    {
        public CustomerOnDelivery(CustomerBL customer)
        {
            Id = customer.getIdBL();
            CustomerName = customer.NameBL;
        }
        public override string ToString()
        {
            return $"\n----------------\nID: {Id}\nName: {CustomerName}\n----------------";
        }
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }
}
