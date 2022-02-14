using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// factory creates BL object
    /// </summary>
    public static class BLFactory
    {
        public static IBL factory()
        {
            return BL.BL.GetBl;
        }
    }
}
