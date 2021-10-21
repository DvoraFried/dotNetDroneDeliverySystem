using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Parcel
    {
        public int id { get;set;}
        public int  SenderId { get;set;}
        public int TargetId { get;set;}
        public weightCategories Weight { get;set;}
        public priorities Priority { get;set;}
        public int DroneId { get;set;}
        public DateTime Requested { get;set;}
        public DateTime Scheduled { get;set;}
        public DateTime PickUp { get;set;}
        public DateTime Delivered { get;set;}
    };
}
