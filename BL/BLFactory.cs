﻿using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public static class BLFactory
    {
        public static IBL factory()
        {
            return BL.BL.GetBLOBJ;
        }
    }
}
