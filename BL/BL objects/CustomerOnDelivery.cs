using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerOnDelivery
    {
        public CustomerOnDelivery() 
        {
            Id = -1;
            CustomerName = "no data";
        }
        public CustomerOnDelivery(int id, string name)
        {
            Id = id;
            CustomerName = name;
        }
        public override string ToString()
        {
            if (Id != -1)
            {
                return $"ID: {Id}\nName: {CustomerName}";
            }
            return "";
        }
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }
}
