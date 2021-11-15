using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    public class ParcelBL
    {
        private int IdBL;
        public void setIdBL(int idP)
        {
            IEnumerable<ParcelBL> parcels = new List<ParcelBL>(); //  במקום זה -> צריך למשוך מהדאטא את רשימת התחנות
            foreach (ParcelBL parcel in parcels)
            {
                if (parcel.getIdBL() == idP)
                {
                    throw new ArgumentException("~ The ID number already exists in the system ~");
                }
            }
            IdBL = idP;
        }
        public int getIdBL() { return IdBL; }
        private int SenderIdBL;
        public void setSenderIdBL(int idS)
        {
            //לשלוף את רשימת הלקוחות ולעבור עליה (פיינד) - אם אין לקוח עם מספר מזהה כמו שהוזן לשלוח חריגה
            SenderIdBL = idS;
        }
        public int getSenderIdBL() { return SenderIdBL; }
        private int TargetIdBL;
        public void setTargetIdBL(int idT)
        {
            //לשלוף את רשימת הלקוחות ולעבור עליה (פיינד) - אם אין לקוח עם מספר מזהה כמו שהוזן לשלוח חריגה
            TargetIdBL = idT;
        }
        public int getTargetIdBL() { return TargetIdBL; }
        public WeightCategoriesBL Weight { get; set; }
        public PrioritiesBL Priority { get; set; }
        public DroneBL DroneIdBL { get; set; }
        public DateTime RequestedBL { get; set; }//יצירת חבילה למשלוח
        public DateTime ScheduledBL { get; set; }//שיוך חבילה לרחפן
        public DateTime PickUpBL { get; set; }//איסוף חבילה מלקוח
        public DateTime DeliveredBL { get; set; }//זמן הגעת חבילה למקבל
    }
}
