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
            if (lnd > 24 || lnd < 0) { throw new UnValidPositionException(lnd, "Longitude"); }
            Longitude = lnd;
            if (ltd > 180 || ltd < 0) { throw new UnValidPositionException(ltd, "Latitude"); }
            Latitude = ltd;
        }
        public override string ToString()
        {
            return $"Longitude : {Longitude}, Latitude: {Latitude}";
        }
        public double Longitude { get; set; }
        
        public double Latitude { get; set; }
    }
}