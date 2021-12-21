using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EnumBL
    {
        public enum WeightCategoriesBL { light, medium, heavy }
        public enum DroneStatusesBL { empty, maintenance, Shipping }
        public enum PrioritiesBL { usual, rapid, emergency }
        public enum DeliveryStatus { created, associated, collected, provided }

    }
}
