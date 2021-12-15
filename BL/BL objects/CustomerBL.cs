using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;

namespace IBL.BO
{
    public class CustomerBL
    {
        public CustomerBL(int id, string name, string phone, Position p, List<ParcelBL> ImSender, List<ParcelBL>ImTarget)
        {
            setIdBL(id);
            NameBL = name;
            PhoneBL = phone;
            Position = p;
            ImTheSender = new List<DeliveryAtCustomer>();
            ImtheTarget = new List<DeliveryAtCustomer>();
            foreach(ParcelBL parcel in ImSender) { ImTheSender.Add(new DeliveryAtCustomer(parcel, id)); }
            foreach (ParcelBL parcel in ImTarget) { ImtheTarget.Add(new DeliveryAtCustomer(parcel, id)); }
        }

        private int IdBL;
        public void setIdBL(int idC) 
        {
            if(idC < 99999999 || idC > 999999999) {
                throw new UnValidIdException(idC, "customer");
            }
            IdBL = idC;
        }
        public override string ToString()
        {
            return $"ID: {getIdBL()}\nName: {NameBL}\nPhone: {PhoneBL}\nPosition - {Position.ToString()}\nParcels sent by this customer: {ImTheSender.ToString()}\nParcels that this customer receives: {ImtheTarget.ToString()} ";
        }
        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public string PhoneBL { get; set; }
        public Position Position { get; set; }
        public List<DeliveryAtCustomer> ImTheSender { get; set; }
        public List<DeliveryAtCustomer> ImtheTarget { get; set; }

    }
}
