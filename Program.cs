using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace PersonnummerCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables 
            string personalNumber;
            int year, month, day, birthNumber, controlNumber;
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
                SplitPersonalNumber(personalNumber, plusOrMinus, out year, out month, out day, out birthNumber, out controlNumber);

                //checks if year, month and day AND checks controlnumber with luhn-algorithm
                if (ValidDateCheck(year, month, day) && ControlNumberCheck(personalNumber, controlNumber))
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
                    //obs^ (this only writes out if validation is succesful, no use calculating gender if there is something wrong?)
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
            Console.WriteLine("\nAnge personnumret i 12 siffor (YYYYMMDD****) eller 10 siffror (YYMMDD-****):  ");
            Console.WriteLine("\n\nEnter '0' to quit.");
            Console.SetCursorPosition(0, 4);
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
            bool punctMarkOK;

            //wrong input loop
            while (true)
            {
                //resets every loop incase user switches input format :)
                punctMarkOK = true;
                //check size
                if (userInput.Length == 12 || userInput.Length == 11 || userInput == "0")
                {
                    //if length is 11 = format is YYMMDD-nnnc
                    if (userInput.Length == 11)
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
        static void SplitPersonalNumber(string personalNumber,
                                        string plusOrMinus,
                                        out int year,
                                        out int month,
                                        out int day,
                                        out int birthNumber,
                                        out int controlNumber)
        {
            //YYYYMMDDnnnc
            if (personalNumber.Length == 12)
            {
                year = int.Parse(personalNumber.Substring(0, 4));
                month = int.Parse(personalNumber.Substring(4, 2));
                day = int.Parse(personalNumber.Substring(6, 2));
                birthNumber = int.Parse(personalNumber.Substring(8, 3));
                controlNumber = int.Parse(personalNumber.Substring(11, 1));
            }
            //YYMMDD-nnnc || YYMMDD+nnnc
            else if (personalNumber.Length == 10)
            {
                year = int.Parse(personalNumber.Substring(0, 2));
                month = int.Parse(personalNumber.Substring(2, 2));
                day = int.Parse(personalNumber.Substring(4, 2));
                birthNumber = int.Parse(personalNumber.Substring(6, 3));
                controlNumber = int.Parse(personalNumber.Substring(9, 1));

                // + sign is for those that are 100years and over. (this year is 2020)
                if (plusOrMinus == "+")
                {
                    if ( year <= 20 )
                    {
                        year += 1900;
                    }
                    else
                    {
                        year += 1800;
                    }
                    
                }
                else if (plusOrMinus == "-")
                {
                    if (year <= 20)
                    {
                        year += 2000;
                    }
                    else
                    {
                        year += 1900;
                    }
                }
            }
            else//default assign
            {
                year = 0;
                month = 0;
                day = 0;
                birthNumber = 0;
                controlNumber = 0;
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

        static bool ControlNumberCheck(string personalNumber, int controlNumber)
        {
            //int array pNums with all digits except controlNumber(the last number)
            int[] pNums = new int[9];
            
            //fill int array from personal number (adjust for 10 or 12 digits)
            for (int i = 0; i < 9; i++)
            {
                if (personalNumber.Length == 12)
                {
                    pNums[i] = int.Parse(personalNumber.Substring(i + 2, 1));
                }
                else
                {
                    pNums[i] = int.Parse(personalNumber.Substring(i, 1));
                }
            }

            
            int[] alternatingTwos = new int[9] { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            //sum of the products from luhn-algorithm
            int luhnSum = 0;
            //temporary integer which is added to luhnSum.
            int temp;

            //loop to double every other number  
            //(in reality it should start with 2 from the right, but since this is has an uneven length (9) both start and last value is doubled
            for (int i = 0; i < 9; i++)
            {
                temp = pNums[i] * alternatingTwos[i];

                // check if temp is bigger than 10. 
                if ( temp >= 10)
                {
                    //e.g. 6*2 = 12, sum of 1 and 2 = 3. 12-9 = 3
                    temp -= 9;
                }

                luhnSum += temp;
            }

            //given from excersice/wikpiedia
            int luhnCalc = (10 - (luhnSum % 10)) % 10;

            Console.WriteLine("Den inmatade kontrollsiffran var = {0}", controlNumber);
            Console.WriteLine("Enligt luhn-algoritmen bör kontrollsifrran vara {0}", luhnCalc);

            if (luhnCalc == controlNumber)
                return true;
            else
                return false;

        }
    }
}
