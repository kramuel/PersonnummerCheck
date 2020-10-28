using System;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables (long is needed)
            string personalNumber;
            bool showMenu = true;
            bool validation = false;

            //menu loop
            while ( showMenu )
            {

                //welcome menu
                PrintMenu();

                //take input
                personalNumber = GetUserInput();

                //exit loop(program)
                if (personalNumber == "0")
                    showMenu = false;

                //check valid year
                validation = ValidYearCheck(personalNumber);

                //check valid month
                validation = ValidMonthCheck(personalNumber);

                //check valid day


                //check valid last numbers (man||woman)


                //return if whole number is valid and if man or woman ((ändra grammatik här))
                //alltså if-sats om validation blev false eller true
                if (validation == false)
                {
                    Console.WriteLine("The Personalnumber IS NOT VALIDATED");
                }
                else
                {
                    Console.WriteLine("The Personalnumber VALIDATED, CORRECT");
                }


                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
            }



            //stop
            Console.WriteLine("\n\nPress any button to close");
            Console.ReadKey();
        }


        /// <summary>
        /// Clears Console and prints menu.
        /// </summary>
        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("***Välkommen till PersonnummerCheck!***");
            Console.WriteLine("Detta program kollar om det angivna personnumret är giltigt.");
            Console.WriteLine("\nAnge personnumret i 12 siffor (YYYYMMDD****):  ");
            Console.WriteLine("Enter '0' to quit.");
        }


        /// <summary>
        /// Takes users input and returns a validated string. Loops if bad string is entered.
        /// Input string has to be 12 characters.
        /// </summary>
        /// <returns></returns>
        static string GetUserInput()
        {
            long intUserInput;
            string userInput = Console.ReadLine();

            //adding better error handling later :)
            //checks if the string consists of numbers
            while (!long.TryParse(userInput, out intUserInput))
            {
                Console.WriteLine("Incorrect input, please try again: ");
                userInput = Console.ReadLine();
            }

            if (userInput == "0")
                return "0";

            // add "or 10" digits later
            while ( userInput.Length != 12)
            {
                Console.WriteLine("Incorrect input, please try again: ");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        /// <summary>
        /// Checks if the year is OK(valid)
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidYearCheck(string personalNumber)
        {
            string year;
            int intYear;

            //takes 4 firsts digits, change later if only 10 digits is entered
            year = personalNumber.Substring(0, 4);

            intYear = int.Parse(year);

            
            if (intYear >= 1753 && intYear <= 2020)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if Month is OK(valid)
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidMonthCheck(string personalNumber)
        {
            throw new NotImplementedException();
        }
    }
}
