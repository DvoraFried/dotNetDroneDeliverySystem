using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerOnDelivery
    {
        public CustomerOnDelivery(CustomerBL customer)
        {
            Id = customer.getIdBL();
            CustomerName = customer.NameBL;
        }
        public override string ToString()
        {
            return $"ID: {Id} |^| Name: {CustomerName}";
        }
        int Id { get; set; }
        string CustomerName { get; set; }
    }
}
