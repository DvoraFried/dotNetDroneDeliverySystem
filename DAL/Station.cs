using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IDAL.DO
{
   public struct Station
    {
        public int Id { get;set;}
        public string Name { get;set;}
        public int ChargeSlots { get;set;}
        public double Longitude { get;set;}
        public double Latitude { get;set;}
    };
    
}
