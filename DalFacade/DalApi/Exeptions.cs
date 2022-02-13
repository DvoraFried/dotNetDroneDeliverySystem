using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFacade.DalApi
{
    public class Exeptions
    {
        public partial class Exceptions : Exception
        {
            public class ObjectExistsInListException : Exception
            {
                public ObjectExistsInListException(string type) : base(string.Format($"the {type} is alreadt exists in the {type}list")) { }
            }
        }
    }
}
