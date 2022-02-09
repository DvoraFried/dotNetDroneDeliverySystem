using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlApi;
using BO;
using static BL.BL;

namespace BL
{
    class Simulation
    {
        IBL BL;
        public Simulation(IBL BL,Drone drone,Action<Drone,int> dronedroneSimulation, Func<bool> needToStop)
        {
            this.BL = BL;
            while (!needToStop())
            {
                //here we need to add the logic of the drone and threadsleep(delay) after every step
            }
        }
    }
}
