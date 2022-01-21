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
                if ((parcel.TargetId == idCustomer || parcel.SenderId == idCustomer) && (parcel.Delivered == null)) 
                { throw new ThereAreParcelForTheCustomer(parcel.TargetId); }
            }
            CustomerDAL customer = DalObj.returnCustomerArray().ToList().First(c => c.Id == idCustomer);
            customer.isActive = false;
            DalObj.ReplaceCustomerById(customer);
        }
        public void DeleteParcel(ParcelBL parcel)
        {
            if (parcel.ScheduledBL == null)
            {
                parcel.isActive = false;
                DalObj.ReplaceParcelById(ConvertToDal.ConvertToParcelDal(parcel));
            }
            else { throw new ParcelAlreadyScheduled(); }
        }
    }
}
