using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public class Position
    {
        public Position(double lnd, double ltd)
        {
            if (lnd > 24 || lnd < 0) { throw new UnValidLongitudeException(lnd); }
            Longitude = lnd;
            if (lnd > 24 || lnd < 0) { throw new UnValidLongitudeException(lnd); }
            Longitude = lnd;
        }
        public double Longitude { get; set; }
        
        public double Latitude { get; set; }
    }
}