using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public partial class Exceptions : Exception
    {
        public class ObjectAlreadyExistInList : Exception
        {
            public ObjectAlreadyExistInList(string type) : base(string.Format($"the object already exist in  {type} list")) { }
        }
        //CantFaindObjectWithThisId("station", DALS.Id)
        public class CantFaindObjectWithThisId : Exception
        {
            public CantFaindObjectWithThisId(string type,int id) : base(string.Format($"cannot find {type} object with {id} id")) { }
        }
    }
}
