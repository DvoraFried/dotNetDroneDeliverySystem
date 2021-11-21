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
        public ParcelBL(int idSender, int idTarget, EnumBL.WeightCategoriesBL weight, EnumBL.PrioritiesBL priority)
        {
            parcelId++;
            IdBL = parcelId;
            SenderIdBL = idSender;
            TargetIdBL = idTarget;
            Weight = weight;
            Priority = priority;
            ScheduledBL = new DateTime();
            PickUpBL = new DateTime();
            DeliveredBL = new DateTime();
            RequestedBL = DateTime.Now;
            DroneIdBL = null;
        }

        private static int parcelId = 0;
        public int GetParcelId() { return parcelId; }
        public void SetParcelId(int pId) {  parcelId=pId; }
        public int IdBL { get; set; }
        public int SenderIdBL { get; set; }
        public int TargetIdBL { get; set; }
        public WeightCategoriesBL Weight { get; set; }
        public PrioritiesBL Priority { get; set; }
        public DroneBL DroneIdBL { get; set; }
        public DateTime RequestedBL { get; set; }//יצירת חבילה למשלוח
        public DateTime ScheduledBL { get; set; }//שיוך חבילה לרחפן
        public DateTime PickUpBL { get; set; }//איסוף חבילה מלקוח
        public DateTime DeliveredBL { get; set; }//זמן הגעת חבילה למקבל
    }
}
