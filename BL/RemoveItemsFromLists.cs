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
                    DalObj.RemoveParcelById(ConvertToDal.ConvertToParcelDal(p));
                    p.Target.Id = -1;
                    DalObj.AddParcelDALWithNoTarget(ConvertToDal.ConvertToParcelDal(p));
                    /*{ throw new ThereAreParcelForTheCustomer(p.Target.Id); }
                    return;*/
                }
                if (p.Sender.Id == idCustomer)
                {
                    DalObj.RemoveParcelById(ConvertToDal.ConvertToParcelDal(p));
                }
            }
            DalObj.RemoveCustomerById(idCustomer);
               
        }
    }
}
