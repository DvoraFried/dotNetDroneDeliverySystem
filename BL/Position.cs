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
        private double Longitude;
        public void SetLongitudePosition(double lnd)
        {
            if (lnd > 24 || lnd < 0)
            {
                throw new UnValidLongitudeException(lnd);
            }
            Longitude = lnd;
        }
        public double GetLongitudePosition() { return Longitude; }

        private double Latitude;
        public void SetLatitudePosition(double ltd)
        {
            if (ltd < 0 || ltd >180)
            {
                throw new UnValidLatitudeException(ltd);
            }
            Latitude = ltd;
        }
        public double GetLatitudePosition() { return Latitude; }
    }
}