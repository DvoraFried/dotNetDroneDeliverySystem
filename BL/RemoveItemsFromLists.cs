using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;
using DO;
using BO;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        public void RemoveCustomerById(int idCustomer)
        {
            if (!DalObj.returnCustomerArray().ToList().Any(c => c.Id == idCustomer))  { throw new ObjectDoesntExistsInListException("customer"); }
            foreach (ParcelDAL parcel in DalObj.returnParcelArray().ToList())
            {
                ParcelBL p = ConvertToBL.ConvertToParcelBL(parcel);
                if (p.Target.Id == idCustomer)
                {
                    { throw new ThereAreParcelForTheCustomer(p.Target.Id); }
                    return;
                }
            }
            DalObj.RemoveCustomerById(idCustomer);
               
        }
    }
}
