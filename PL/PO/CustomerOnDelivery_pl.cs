using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class CustomerOnDelivery_pl
    {
        public CustomerOnDelivery_pl(CustomerOnDelivery customerOnDelivery)
        {
            Id = customerOnDelivery.Id;
            CustomerName = customerOnDelivery.CustomerName;
        }
        private int Id { get; set; }
        private string CustomerName { get; set; }
    }
}
