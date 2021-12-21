using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct ParcelDAL
    {
        public int Id { get;set;}
        public int  SenderId { get;set;}
        public int TargetId { get;set;}
        public WeightCategories Weight { get;set;}
        public Priorities Priority { get;set;}
        public int DroneId { get;set;}
        public DateTime? Requested { get;set;}//יצירת חבילה למשלוח
        public DateTime? Scheduled { get; set; }//שיוך חבילה לרחפן
        public DateTime? PickUp { get;set;}//איסוף חבילה מלקוח
        public DateTime? Delivered { get;set;}//זמן הגעת חבילה למקבל
    };
}
