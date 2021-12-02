using System;
using IBL.BO;
using static IBL.BO.EnumBL;
using System.Text;
using IBL;
using static IBL.BO.Exceptions;
namespace ConsoleUI_BL
{
    class Program
    {
        static void addObject()
        {
            Console.WriteLine("Enter your choice to add:\n 0.Station \n 1.Drone\n 2.CLient\n 3.Parcel ");
            int choice = -1;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Catch ArgumentNullException");
            }
            switch (choice)
            {
                case 0:
                    addStation();
                    break;
                case 1:
                    addDrone();
                    break;
                case 2:
                    addCustomer();
                    break;
                case 3:
                    addParcel();
                    break;
                default:
                    Console.WriteLine("== ERROR ==");
                    break;
            }
        }

        private static void addStation()
        {
            try
            {
                Console.WriteLine("Enter station id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a station Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter a Latitude");
                int latitude = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a Longitude");
                int longitude = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter amount of Availble charging slots: ");
                int chargeSlot = Convert.ToInt32(Console.ReadLine());
                bl.AddStation(id, name, latitude, longitude, chargeSlot);
            }
            catch (FormatException)
            {
                Console.WriteLine("~ data reciving error~");
            }
            catch (OverflowException)
            {
                Console.WriteLine("~ data reciving error~");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void addDrone()
        {
            try
            {
                Console.WriteLine("Enter the ordinal number of the drone: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the model name");
                string Model = Console.ReadLine();
                Console.WriteLine("Enter max weight of drone \ncategory: \nLight : 0\n, Medium : 1\n, Heavy : 2\n: ");
                int MaxWeight = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter station id for charging the drone:");
                int stationId = Convert.ToInt32(Console.ReadLine());
                bl.AddDrone(id, Model, (WeightCategoriesBL)MaxWeight, stationId);
            }
            catch (FormatException)
            {
                Console.WriteLine("~ data reciving error~");
            }
            catch (OverflowException)
            {
                Console.WriteLine("~data reciving error~");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static void addCustomer()
        {
            try
            {
                Console.WriteLine("Enter costumer's id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter costumer's Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter costumer's Phone: ");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter a Latitude: ");
                int latitude = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a Longitude: ");
                int longitude = Convert.ToInt32(Console.ReadLine());
                bl.AddCustomer(id, name, phone, longitude,latitude);
            }
            catch (FormatException)
            {
                Console.WriteLine("~ data reciving error~");
            }
            catch (OverflowException)
            {
                Console.WriteLine("~data reciving error~");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void addParcel()
        {
            try
            {
                Console.WriteLine("Enter sender's id: ");
                int senderID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter target's id: ");
                int targetId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter weight of parcel (Light =1, Medium=2, Heavy=3): ");
                int weight = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter priority of parcel (Regular=1, Fast=2, Emergency=3 ): ");
                int priority = Convert.ToInt32(Console.ReadLine());
                bl.AddParcel(senderID, targetId, (WeightCategoriesBL)weight, (PrioritiesBL)priority);
            }
            catch (FormatException)
            {
                Console.WriteLine("~ data reciving error~");
            }
            catch (OverflowException)
            {
                Console.WriteLine("~data reciving error~");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static IBL.IBL bl;
        static void Main(string[] args)
        {
            int choice =-1;
            while (choice != 5)
            {
                Console.WriteLine("Choose your option:\n 0.Add an object\n 1.Update object\n 2.Display object by Id\n 3.display list of objects\n 4.Exit");

                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        addObject(); break;
/*                    case 2:
                        update(); break;
                    case 3:
                        display(); break;
                    case 4:
                        displayLists(); break;
                    case 5:*/
                        Console.WriteLine("bye!");
                        return;
                    default:
                        Console.WriteLine("error");
                        break;
                }
            }
        }
    }
}
