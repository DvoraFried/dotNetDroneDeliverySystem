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
//=====================================================================
//rendering information for 2 baseStations
//=====================================================================
            for (int i = 0; i < 2;i++)
            {
                Config.baseStationsIndex++;
                myBaseStations[i].id = Config.baseStationsIndex + 1;
                myBaseStations[i].name = "station"+(Config.baseStationsIndex + 1).ToString();
                myBaseStations[i].loadStation = rnd.Next(2, 4);
                myBaseStations[i].longitude = rnd.Next(0, 24);
                myBaseStations[i].latitude = rnd.Next(0,180);
            }
//=====================================================================           
//rendering information for five quadocopter
//=====================================================================
            for (int i = 0; i < 5; i++)
            {
                Config.quadocoptersIndex++;
                myQuadocopters[i].id = Config.quadocoptersIndex ;//id number
                myQuadocopters[i].model ="model "+(Config.quadocoptersIndex ).ToString();//quadocopter model
                //weight category is missing here!!!!!
                myQuadocopters[i].battery = rnd.Next(10,101);//battery 

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
//=====================================================================            
//rendering information for ten customers 
//=====================================================================
            for (int i = 0; i < 10; i++)
            {
                Config.customersIndex++;
                myCustomers[i].id = Config.customersIndex ;
                myCustomers[i].name = "customer"+(Config.customersIndex).ToString();
                myCustomers[i].telNumber = rnd.Next(5000000, 60000000);
                myCustomers[i].longitude = rnd.Next(0, 24);
                myCustomers[i].latitude = rnd.Next(0, 180);
            }
//=====================================================================            
//rendering information for 10 packages
//=====================================================================
            for (int i = 0; i < 10; i++)
            {
                Config.packagesIndex++;
                myPackages[i].id = Config.packagesIndex;
                myPackages[i].senderId= rnd.Next(1, (Config.customersIndex)+1);
                myPackages[i].getterId = rnd.Next(1, (Config.customersIndex) + 1);
                int num = rnd.Next(0, 3);
                switch (num)
                {
                    case 0:
                        myPackages[i].packageWeight = weightCategories.light;
                        break;
                    case 1:
                        myPackages[i].packageWeight = weightCategories.medium;
                        break;
                    case 2:
                        myPackages[i].packageWeight = weightCategories.heavy;
                        break;
                }
                
                num = rnd.Next(0, 3);
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
                myPackages[i].quadocopterId = 0;
                //there is times to write, but i'm not sure what to do:(
            }
            
            

        }
    }

}
