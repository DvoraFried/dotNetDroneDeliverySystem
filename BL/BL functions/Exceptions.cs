using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    /// <summary>
    /// class for exceptions of all kinds!
    /// </summary>
    internal partial class Exceptions : Exception
    {
        internal class UnValidIdException: Exception
        {
            internal UnValidIdException(int id,string type):base(string.Format($"the {id} is not valid to this {type}")){}
        }
        internal class UnValidPositionException : Exception
        {
            internal UnValidPositionException(double value, string type ) : base(string.Format($"the {type} {value} is not valide")) { }
        }
        internal class ObjectExistsInListException : Exception
        {
            internal ObjectExistsInListException(string type) : base(string.Format($"the {type} is alreadt exists in the {type}'s list")) { }
        }
        internal class ObjectDoesntExistsInListException : Exception
        {
            internal ObjectDoesntExistsInListException(string type) : base(string.Format($"the {type} does not exsist")) { }
        }

        #region DRONE EXCEPTIONS:
        internal class DroneIsNotInMaintenanceException : Exception
        {
            internal DroneIsNotInMaintenanceException(int id):base(string.Format($"the drone {id} is not in maintenance")) { }
        }
        internal class NoPlaceToChargeException : Exception
        {
            internal NoPlaceToChargeException() : base(string.Format($"no place to charge the drone")) { }
        }
        internal class DroneIsNotEmptyException : Exception
        {
            internal DroneIsNotEmptyException() : base(string.Format("the drone is not vacant")) { }
        }
        internal class NoParcelFoundException : Exception
        {
            internal NoParcelFoundException() : base(string.Format("There is no delivery in transfer at this drone")) { }
        }
        internal class NoSuitableParcelException : Exception
        {
            internal NoSuitableParcelException(int droneIdx): base(string.Format($"There is no Suitable parcel to the the drone {droneIdx} in parcels list")) { }
        }
        internal class ThereIsNotEnoughBatteryException : Exception
        {
            internal ThereIsNotEnoughBatteryException(): base(string.Format("There is not enough battery to reach the destination")) { }
        }
        internal class TheDroneHasAlreadyPickedUpTheParcel : Exception
        {
            internal TheDroneHasAlreadyPickedUpTheParcel() : base(string.Format("The drone has already picked up the parcel")) { }
        }
        #endregion

        #region PARCEL EXCEPTIONS:
        internal class ThePackageHasNotYetBeenCollectedException : Exception
        {
            internal ThePackageHasNotYetBeenCollectedException() : base(string.Format("The parcel has not yet been collected")) { }
        }
        internal class ThereAreParcelForTheCustomer : Exception
        {
            internal ThereAreParcelForTheCustomer(int idParcel) : base(string.Format($"The parcel {idParcel} was assaighned to this Customer")) { }
        }
        internal class ParcelAlreadyScheduled : Exception
        {
            internal ParcelAlreadyScheduled(): base(string.Format("Sorry, the parcel already scheduled to drone")) { }
        }
        #endregion  
    }
}
