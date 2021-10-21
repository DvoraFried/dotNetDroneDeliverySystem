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
        static internal Station[] myBaseStations = new Station[5];
        static internal Drone[] myQuadocopters = new Drone[10];
        static internal Customer[] myCustomers = new Customer[100];
        static internal Parcel[] myPackages = new Parcel[1000];
        internal class Config
        {
            static internal int baseStationsIndex = 0;
            static internal int quadocoptersIndex=0;
            static internal int customersIndex = 0;
            static internal int packagesIndex = 0;
            static internal int idNumber;
        }
//=====================================================================

        static void Initialize()
        {
            Random rnd = new Random();
//=====================================================================
//rendering information for 2 baseStations
//=====================================================================
            for (int i = 0; i < 2;i++)
            {
                Config.baseStationsIndex++;
                myBaseStations[i].Id = Config.baseStationsIndex + 1;
                myBaseStations[i].Name = "station"+(Config.baseStationsIndex + 1).ToString();
                myBaseStations[i].ChargeSlots = rnd.Next(2, 4);
                myBaseStations[i].Longitude = rnd.Next(0, 24);
                myBaseStations[i].Latitude = rnd.Next(0,180);
            }
//=====================================================================           
//rendering information for five quadocopter
//=====================================================================
            for (int i = 0; i < 5; i++)
            {
                Config.quadocoptersIndex++;
                myQuadocopters[i].Id = Config.quadocoptersIndex ;
                myQuadocopters[i].Model ="model "+(Config.quadocoptersIndex ).ToString();
                //~~~~~~~~~~~~~~~~~~~~~~weight category is missing here in this line!!~~~~~~~~~~~~~~~~
                myQuadocopters[i].Battery = rnd.Next(10,101);

                int num = rnd.Next(0,3);
                switch (num)
                {
                    case 0:
                        myQuadocopters[i].Status = DroneStatuses.empty;
                        break;
                    case 1:
                        myQuadocopters[i].Status = DroneStatuses.maintenance;
                        break;
                    case 2:
                        myQuadocopters[i].Status = DroneStatuses.Shipping;
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
                myPackages[i].SenderId= rnd.Next(1, (Config.customersIndex)+1);
                myPackages[i].TargetId = rnd.Next(1, (Config.customersIndex) + 1);
                int num = rnd.Next(0, 3);
                switch (num)
                {
                    case 0:
                        myPackages[i].Weight = WeightCategories.light;
                        break;
                    case 1:
                        myPackages[i].Weight = WeightCategories.medium;
                        break;
                    case 2:
                        myPackages[i].Weight = WeightCategories.heavy;
                        break;
                }
                
                num = rnd.Next(0, 3);
                switch (num)
                {
                    case 0:
                        myPackages[i].Priority = Priorities.emergency;
                        break;
                    case 1:
                        myPackages[i].Priority = Priorities.rapid;
                        break;
                    case 2:
                        myPackages[i].Priority = Priorities.usual;
                        break;
                }
                myPackages[i].DroneId = 0;
                //~~~~~~~~~~~~~~~~~~~~~~~`there is times to write here at this line but i'm not sure what to do:(~~~~~~~~~~~~~~~
            }
            
        }
    }

}
