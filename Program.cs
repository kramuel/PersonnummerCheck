using System;
using System.Reflection.Metadata.Ecma335;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables 
            string personalNumber;
            int year, month, day, birthNumber;
            string plusOrMinus;

            //menu loop
            while (true)
            {
                //welcome menu
                PrintMenu();

                //take input and check if characters are numbers. outputs plusOrMinus sign
                personalNumber = GetUserInput(out plusOrMinus);

                //exit loop(program)
                if (personalNumber == "0")
                    break;

                //splits personalnumber into substrings and parses to integers
                SplitPersonalNumber(personalNumber, plusOrMinus, out year, out month, out day, out birthNumber);

                //checks if year, month and day is Valid
                if (ValidDateCheck(year, month, day))
                {
                    Console.WriteLine("Detta personnummer är VALIDERAT/GILTIGT OCH KORREKT");
                    //check valid last numbers (man||woman)  3 numbers can only be 000-999 no check needed
                    if (CheckIfWoman(birthNumber))
                    {
                        Console.WriteLine("Det juridiska könet är KVINNA");
                    }
                    else
                    {
                        Console.WriteLine("Det juridiska könet är MAN");
                    }
                }
                else
                {
                    Console.WriteLine("Detta personnummer är ICKE GILTIGT");
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
        /// Input string has to be numbers with these formats: YYYYMMDDnnnc || YYMMDD-nnnc
        /// </summary>
        /// <returns></returns>
        static string GetUserInput(out string plusOrMinus)
        {
            string userInput = Console.ReadLine();
            plusOrMinus = "";
            bool punctMarkOK = true;

            //wrong input loop
            while (true)
            {
                if (userInput.Length == 12 || userInput.Length == 11 || userInput == "0")//check size
                {
                    if (userInput.Length == 11)//if length is 11 = format is YYMMDD-nnnc
                    {
                        //removs plusOrMinus sign so string can be parsed. outputs plusOrMinus
                        if (userInput[6] == '+' || userInput[6] == '-')
                        {
                            plusOrMinus = userInput.Substring(6, 1);
                            userInput = userInput.Remove(6, 1);
                            punctMarkOK = true;
                        }
                        else//if anyhting other than '+' or '-', this bool prevents the break from loop
                        {
                            punctMarkOK = false;
                        }
                    }
                    //checks if all characters are digits ( and if the punctuation mark was not correct )
                    if (long.TryParse(userInput, out _) && punctMarkOK)
                    {
                        break; // succesful
                    }
                }

                Console.WriteLine("Felaktig inmatning, försök igen: ");
                userInput = Console.ReadLine();
            }


            if (userInput == "0")
                return "0";
            else
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
        static void SplitPersonalNumber(string personalNumber, string plusOrMinus, out int year, out int month, out int day, out int birthNumber)
        {
            //YYYYMMDDnnnc
            if (personalNumber.Length == 12)
            {
                year = int.Parse(personalNumber.Substring(0, 4));
                month = int.Parse(personalNumber.Substring(4, 2));
                day = int.Parse(personalNumber.Substring(6, 2));
                birthNumber = int.Parse(personalNumber.Substring(8, 3));
            }
            //YYMMDD-nnnc || YYMMDD+nnnc
            else if (personalNumber.Length == 10)
            {
                year = int.Parse(personalNumber.Substring(0, 2));
                month = int.Parse(personalNumber.Substring(2, 2));
                day = int.Parse(personalNumber.Substring(4, 2));
                birthNumber = int.Parse(personalNumber.Substring(6, 3));

                if (plusOrMinus == "+")
                {
                    year += 1900;
                }
                else if (plusOrMinus == "-")
                {
                    year += 2000;
                }
            }
            else
            {
                year = 0;
                month = 0;
                day = 0;
                birthNumber = 0;
            }
        }

        /// <summary>
        /// Checks if day is valid, regards to month and year
        /// </summary>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        static bool ValidDateCheck(int year, int month, int day)
        {
            //return false if year is not in specified range ( 1753 - 2020 )
            if (! ( year >= 1753 && year <= 2020 ) )
                return false;

            //day can be between 1 and 31 in these months (jan,mar,may,jul,aug,oct,dec)
            if ((day >= 1 && day <= 31) && (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12))
            {
                return true;
            }//day can be between 1 and 30 in these months ( apr,jun,sep,nov )
            else if ((day >= 1 && day <= 30) && (month == 4 || month == 6 || month == 9 || month == 11))
            {
                return true;
            }//day can be between 1 and 28 in feb (if not leapyear)
            else if ((day >= 1 && day <= 28) && month == 2)
            {
                return true;
            }//day can be 29 in feb if leapyear
            else if (day == 29 && month == 2 && LeapYearCheck(year))
            {
                return true;
            }
            else//all other months and days returns false
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
            return birthNumber % 2 == 0;
        }

        ///// <summary>
        ///// Checks if input was in format YYYY or YY, adds 1900or2000 depending on input
        ///// Returns true if year is in valid range ( 1753 - 2020 )
        ///// </summary>
        ///// <param name="year"></param>
        ///// <param name="plusOrMinus"></param>
        ///// <returns></returns>
        //static bool ValidYearCheck(ref int year, string plusOrMinus)
        //{
        //    if (plusOrMinus == "+")
        //    {
        //        year += 1900;
        //    }
        //    else if (plusOrMinus == "-")
        //    {
        //        year += 2000;
        //    }

        //    //returns true if year is 1753-2020
        //    if (year >= 1753 && year <= 2020)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
