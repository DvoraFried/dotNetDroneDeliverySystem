using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public static class BLFactory
    {
        public static IBL factory()
        {
            return BL.GetBLOBJ;
        }
    }
}
