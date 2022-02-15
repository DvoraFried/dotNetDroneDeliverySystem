using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class Station_pl: DependencyObject
    {
        public Station_pl(BO.Station station)
        {
            Id = station.Id;
            Name = station.Name;
            Longitude = station.Position.Longitude;
            Latitude = station.Position.Latitude;
            ChargeSlots = station.ChargeSlotsBL;
            DronesInCharging = (from d in station.DronesInCharging select new DroneInCharge_pl(d)).ToList();
        }
        public override string ToString()
        {
            if (DronesInCharging != null)
            {
                return $"ID: {Id}\nName: {Name}\nPosition -\n   Lnd: {Latitude} Ltd: {Longitude}\nDrones In charge: {from d in DronesInCharging select d.ToString()}";
            }
            return $"ID: {Id}\nName: {Name}\nPosition -\n   Lnd: {Latitude} Ltd: {Longitude}\nDrones in Charging: No Drones";
        }

        public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id",
                     typeof(object),
                     typeof(Station_pl),
                     new UIPropertyMetadata(0));
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty NameProperty =
        DependencyProperty.Register("Name",
                     typeof(object),
                     typeof(Station_pl),
                     new UIPropertyMetadata(0));
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty ChargeSlotsProperty =
        DependencyProperty.Register("ChargeSlots",
                     typeof(object),
                     typeof(Station_pl),
                     new UIPropertyMetadata(0));
        public int ChargeSlots
        {
            get { return (int)GetValue(ChargeSlotsProperty); }
            set { SetValue(ChargeSlotsProperty, value); }
        }

        public static readonly DependencyProperty LongitudeProperty =
        DependencyProperty.Register("Longitude",
               typeof(object),
               typeof(Station_pl),
               new UIPropertyMetadata(0));

        public double Longitude
        {
            get { return (double)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        }

        public static readonly DependencyProperty LatitudeProperty =
        DependencyProperty.Register("Latitude",
            typeof(object),
            typeof(Station_pl),
            new UIPropertyMetadata(0));
        public double Latitude
        {
            get { return (double)GetValue (LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }

        public static readonly DependencyProperty DronesInChargingProperty =
        DependencyProperty.Register("DronesInCharging",
            typeof(object),
            typeof(Station_pl),
            new UIPropertyMetadata(0));

        public List<DroneInCharge_pl> DronesInCharging
        {
            get { return (List<DroneInCharge_pl>)GetValue(DronesInChargingProperty); }
            set { SetValue(DronesInChargingProperty, value); }
        }
    }
}

