using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct package
    {
        public int id { get;set;};
        public int  customerId { get;set;};
        public int getterId { get;set;};
        public weightCategories packageWeight { get;set;};
        public priority packagePriority { get;set;};
        public int quadocopterId { get;set;};
        public int packagingTime { get;set;};
        public int packageLoadingTime { get;set;};
        public int collectingTime { get;set;};
        public int arrivalTime { get;set;};
    };
}
