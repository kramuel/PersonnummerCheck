using System;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables 
            string personalNumber;
            int year, month, day, birthNumber;

            //menu loop
            while (true)
            {

                //welcome menu
                PrintMenu();

                //take input and check if numbers
                personalNumber = GetUserInput();

                //exit loop(program)
                if (personalNumber == "0")
                    break;

                //assign substrings
                SplitPersonalNumber(personalNumber, out year, out month, out day, out birthNumber);

                //checks if year, month and day Valid
                if (ValidYearCheck(year) && ValidMonthCheck(month) && ValidDayCheck(year, month, day))
                {
                    Console.WriteLine("The Personalnumber VALIDATED, CORRECT");
                    //check valid last numbers (man||woman)  3 numbers can only be 000-999 no check needed
                    if (CheckIfWoman(birthNumber))
                        Console.WriteLine("This persons legal gender is Woman");
                    else
                        Console.WriteLine("This persons legal gender is Man");
                }
                else
                {
                    Console.WriteLine("The Personalnumber IS NOT VALIDATED");
                }

                //pause
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
            }
            //stop
            Console.WriteLine("\n\nPress any key to close");
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
            while (userInput.Length != 12)
            {
                Console.WriteLine("Incorrect input, please try again: ");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        /// <summary>
        /// Splits personalNumber string into substrings, and parses them to integers.
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="birthNumber"></param>
        static void SplitPersonalNumber(string personalNumber, out int year, out int month, out int day, out int birthNumber)
        {
            year = int.Parse(personalNumber.Substring(0, 4));
            month = int.Parse(personalNumber.Substring(4, 2));
            day = int.Parse(personalNumber.Substring(6, 2));
            birthNumber = int.Parse(personalNumber.Substring(8, 3));
        }

        /// <summary>
        /// Checks if the year is OK(valid)
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidYearCheck(int year)
        {
            //returns true if year is 1753-2020
            if (year >= 1753 && year <= 2020)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if Month is OK(valid)
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidMonthCheck(int month)
        {
            //returns true if month is 1-12
            if (month >= 1 && month <= 12)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Checks if day is valid, regards to month and year
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidDayCheck(int year, int month, int day)
        {
            bool leapYear;

            leapYear = LeapYearCheck(year);

            //day can be between 1 and 31 in these months (jan,mar,may,jul,aug,oct,dec)
            if ((day >= 1 && day <= 31) && (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12))
            {
                return true;
            }//day can be between 1 and 30 in these months ( apr,jun,sep,nov
            else if ((day >= 1 && day <= 30) && (month == 4 || month == 6 || month == 9 || month == 11))
            {
                return true;
            }//day can be between 1 and 28 in feb (if not leapyear)
            else if ((day >= 1 && day <= 28) && month == 2)
            {
                return true;
            }//day can be 29 in feb if leapyear
            else if (day == 29 && month == 2 && leapYear)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Returns true if year is a leap year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        static bool LeapYearCheck(int year)
        {
            //boolean that checks if year is divisible by 400, then if divisible by 4 and NOT by 100
            return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
        }

        /// <summary>
        /// Returns true if woman. Checks if BirthNumber is odd or even
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool CheckIfWoman(int birthNumber)
        {
            //returns true if birthNumber is even = it is a woman
            if (birthNumber % 2 == 0)
                return true;
            else
                return false;
        }
    }
}
