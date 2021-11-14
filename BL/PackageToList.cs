using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class PackageToList
    {
        public int Id { get; set; }

        public string SenderName { get; set; }
        public string UstomerReceivesName { get; set; }
        public WeightCategory weight { get; set; }
        public priority priority { get; set; }
    }
}
