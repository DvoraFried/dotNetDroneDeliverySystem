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
        public Position(double lnd, double ltd)
        {
            Longitude = lnd;
            Latitude = ltd;
            
            if (ltd > 180 || ltd < 0) { throw new UnValidPositionException(ltd, "Latitude"); }
            Latitude = ltd;
        }
        public override string ToString()
        {
            return $"Longitude : {Longitude}, Latitude: {Latitude}";
        }
        private double longitude;
        public double Longitude { get { return longitude; }
            set {
                if (value > 24 || value < 0) { throw new UnValidPositionException(value, "Longitude"); }
                longitude = value;
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (value > 180 || value < 0) { throw new UnValidPositionException(value, "Latitude"); }
                latitude = value;
            }
        }
    }
}