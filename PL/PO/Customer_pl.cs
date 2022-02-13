using BO;
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
        BlApi.IBL dalOB;
        public Customer_pl(BlApi.IBL blObj, Customer customerBl)
        {
            dalOB = blObj;
            id = customerBl.getIdBL();
            name = customerBl.NameBL;
            phone = customerBl.PhoneBL;
            position = new Position_pl(customerBl.Position);
            ImTheSender = (from C in customerBl.ImTheSender select new DeliveryAlCustomer_pl(C)).ToList();
            ImTheTarget = (from C in customerBl.ImTheTarget select new DeliveryAlCustomer_pl(C)).ToList();
            blObj.ActionCustomerChanged += UpdatePlCustomer;
        }
        public void UpdatePlCustomer(Customer customerBl)
        {
            name = customerBl.NameBL;
            phone = customerBl.PhoneBL;
            ImTheSender = (from C in customerBl.ImTheSender select new DeliveryAlCustomer_pl(C)).ToList();
            ImTheTarget = (from C in customerBl.ImTheTarget select new DeliveryAlCustomer_pl(C)).ToList();
        }
        
        public static readonly DependencyProperty idProperty =
        DependencyProperty.Register("Id",
                     typeof(object),
                     typeof(Customer_pl),
                     new UIPropertyMetadata(0));
        private int id
        {
            get { return (int)GetValue(idProperty); }
            set { SetValue(idProperty, value); }
        }

        public static readonly DependencyProperty nameProperty =
        DependencyProperty.Register("name",
                     typeof(object),
                     typeof(Customer_pl),
                     new UIPropertyMetadata(0));
        public string name
        {
            get { return (string)GetValue(nameProperty); }
            set { SetValue(nameProperty, value); }
        }

        public static readonly DependencyProperty phoneProperty =
        DependencyProperty.Register("phone",
                     typeof(object),
                     typeof(Customer_pl),
                     new UIPropertyMetadata(0));
        public string phone
        {
            get { return (string)GetValue(phoneProperty); }
            set { SetValue(phoneProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
        DependencyProperty.Register("position",
                     typeof(object),
                     typeof(Customer_pl),
                     new UIPropertyMetadata(0));
        public Position_pl position
        {
            get { return (Position_pl)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty ImTheSenderProperty =
        DependencyProperty.Register("ImTheSender",
                  typeof(object),
                  typeof(Customer_pl),
                  new UIPropertyMetadata(0));
        public List<DeliveryAlCustomer_pl> ImTheSender
        {
            get { return (List<DeliveryAlCustomer_pl>)GetValue(ImTheSenderProperty); }
            set { SetValue(ImTheSenderProperty, value); }
        }

        public static readonly DependencyProperty ImTheTargetProperty =
        DependencyProperty.Register("ImTheTarget",
                  typeof(object),
                  typeof(Customer_pl),
                  new UIPropertyMetadata(0));
        public List<DeliveryAlCustomer_pl> ImTheTarget
        {
            get { return (List<DeliveryAlCustomer_pl>)GetValue(ImTheTargetProperty); }
            set { SetValue(ImTheTargetProperty, value); }
        }
    }
}