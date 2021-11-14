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
        EnumBL.WeightCategoriesBL weight { get; set; }
        EnumBL.PrioritiesBL priority { get; set; }
        EnumBL.DroneStatusesBL PackageStatus { get; set; }
    }
}
