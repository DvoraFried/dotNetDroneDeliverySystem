using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    class Customer_pl: DependencyObject

    {
        DalApi.IDal dalOB;
/*        public Customer(DalApi.IDal dalOBG, int id, string name, string phone, Position p, List<Parcel> parcels, bool active = true)
        {
            dalOB = dalOBG;
            setIdBL(id);
            NameBL = name;
            PhoneBL = phone;
            Position = p;
            isActive = active;
            ImTheSender = new List<DeliveryAtCustomer>();
            ImTheTarget = new List<DeliveryAtCustomer>();
            foreach (Parcel parcel in parcels)
            {
                if (parcel.Sender.Id == id) { ImTheSender.Add(new DeliveryAtCustomer(dalOB, parcel, id)); }
                if (parcel.Target.Id == id) { ImTheTarget.Add(new DeliveryAtCustomer(dalOB, parcel, id)); }
            }
        }*/

        private int IdBL;
        public void setIdBL(int idC)
        {
            IdBL = idC;
        }
        public int getIdBL() { return IdBL; }
        public string NameBL { get; set; }
        public string PhoneBL { get; set; }
        public Position_pl Position { get; set; }
        public List<DeliveryAtCustomer> ImTheSender { get; set; }
        public List<DeliveryAtCustomer> ImTheTarget { get; set; }
        public bool isActive { get; set; }
    }
}
}
