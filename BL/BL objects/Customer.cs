using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Exceptions;

namespace BO
{
    public class Customer
    {
        DalApi.IDal dalOB;
        public Customer(DalApi.IDal dalOBG,int id, string name, string phone, Position p, IEnumerable<Parcel>parcels, bool active = true)
        {
            dalOB = dalOBG;
            setIdBL(id);
            NameBL = name;
            PhoneBL = phone;
            Position = p;
            isActive = active;
            ImTheSender = (from P in parcels where P.Sender.Id == id select new DeliveryAtCustomer(dalOB,P,id)).ToList();
            ImTheTarget = (from P in parcels where P.Target.Id == id select new DeliveryAtCustomer(dalOB, P, id)).ToList();
        }

        private int IdBL;
        public void setIdBL(int idC)  {
            if(idC < 99999999 || idC > 999999999) {
                throw new UnValidIdException(idC, "customer");
            }
            IdBL = idC;
        }
     
        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public string PhoneBL { get; set; }
        public Position Position { get; set; }
        public List<DeliveryAtCustomer> ImTheSender { get; set; }
        public List<DeliveryAtCustomer> ImTheTarget { get; set; }
        public bool isActive { get; set; }

        public override string ToString()
        {
            return $"ID: {getIdBL()}\nName: {NameBL}\nPhone: {PhoneBL}\nPosition - {Position.ToString()}\nParcels sent by this customer: {ImTheSender.ToString()}\nParcels that this customer receives: {ImTheTarget.ToString()}";
        }
    }
}
