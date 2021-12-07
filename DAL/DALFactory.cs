using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public static class DALFactory
    {
        public static IDAL factory()
        {
            return DalObject.DalObject.GetDOBJ;
        }
    }
}
