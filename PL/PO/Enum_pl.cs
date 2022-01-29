using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Enum_pl
    {
        public enum WeightCategories { light, medium, heavy }
        public enum DroneStatuses { empty, maintenance, Shipping }
        public enum Priorities { usual, rapid, emergency }
        public enum DeliveryStatus { created, associated, collected, provided }

    }
}
