using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;
using DO;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        public void RemoveCustomerById(int idCustomer)
        {
            if (DalObj.returnCustomerArray().ToList().Any(c => c.Id == idCustomer))  { throw new ObjectDoesntExistsInListException("customer"); }
            DalObj.returnCustomerArray().ToList().RemoveAll(c => c.Id == idCustomer);
        }
    }
}
