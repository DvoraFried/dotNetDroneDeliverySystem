using System;
using 
namespace ConsoleUI_BL
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
                    Console.WriteLine("chargs lots: ");
                    int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    longitude = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("longitude: ");
                    latitude = Convert.ToDouble(Console.ReadLine());
                    
                    break;
                case 2:
                    Console.WriteLine("wieght category: ");
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
                    break;
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
/*                    case 2:
                        update(); break;
                    case 3:
                        display(); break;
                    case 4:
                        displayLists(); break;*/
                    case 5:
                        Console.WriteLine("bye!"); break;
                }
            }
        }
    }
}
