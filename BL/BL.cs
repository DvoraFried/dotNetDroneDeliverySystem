﻿
using DalObject;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Excptions;

namespace IBL.BO
{
    public partial class BL
    {
        public static List<DroneBL> DronesListBL = new List<DroneBL>();

        static IDAL.DO.IDAL DalObj = DALFactory.factory();

        public static double updateButteryStatus(DroneBL drone, Position position, EnumBL.WeightCategoriesBL weight)
        {
            double distance = CalculateDistance(drone.CurrentPosition, position);
            double lessPower = weight == EnumBL.WeightCategoriesBL.light ? distance * 0.05 : weight == EnumBL.WeightCategoriesBL.medium ? distance * 0.1 : distance * 0.15;
            if (lessPower > drone.BatteryStatus)
            {
                throw new ThereIsNotEnoughBatteryException();
            }
            return (drone.BatteryStatus - lessPower);
        }
    }

}