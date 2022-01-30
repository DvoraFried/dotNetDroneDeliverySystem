using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class CustomerOnDelivery_pl
    {
        public CustomerOnDelivery_pl(int id, string name, bool isActive = true)
        {
            flag = isActive;
            Id = id;
            CustomerName = name;
        }
        private bool flag;
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }
}
