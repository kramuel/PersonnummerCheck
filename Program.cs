using System;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables (long is needed)
            long personalNumber;
            bool showMenu = true;

            //menu loop
            while ( showMenu )
            {

                //welcome menu
                PrintMenu();

                //take input
                personalNumber = GetUserInput();

                //exit loop
                if (personalNumber == 0)
                    break;

                //check valid year
                showMenu = ValidYearCheck(personalNumber);

                //check valid month


                //check valid day


                //check valid last numbers (man||woman)


                //return if whole number is valid and if man or woman ((ändra grammatik här))



            }



            //stop
            Console.WriteLine("\n\nPress any button to close");
            Console.ReadKey();
        }

        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("***Välkommen till PersonnummerCheck!***");
            Console.WriteLine("Detta program kollar om det angivna personnumret är giltigt.");
            Console.WriteLine("\nAnge personnumret i 12 siffor (YYYYMMDD****):  ");
            Console.WriteLine("Enter '0' to quit.");
        }

        static long GetUserInput()
        {
            long intUserInput;
            string userInput = Console.ReadLine();

            //adding error handling later :)
            while (!long.TryParse(userInput, out intUserInput))
            {
                Console.WriteLine("fail");
                break;
            }

            if (intUserInput == 0)
                return 0;

            return intUserInput;
        }

        static bool ValidYearCheck(long personalNumber)
        {



            return true;
        }
    }
}
