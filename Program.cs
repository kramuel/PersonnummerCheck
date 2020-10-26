using System;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //welcome menu
            PrintMenu();

            //take input


            //check valid year


            //check valid month


            //check valid day


            //check valid last numbers (man||woman)


            //return if whole number is valid and if man or woman ((ändra grammatik här))

            // ask for nother number(loop?)





            //stop
            Console.ReadKey();
        }

        static void PrintMenu()
        {
            Console.WriteLine("***Välkommen till PersonnummerCheck!***");
            Console.WriteLine("Detta program kollar om det angivna personnumret är giltigt.");
            Console.WriteLine("\nAnge personnumret i 12 siffor (YYYYMMDD****) :  ");
        }
    }
}
