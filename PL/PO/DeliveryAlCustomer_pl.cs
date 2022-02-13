using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class DeliveryAlCustomer_pl : DependencyObject
    {
        public DeliveryAlCustomer_pl(DeliveryAtCustomer deliveryBL)
        {
            Id = deliveryBL.Id;
            Weight = (Enum_pl.WeightCategories)(int)deliveryBL.Weight;
            Status = (Enum_pl.DeliveryStatus)(int)deliveryBL.Status;
            Customer = new CustomerOnDelivery_pl(deliveryBL.Customer);
        }

        public static readonly DependencyProperty idProperty =
        DependencyProperty.Register("Id",
                     typeof(object),
                     typeof(DeliveryAlCustomer_pl),
                     new UIPropertyMetadata(0));
        public int Id
        {
            get { return (int)GetValue(idProperty); }
            set { SetValue(idProperty, value); }
        }

        public static readonly DependencyProperty WeightProperty =
        DependencyProperty.Register("Weight",
                     typeof(object),
                     typeof(DeliveryAlCustomer_pl),
                     new UIPropertyMetadata(0));

        private Enum_pl.WeightCategories Weight
        {
            get { return (Enum_pl.WeightCategories)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }

        public static readonly DependencyProperty PriorityProperty =
        DependencyProperty.Register("Priority",
                   typeof(object),
                   typeof(DeliveryAlCustomer_pl),
                   new UIPropertyMetadata(0));
        private Enum_pl.Priorities Priority
        {
            get { return (Enum_pl.Priorities)GetValue(PriorityProperty); }
            set { SetValue(PriorityProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status",
               typeof(object),
               typeof(DeliveryAlCustomer_pl),
               new UIPropertyMetadata(0));
        private Enum_pl.DeliveryStatus Status
        {
            get { return (Enum_pl.DeliveryStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty CustomerProperty =
        DependencyProperty.Register("Customer",
         typeof(object),
         typeof(DeliveryAlCustomer_pl),
         new UIPropertyMetadata(0));
        private CustomerOnDelivery_pl Customer
        {
            get { return (CustomerOnDelivery_pl)GetValue(CustomerProperty); }
            set { SetValue(CustomerProperty, value); }
        }

    }
}
