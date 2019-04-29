using System;
using System.Linq;

/// <summary>
/// This class is a C# console application that is a simple version of 
/// Mastermind.  The randomly generated answer should be four (4) digits in 
/// length, with each digit between the numbers 1 and 6.  After the player 
/// enters a combination, a minus (-) sign should be printed for every digit 
/// that is correct but in the wrong position, and a plus (+) sign should be 
/// printed for every digit that is both correct and in the correct position.  
/// Nothing should be printed for incorrect digits.  The player has ten (10) 
/// attempts to guess the number correctly before receiving a message that they
/// have lost.
/// </summary>
class MainClass {

    const int MIN_NUMBER = 1;
    const int MAX_NUMBER = 6;
    const int NUMBER_OF_DIGITS_IN_NUMBER = 4;
    const int NUMBER_OF_GUESS_ATTEMPTS = 10;

    public static void Main(string[] args)
    {
        bool playAgain = true;

        while (playAgain)
        {
            // DEFINE and Initialize variables...
            bool playGameNow = true;
            Random rnd = new Random();
            int[] target = new int[NUMBER_OF_DIGITS_IN_NUMBER];
            string[] guessedResults = new string[NUMBER_OF_DIGITS_IN_NUMBER];


            // GENERATES the Number to Guess...
            while (target.Distinct().Count() != NUMBER_OF_DIGITS_IN_NUMBER)
            {
                for (int x = 0; x < NUMBER_OF_DIGITS_IN_NUMBER; x++)
                {
                    target[x] = rnd.Next(MIN_NUMBER, MAX_NUMBER+1);
                    guessedResults[x] = " ";
                }
            }

            // CONCATENATES the individual numeric digits into a single target number string for display purposes...
            string targetNumber = string.Join("", target.Select(i => i.ToString()).ToArray());

            try
            {
                Console.WriteLine("Welcome to 'Mastermind'. You win you must");
                Console.WriteLine("enter a {0}-digit number, where the numbers are between {1} and {2}:", NUMBER_OF_DIGITS_IN_NUMBER, MIN_NUMBER, MAX_NUMBER);

                int attempts = 0;
                while (playGameNow)
                {
                    string userInput = Console.ReadLine();
                    attempts++;

                    Console.WriteLine("Results of Attempt #: {0} of {1}", attempts, NUMBER_OF_GUESS_ATTEMPTS);

                    int[] userNumber = userInput.Select(v => v - '0').ToArray();
                    int countCorrect = 0;
                    int positionCorrect = 0;

                    for (int c = 0; c < NUMBER_OF_DIGITS_IN_NUMBER; c++)
                    {
                        // VERIFY Number Entered by User Exists in Target Number...
                        if (target.Contains(userNumber[c]))
                        {
                            countCorrect++;

                            if (target[c] == userNumber[c])
                            {
                                positionCorrect++;
                                guessedResults[c] = "+";    // Correct Number, Correct Position
                            }
                            else
                            {
                                guessedResults[c] = "-";    // Correct Number, Wrong Position
                            }
                        }
                        else
                        {
                            guessedResults[c] = " ";        // Wrong Number, Wrong Position
                        }

                        // CHECK to see if all numbers guessed are in the correct position....
                        if (positionCorrect == NUMBER_OF_DIGITS_IN_NUMBER)
                        {
                            Console.WriteLine("{0}{1}{2}{3}", guessedResults[0], guessedResults[1], guessedResults[2], guessedResults[3]);
                            Console.WriteLine(" ");
                            Console.WriteLine(" * * * YOU WON!!! * * *  ");
                            Console.WriteLine(" ");
                            playGameNow = false;
                        }
                    }

                    Console.WriteLine("{0}{1}{2}{3}", guessedResults[0] , guessedResults[1] , guessedResults[2] , guessedResults[3]);

                    // DETERMINE If Game is over because player failed to guess number in the alloted attempts...
                    if ((attempts >= NUMBER_OF_GUESS_ATTEMPTS) && (playGameNow == true))
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("You LOST!!");
                        Console.WriteLine(" ");
                        Console.ReadKey();
                        playGameNow = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error occurred: {0}. Please make sure you entered a valid number.", ex.Message);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Play Again (Y/N)?");
            string userPlayAgainInput = Console.ReadLine();
            if (userPlayAgainInput.ToUpper() != "Y")
            {
                playAgain = false; 
            }

        }
    }


}
