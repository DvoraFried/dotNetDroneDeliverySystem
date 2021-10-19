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
        static internal baseStation[] myBaseStations = new baseStation[5];
        static internal quadocopter[] myQuadocopters = new quadocopter[10];
        static internal customer[] myCustomers = new customer[100];
        static internal package[] myPackages = new package[1000];
        internal class Config
        {
            static internal int baseStationsIndex = 0;
            static internal int quadocoptersIndex=0;
            static internal int customersIndex = 0;
            static internal int packagesIndex = 0;
            static internal int idNumber;
        }

        static void Initialize()
        {
            Random rnd = new Random();
//rendering id number for the 2 baseStations
            for (int i = 0; i < 2;i++)
            {
                myBaseStations[i].id = rnd.Next(100, 1000);
            }
//rendering a state for five quadocopter
            for (int i = 0; i < 5; i++)
            {
                int num = rnd.Next(0,3);
                switch (num)
                {
                    case 0:
                        myQuadocopters[i].quadoState = quadocopterState.empty;
                        break;
                    case 1:
                        myQuadocopters[i].quadoState = quadocopterState.maintenance;
                        break;
                    case 2:
                        myQuadocopters[i].quadoState = quadocopterState.Shipping;
                        break;
                }
            }
//rendering for ten customers id number and name
            for (int i = 0; i < 10; i++)
            {
                int num= rnd.Next(1000, 10000); 
                myCustomers[i].id = num;
                myCustomers[i].name= /*"customer"+*/num.ToString();
            }
//rendering for ten customers id number and name
            for (int i = 0; i < 10; i++)
            {
                int num = rnd.Next(0, 3);
                switch (num)
                {
                    case 0:
                        myPackages[i].packagePriority = priority.emergency;
                        break;
                    case 1:
                        myPackages[i].packagePriority = priority.rapid;
                        break;
                    case 2:
                        myPackages[i].packagePriority = priority.usual;
                        break;
                }
            }


        }
    }

}
