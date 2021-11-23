using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL.BO
{
    public partial class Excptions : Exception
    {
        public class UnValidIdException: Exception
       {
            public UnValidIdException(int id,string type):base(string.Format($"the{id} is not valid to this {type}")){}
        }
        
        public class UnValidLongitudeException : Exception
        {
            public UnValidLongitudeException(double lnd) : base(string.Format($"the longitude {lnd} is not valide")) { }
        }

        public class UnValidLatitudeException : Exception
        {
            public UnValidLatitudeException(double ltd) :base(string.Format($"the Latitude {ltd} is not valide")) { }
        }
        public class ObjectExistsInListException : Exception
        {
            public ObjectExistsInListException(string type) : base(string.Format($"the {type} is alreadt exists in the {type}list")) { }
        }
        public class ObjectDoesntExistsInListException : Exception
        {
            public ObjectDoesntExistsInListException(string type) : base(string.Format($"the {type} is not exsist")) { }
        }
        public class DroneIsNotInMaintenanceException : Exception
        {
            public DroneIsNotInMaintenanceException(int id):base(string.Format($"the drone with the {id} is not empty")) { }
        }
        public class NoPlaceToChargeException : Exception
        {
            public NoPlaceToChargeException() : base(string.Format($"no place to charge the drone")) { }
        }
        public class DroneIsNotInMaintenanceException : Exception
        {
            public DroneIsNotInMaintenanceException(): base(string.Format($"the drone is not in charge")) { }
        }
    }
}
