using System;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables 
            string personalNumber;
            int year, month, day;
            bool showMenu = true;
            bool validation = false;
            bool woman;

            //menu loop
            while (showMenu)
            {

                //welcome menu
                PrintMenu();

                //take input
                personalNumber = GetUserInput();

                //exit loop(program)
                if (personalNumber == "0")
                    break;

                //check valid year
                validation = ValidYearCheck(personalNumber, out year);

                //check valid month
                validation = ValidMonthCheck(personalNumber, out month);

                //check valid day
                validation = ValidDayCheck(personalNumber, year, month, out day);

                //check valid last numbers (man||woman)  3 numbers can only be 000-999 no check needed
                woman = CheckIfWoman(personalNumber);

                //check if validated or not and output if the p-number is correct or not
                if (validation == false)
                {
                    Console.WriteLine("The Personalnumber IS NOT VALIDATED");
                }
                else
                {
                    Console.WriteLine("The Personalnumber VALIDATED, CORRECT");
                    if (woman)
                        Console.WriteLine("This persons legal gender is Woman");
                    else
                        Console.WriteLine("This persons legal gender is Man");
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
        static bool ValidYearCheck(string personalNumber, out int intYear)
        {
            string year;

            //takes 4 firsts digits, change later if only 10 digits is entered
            year = personalNumber.Substring(0, 4);

            intYear = int.Parse(year);

            //checks if YYYY is 1753-2020, return true if it is
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
        static bool ValidMonthCheck(string personalNumber, out int intMonth)
        {
            string month;

            //takes 5th and 6th character and parses to an Integer
            month = personalNumber.Substring(4, 2);
            intMonth = int.Parse(month);

            //checks if MM is 1-12, return true if it is
            if (intMonth >= 1 && intMonth <= 12)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Checks if day is valid, regards to month and year
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidDayCheck(string personalNumber, int year, int month, out int intDay)
        {
            string day;
            bool leapYear;
 
            day = personalNumber.Substring(6, 2);
            intDay = int.Parse(day);

            leapYear = LeapYearCheck(year);

            //day can be between 1 and 31 in these months (jan,mar,may,jul,aug,oct,dec)
            if ((intDay >= 1 && intDay <= 31) && (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12))
            {
                return true;
            }//day can be between 1 and 30 in these months ( apr,jun,sep,nov
            else if ((intDay >= 1 && intDay <= 30) && (month == 4 || month == 6 || month == 9 || month == 11))
            {
                return true;
            }//day can be between 1 and 28 in feb (if not leapyear)
            else if ((intDay >= 1 && intDay <= 28) && month == 2)
            {
                return true;
            }//day can be 29 in feb if leapyear
            else if (intDay == 29 && month == 2 && leapYear)
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
        static bool CheckIfWoman(string personalNumber)
        {
            string birthNumber;

            birthNumber = personalNumber.Substring(8, 3);
            int intBirthNum = int.Parse(birthNumber);

            if (intBirthNum % 2 == 0)
                return true;
            else
                return false;
        }
    }
}
