using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BL
    {
        public class Add
        {
            public void AddStation(int id, string name, Position position, int chargeSlots)
            {
                StationBL station = new StationBL();
                try
                {
                    station.set_id(id);
                    station.NameBL = name;
                    station.PositionBL = position;
                    station.ChargeSlotsBL = chargeSlots;
                    station.DronesInCharging = 0;
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                //פה צריך דחיפה של התחנה שיצרנו לדאל
            }
            public void AddDrone(int id, string model, EnumBL.WeightCategoriesBL weight, int stationId)
            {
                DroneBL drone = new DroneBL();
                Random rnd = new Random();
                try
                {
                    drone.setIdBL(id);
                    drone.ModelBL = model;
                    drone.MaxWeight = weight;
                    //כדי לעדכן את הערך 'מיקום' של הרחפן צריך לשלוף את רשימת התחנות
                    //למצוא את התחנה שהמספר המזהה שלה זהה לזה שהזין המשתמש,
                    //ולהעתיק את ערכי המיקום שלה למיקום של הרחפן
                    //(במידה ולא קיימת תחנה עם המזהה שהוזן - להוציא שגיאה.
                    drone.BatteryStatus = rnd.Next(20, 41);
                    //כאן צריך לעדכן מצב רחפן ואין לנו את זה כי לא יודעות איך לעשות - עם אינם או מה
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                //פה צריך דחיפה של הרחפן שיצרנו לדאל
            }
            public void AddCustomer(int id, string name, string phone, Position position)
            {
                CustomerBL customer = new CustomerBL();
                try
                {
                    customer.setIdBL(id);
                    customer.NameBL = name;
                    customer.PhoneBL = phone;
                    customer.position = position;
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                //פה צריך דחיפה של משתמש שיצרנו לדאל
            }
            public void AddParcel(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority)
            {
                ParcelBL parcel = new ParcelBL();
                try
                {
                    //parcel.setIdBL(id); לא כתוב מה לגבי מספר מזהה של החבילה עצמה
                    parcel.setSenderIdBL(idSender);
                    parcel.setTargetIdBL(idTarget);
                    parcel.Weight = weight;
                    parcel.Priority = priority;
                    parcel.ScheduledBL = new DateTime();
                    parcel.PickUpBL = new DateTime();
                    parcel.DeliveredBL = new DateTime();
                    parcel.RequestedBL = DateTime.Now;
                    parcel.DroneIdBL = null;
                }
                catch (Exception errorMessage)
                {
                    throw new ArgumentException($"{errorMessage}"); //המיין אמור לתפס את זה... מקווה שככה
                }
                //פה צריך דחיפה של החבילה שיצרנו לדאל
            }
        }
    }
}
