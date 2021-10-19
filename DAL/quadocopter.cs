using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct quadocopter
    {
        public int id { get; set; }
        public string model { get;set;}
        public weightCategories quadoWeight { get;set;}
        public int battery { get;set;}
        public quadocopterState quadoState { get;set;}

    };

}

