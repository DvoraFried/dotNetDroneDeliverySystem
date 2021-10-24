using System;

namespace ConsoleUI
{
    class Program
    {
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
                    Console.WriteLine("id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("name: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("chargs lots: ");
                    int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    longitude = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    latitude = Convert.ToDouble(Console.ReadLine());
                    // כאן צריך לעשות add station  אבל הפונקציה שלנו לא מקבלת נתונים
                    break;
                case 2:
                    Console.WriteLine("enter a drone number: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("enter a drone Model: ");
                    string Model = Console.ReadLine();
                    Console.WriteLine("enter a wieght category: ");
                    //
                    Console.WriteLine("enter a battery status: ");
                    double battery = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("enter a drone status: ");
                    //
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
                    // כאן צריך לעשות add customer אבל הפונקציה שלנו לא מקבלת נתונים
                    break;
                case 4:
                    Console.WriteLine("parcel id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("sender id: ");
                    int senderId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("sender id: ");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("wieght category: ");
                    //
                    Console.WriteLine("priority: ");
                    //
                    Console.WriteLine("drone id: ");
                    int droneId = Convert.ToInt32(Console.ReadLine());
                    // כאן צריך לעשות add parcel אבל הפונקציה שלנו לא מקבלת נתונים
                    break;
                default:
                    Console.WriteLine("~~~invalid input~~~"); break;     
            }
        }

        static void update()
        {
            Console.WriteLine("1 - Assign a package to a skimmer");
            Console.WriteLine("2 - ");
            Console.WriteLine("3 - ");
            Console.WriteLine("4 - Sending a skimmer for charging at a base station");
            Console.WriteLine("5 - Release skimmer from charging at base station");


        }

        static void display()
        {
            Console.WriteLine("1 - Base Station display");
            Console.WriteLine("2 - Skimmer display");
            Console.WriteLine("3 - customer display");
            Console.WriteLine("4 - package display");
        }

        static void displayLists()
        {
            Console.WriteLine("1 - display base stations list");
            Console.WriteLine("2 - display skimmers list");
            Console.WriteLine("3 - display customer list");
            Console.WriteLine("4 - display packages list");
            Console.WriteLine("5 - display packages that have not yet been associated with a skimmer list");
            Console.WriteLine("6 - display base stations with available charging stations");
        }
        static void Main(string[] args)
        {
            int choice = 0;
            while (choice != 5)
            {
                Console.WriteLine("Type your selection:");
                Console.WriteLine("1 - Insert options");
                Console.WriteLine("2 - Update options");
                Console.WriteLine("3 - Display options");
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
