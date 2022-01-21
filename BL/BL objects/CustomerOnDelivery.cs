using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerOnDelivery
    {
        public CustomerOnDelivery(int id, string name, bool isActive = true)
        {
            flag = isActive;
            Id = id;
            CustomerName = name;
        }
        public override string ToString()
        {
            if (flag) { return $"ID: {Id}\nName: {CustomerName}"; }
            return "no data";
        }
        private bool flag;
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }
}
