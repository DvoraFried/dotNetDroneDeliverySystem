using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        static internal quadocopter[] myQuadocopters = new quadocopter[10];
        static internal baseStation[] myBaseStations = new baseStation[5];
        static internal customer[] myCustomers = new customer[100];
        static internal package[] myPackages = new package[1000];
        internal class Config
        {
            static internal int quadocoptersIndex=0;
            static internal int baseStationsIndex = 0;
            static internal int customersIndex = 0;
            static internal int packagesIndex = 0;
            static internal int idNumber;
        }

        static void Initialize()
        {

        }
    }

}
