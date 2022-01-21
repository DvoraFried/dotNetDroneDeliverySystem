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
                if (parcel.TargetId == idCustomer || parcel.SenderId == idCustomer)
                {
                    ParcelDAL p = parcel;
                    if (parcel.TargetId == idCustomer) { p.TargetId = -1; }
                    else { p.SenderId = -1; }
                    if (parcel.Delivered != null)
                    {
                        //DalObj.RemoveParcelById(parcel);
                        p.TargetId = -1;
                        DalObj.ReplaceParcelById(p);
                        //DalObj.AddParcelDALWithNoTargetOrSender(p);
                    }
                    else { throw new ThereAreParcelForTheCustomer(parcel.TargetId); }
                }
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
