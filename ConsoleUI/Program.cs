using System;

void addingOptions()
{
    Console.WriteLine("1 - Add base station");
    Console.WriteLine("2 - Add quadocopter");
    Console.WriteLine("3 - Add customer");
    Console.WriteLine("4 - Add package");
}

void updateOptions()
{
    Console.WriteLine("1 - Assign a package to a skimmer");
    Console.WriteLine("2 - ");
    Console.WriteLine("3 - ");
    Console.WriteLine("4 - Sending a skimmer for charging at a base station");
    Console.WriteLine("5 - Release skimmer from charging at base station");
}

void displayOptions()
{
    Console.WriteLine("1 - Base Station display");
    Console.WriteLine("2 - Skimmer display");
    Console.WriteLine("3 - customer display");
    Console.WriteLine("4 - package display");
}

void displayLists()
{
    Console.WriteLine("1 - display base stations list");
    Console.WriteLine("2 - display skimmers list");
    Console.WriteLine("3 - display customer list");
    Console.WriteLine("4 - display packages list");
    Console.WriteLine("5 - display packages that have not yet been associated with a skimmer list");
    Console.WriteLine("6 - display base stations with available charging stations");
}


namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

        }
    }
}
