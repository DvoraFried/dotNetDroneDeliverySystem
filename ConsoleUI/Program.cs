using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        //bbbb
        static void printStation(Station station)
        {
            Console.WriteLine($"id: {station.Id}     Name: {station.Name}     ChargeSlots: {station.ChargeSlots}     Latitude: {station.Latitude}     Longitude: {station.Longitude}");
        }
        static void printCustomer(Customer customer)
        {
            Console.WriteLine($"id: {customer.Id}     Name: {customer.Name}     Phone: {customer.Phone}     Latitude: {customer.Latitude}     Longitude {customer.Longitude}");
        }
        static void printDrone(Drone drone)
        {
            Console.WriteLine($"id: {drone.Id}     Model: {drone.Model}     Status: {drone.Status}     Battery: {drone.Battery}     Max whight: {drone.MaxWeight}");
        }
        static void printParcel(Parcel parcel)
        {
            Console.WriteLine($"id: {parcel.Id}     Weight: {parcel.Weight}     Priority: {parcel.Priority}");
        }

        static void adding()
        {
            Console.WriteLine("1 - Add base station");
            Console.WriteLine("2 - Add quadocopter");
            Console.WriteLine("3 - Add customer");
            Console.WriteLine("4 - Add package");
            int choice = Convert.ToInt32(Console.ReadLine());
            int id = 0; double longitude = 0, latitude = 0;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("chargs lots: ");
                    int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    longitude = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    latitude = Convert.ToDouble(Console.ReadLine());
                    DalObject.DalObject.Add.AddStation(longitude, latitude, ChargeSlots);
                    break;
                case 2:
                    Console.WriteLine("wieght category: ");
                    DalObject.DalObject.Add.AddDrone((WeightCategories)Convert.ToInt32(Console.ReadLine()));
                    break;
                case 3:
                    Console.WriteLine("id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("name: ");
                    string userName = Console.ReadLine();
                    Console.WriteLine("phone: ");
                    string phone = Console.ReadLine();
                    Console.WriteLine("longitude: ");
                    longitude = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    latitude = Convert.ToDouble(Console.ReadLine());
                    DalObject.DalObject.Add.AddCustomer(id, userName, phone, longitude, latitude);
                    break;
                case 4:
                    Console.WriteLine("sender id: ");
                    int senderId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("customer id: ");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("wieght category: ");
                    WeightCategories weightCat = (WeightCategories)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("priority: ");
                    Priorities prioritiesCat = (Priorities)Convert.ToInt32(Console.ReadLine());
                    DalObject.DalObject.Add.AddParcel(senderId, customerId, weightCat, prioritiesCat);
                    break;
                default:
                    Console.WriteLine("~~~invalid input~~~"); break;
            }
        }

        static void update()
        {
            Console.WriteLine("1 - Schedule parcel to drone");
            Console.WriteLine("2 - ");
            Console.WriteLine("3 - ");
            Console.WriteLine("4 - Sending a skimmer for charging at a base station");
            Console.WriteLine("5 - Release skimmer from charging at base station");
            int id, choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("parcel's id: ");
                    DalObject.DalObject.Update.Scheduled(Convert.ToInt32(Console.ReadLine()));
                    break;
                case 2:
                    Console.WriteLine("parcel's id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("sender id: ");
                    int sid = Convert.ToInt32(Console.ReadLine());
                    DalObject.DalObject.Update.PickUp(id, sid);
                    break;
                case 3:
                    Console.WriteLine("parcel's id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("customer id: ");
                    DalObject.DalObject.Update.Delivered(id, Convert.ToInt32(Console.ReadLine()));
                    break;
                case 4:
                    Console.WriteLine("drone id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("station id: ");
                    DalObject.DalObject.Update.Charge(id, Convert.ToInt32(Console.ReadLine()));
                    break;
                case 5:
                    Console.WriteLine("drone id: ");
                    DalObject.DalObject.Update.releaseCharge(Convert.ToInt32(Console.ReadLine()));
                    break;
                default:
                    Console.WriteLine("~~~invalid input~~~"); break;
            }
        }

        static void display()
        {
            Console.WriteLine("1 - Base Station display");
            Console.WriteLine("2 - Drone display");
            Console.WriteLine("3 - customer display");
            Console.WriteLine("4 - parcel display");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("station id: ");
                    printStation((((DalObject.DalObject.returnObject.returnStation<IDAL.DO.Station>(Convert.ToInt32(Console.ReadLine()))))));
                    break;
                case 2:
                    Console.WriteLine("drone id: ");
                    printDrone(DalObject.DalObject.returnObject.returnDrone<IDAL.DO.Drone>(Convert.ToInt32(Console.ReadLine())));
                    
                    break;
                case 3:
                    Console.WriteLine("customer id: ");
                    printCustomer(DalObject.DalObject.returnObject.returnCustomer<Customer>(Convert.ToInt32(Console.ReadLine())));
                    
                    break;
                case 4:
                    Console.WriteLine("parcel id: ");
                    printParcel( DalObject.DalObject.returnObject.returnParcel<IDAL.DO.Parcel>(Convert.ToInt32(Console.ReadLine())));
                    break;
                default:
                    Console.WriteLine("~~~invalid input~~~"); break;
            }
        }

        static void displayLists()
        {
            Console.WriteLine("1 - display base stations list");
            Console.WriteLine("2 - display drons list");
            Console.WriteLine("3 - display customer list");
            Console.WriteLine("4 - display parcels list");
            Console.WriteLine("5 - display parcels that have not yet been associated with a drone list");
            Console.WriteLine("6 - display base stations with available charging stations");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    foreach (Station element in DalObject.DalObject.returnArrayObject.returnStationArray()) { printStation(element); }; break;
                case 2:
                    foreach (Drone element in DalObject.DalObject.returnArrayObject.returnDroneArray()) { printDrone(element); }; break;
                case 3:
                    foreach (Customer element in DalObject.DalObject.returnArrayObject.returnCustomerArray()) { printCustomer(element); } ; break;
                case 4:
                    foreach (Parcel element in DalObject.DalObject.returnArrayObject.returnParcelArray()) { printParcel(element);}; break;
                case 5:
                    foreach (Parcel element in DalObject.DalObject.returnArrayObject.returnNotScheduledParcel()) { printParcel(element); }; break;
                case 6:
                    foreach (Station element in DalObject.DalObject.returnArrayObject.returnStationWithChargeSlots()) { printStation(element); }; break;
                default:
                    Console.WriteLine("~~~invalid input~~~"); break;
            }
        }

        static void Main(string[] args)
        {
            int choice = 0;
            while (choice != 5)
            {
                Console.WriteLine("Type your selection:");
                Console.WriteLine("1 - add");
                Console.WriteLine("2 - Update");
                Console.WriteLine("3 - Display");
                Console.WriteLine("4 - List view options");
                Console.WriteLine("5 - Exit:");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        adding(); break;
                    case 2:
                        update(); break;
                    case 3:
                        display(); break;
                    case 4:
                        displayLists(); break;
                    case 5:
                        Console.WriteLine("bye!"); break;
                }
            }
        }
    }
}
