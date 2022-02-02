using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Position_pl
    {
        public Position_pl(Position blPos)
        {
            this.Longitude = blPos.Longitude;
            this.Latitude = blPos.Latitude;
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
