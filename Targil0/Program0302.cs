using System;

namespace Targil0
{
    class Program
    {
        static void Main(string[] args)
        {
            welcom0302();
            welcom8721();
            Console.ReadKey();

        }
        static partial void welcom8721();

        private static void welcom0302()
        {
            Console.WriteLine("enter your name");
            string userName = Console.ReadLine();
            Console.WriteLine("welcom {0}", userName);
        }
    }
}
