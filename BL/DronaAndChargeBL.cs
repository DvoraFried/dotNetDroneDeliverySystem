using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.EnumBL;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public partial class BL
    {
        class DronaAndChargeBL
        {
            public void SendDroneToCharge(int id)
            {
                if (!DronesListBL.Any(d => (d.getIdBL() == id))) { throw new ObjectDoesntExistsInListException("drone"); }
                if (DronesListBL.Find(d => (d.getIdBL() == id)).DroneStatus!= DroneStatusesBL.empty) { throw new DroneIsNotEmptyException(id); }
                int droneBLIndex = DronesListBL.IndexOf(DronesListBL.First(d => (d.getIdBL() == id)));

            }
        }
    }
}
