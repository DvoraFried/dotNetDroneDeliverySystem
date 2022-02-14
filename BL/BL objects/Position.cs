using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;

namespace BO
{
    public class Position
    {
        #region CTOR
        public Position(double lnd, double ltd)
        {
            Longitude = lnd;
            Latitude = ltd;
            
            if (ltd > 180 || ltd < 0) { throw new UnValidPositionException(ltd, "Latitude"); }
            Latitude = ltd;
        }
        #endregion

        #region TOSTRING
        public override string ToString()
        {
            return $"Longitude : {Longitude}, Latitude: {Latitude}";
        }
        #endregion

        private double longitude;
        #region GET-SET_longitude
        public double Longitude { get { return longitude; }
            set {
                if (value > 24 || value < 0) { throw new UnValidPositionException(value, "Longitude"); }
                longitude = value;
            }
        }
        #endregion

        private double latitude;
        #region GET-SET_latitude
        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (value > 180 || value < 0) { throw new UnValidPositionException(value, "Latitude"); }
                latitude = value;
            }
        }
        #endregion

    }
}