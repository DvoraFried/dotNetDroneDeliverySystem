using System;
using BO;
using static BO.Enum;
using System.Text;
using BlApi;
using static BO.Exceptions;
namespace ConsoleUI_BL
{
    class Program
    {
        static void addObject()
        {
            Console.WriteLine("Enter your choice to add:\n 0.Station \n 1.Drone\n 2.customer\n 3.Parcel ");
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                Console.WriteLine("Enter max weight of drone category: \nLight : 0\nMedium : 1\nHeavy : 2");
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
                Console.WriteLine(e.Message);
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
                bl.AddCustomer(id, name, phone, longitude, latitude);
            }
            catch (FormatException)
            {
                Console.WriteLine("~ data reciving error ~");
            }
            catch (OverflowException)
            {
                Console.WriteLine("~ data reciving error ~");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                Console.WriteLine("Enter weight of parcel: \nLight : 0\nMedium : 1\nHeavy : 2");
                int weight = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter priority of parcel \nusual : 0\nrapid : 1\nemergency :2");
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
                Console.WriteLine(e.Message);
            }
        }

        public static void updateObject()
        {
            Console.WriteLine("Enter your choice to update:\n 0.Station \n 1.Drone\n 2.customer\n 3.send drone to charge\n 4. release drone from charging\n 5.assigning parcel to drone\n 6.collection of a parcel by drone\n 7.delivery of a parcel by drone ");
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
                    updateStation(); break;
                case 1:
                    updateDrone(); break;
                case 2:
                    updateCustomer(); break;
                case 3:
                    sendDroneToCharge(); break;
                case 4:
                    ReleaseDroneFromCharging(); break;
                case 5:
                case 6:
                case 7:
                    parcelAndDrone(choice); break;
                default:
                    Console.WriteLine("== ERROR =="); break;
            }
        }

        public static void updateDrone()
        {
            try
            {
                Console.WriteLine("Enter drone's id: ");
                int droneID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter new model: ");
                string model = Console.ReadLine();
                bl.UpDateDroneName(droneID, model);
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
                Console.WriteLine(e.Message);
            }
        }

        public static void updateStation()
        {
            try
            {
                Console.WriteLine("Enter station's id: ");
                int stationID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter station's name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter amount of charging slots: ");
                int chargingLots = Convert.ToInt32(Console.ReadLine());
                bl.UpDateStationData(stationID, name, chargingLots);
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
                Console.WriteLine(e.Message);
            }
        }

        public static void updateCustomer()
        {
            try
            {
                Console.WriteLine("Enter costumer's id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter costumer's Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter costumer's Phone: ");
                string phone = Console.ReadLine();
                bl.UpDateCustomerData(id, name, phone);
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
                Console.WriteLine(e.Message);
            }
        }

        public static void sendDroneToCharge()
        {
            try
            {
                Console.WriteLine("Enter drone's id: ");
                bl.SendDroneToCharge(Convert.ToInt32(Console.ReadLine()));
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
                Console.WriteLine(e.Message);
            }
        }

        public static void ReleaseDroneFromCharging()
        {
            try
            {
                Console.WriteLine("Enter drone's id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                bl.ReleaseDroneFromCharging(id);
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
                Console.WriteLine(e.Message);
            }
        }
        public static void parcelAndDrone(int choice)
        {
            try
            {
                Console.WriteLine("Enter drone's id: ");
                switch (choice)
                {
                    case 5:
                        bl.AssigningPackageToDrone(Convert.ToInt32(Console.ReadLine())); break;
                    case 6:
                        bl.CollectionOfAParcelByDrone(Convert.ToInt32(Console.ReadLine())); break;
                    default:
                        bl.DeliveryOfAParcelByDrone(Convert.ToInt32(Console.ReadLine())); break;
                }
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
                Console.WriteLine(e.Message);
            }
        }

        static void display()
        {
            Console.WriteLine("Enter your choice to display:\n0.Station \n1.Drone\n2.customer\n3.parcel");
            int choice = -1;
            int id = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter item's id:");
                id = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        bl.DisplayStatoin(id); break;
                    case 1:
                        bl.DisplayDrone(id); break;
                    case 2:
                        bl.DisplayCustomer(id); break;
                    case 3:
                        bl.DisplayParcel(id); break;
                    default:
                        Console.WriteLine("== ERROR =="); break;
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Catch ArgumentNullException");
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
                Console.WriteLine(e.Message);
            }
        }
        static void displayLists()
        {
            Console.WriteLine("Enter your choice to display:\n 0.Stations \n 1.Drones\n 2.Customers\n 3.Parcels\n4.Parcels that have not yet been associated with a drone");
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
                    bl.DisplayStatoinList(); break;
                case 1:
                    bl.DisplayDroneList(); break;
                case 2:
                    bl.DisplayCustomerList(); break;
                case 3:
                    bl.DisplayParcelList(); break;
                case 4:
                    bl.DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone(); break;
                default:
                    Console.WriteLine("== ERROR =="); break;
            }
        }
        public static BlApi.IBL bl = BLFactory.factory();
        static void Main(string[] args)
        {
            try
            {
                int choice = -1;
                while (choice != 5)
                {
                    Console.WriteLine("Choose your option:\n1.Add an object\n2.Update object\n3.Display object by Id\n4.display list of objects\n5.Exit");

                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            addObject(); break;
                        case 2:
                            updateObject(); break;
                        case 3:
                            display(); break;
                        case 4:
                            displayLists(); break;
                        case 5:
                            Console.WriteLine("bye!");
                            return;
                        default:
                            Console.WriteLine("error");
                            break;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Catch ArgumentNullException");
            }
        }
    }
}
