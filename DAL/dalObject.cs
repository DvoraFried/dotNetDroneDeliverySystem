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

            for (int i = 0; i < 2;i++)
            {
                myBaseStations[i].id = rnd.Next(100, 1000);
            }

            /*            for (int i = 0; i < 5; i++)
                        {
                            myQuadocopters[i].quadoWeight = rnd.Next(0,3);
                        }*/
            for (int i = 0; i < 10; i++)
            {
                int num= rnd.Next(1000, 10000); ;
                myCustomers[i].id = num;
                myCustomers[i].name= /*"customer"+*/num.ToString();
            }


        }
    }

}
