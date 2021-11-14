using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Phone { get; set; }
        public int NumOfPackagesSentAndDelivered { get; set; }
        public int NumOfPackagesSentButNotYetDelivered { get; set; }
        public int NumOfPackagesHeReceived { get; set; }
        public int NumOfPackagesOnTheWay { get; set; }
    }
}
