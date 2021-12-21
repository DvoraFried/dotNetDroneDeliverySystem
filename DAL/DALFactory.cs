using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public static class DALFactory
    {
        public static DalApi.IDAL factory()
        {
            return DalObject.DalObject.GetDal;
        }
    }
}
