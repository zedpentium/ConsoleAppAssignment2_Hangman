using System;
//using System.Linq;
//using System.IO;

namespace ConsoleAppAssignment2_Hangman
{
    class Program
    {

        
        public static void Main(string[] args)
        {
        startgameagain:


            /*string textlasFil = "Tomt";
            // Sätt variable med Filsökväg.
            string filPathRead = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Läs in filen OM den existerar, else meddelande att filen ej kan läsas/hittas under "Den här datorn/Dokument" på windows.
            try
            {
                using (StreamReader inputFile = new StreamReader(Path.Combine(filPathRead, "ER_T&B_ConApp_textfil.txt")))
                    textlasFil = inputFile.ReadToEnd();

                Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine("\nLäsning av fil lyckad!");

                Console.ResetColor();

                if (ValdFarg == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine();
                Console.WriteLine("Detta är texten från filen ER_T & B_ConApp_textfil.txt under " + filPathRead + ":");
                Console.WriteLine(textlasFil);
            }
            catch (IOException e)

            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine("Ett fel uppstod. Felet är följande: ");
                Console.WriteLine(e.Message);

                Console.ResetColor();

                if (ValdFarg == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }*/


            string[] listOfGuessWords = new string[5] { "money", "car", "cat", "dog", "banana" };

            Random rndWordPick = new Random();
            int idx = rndWordPick.Next(0, listOfGuessWords.Length);
            string wordToGuess = listOfGuessWords[idx];

            // Declare Variables
            string userInput;
            //string checkedUserInput;
            int nrGuessesLeft = 10;
            //bool guessCorrect = false;
            int nrOfguess = 0;
            //string wordToGuess = "hangman";
            string[] whatUserGuessed = new string[15];

            string guessedWordsString = "-> ";
            //whatUserGuessed[0] = "";

            //int stringBuildNr = 0;

            char[] displayWordToGuess = new char[wordToGuess.Length];

            char[] charArray = wordToGuess.ToCharArray();
            //char[] copyOfcharArray = charArray;

            for (int nrWord = 0; nrWord < wordToGuess.Length; nrWord++)
            {
                displayWordToGuess[nrWord] = '_';
            }



            //string displayWordToGuess = "_";


            //char[] displayWordToGuess = "******".ToCharArray();
            //char[] wordToGuess = "miller".ToCharArray();
            //char[] copy = wordToGuess;
            //int index = 0;

            //DisplayUpdate(nrOfguess);

            //userInput = Console.ReadLine();
            //checkedUserInput = UserInputValidation(userInput)

            Console.Clear();
            //Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");
            //Console.Write("\nPlease enter your guess: ");

            //string[] arraytheWordToGuess = new string[10];


            bool rightInput = true;

            //int nrOfpasses = 0;

            for (nrOfguess = 10; nrOfguess > 0; nrOfguess--)
            {


                do
                {

                    string wordReveale = new string(displayWordToGuess);
                    DisplayUpdate(nrOfguess, wordReveale, guessedWordsString);

                    try
                    {

                        errorinputretry:

                        userInput = Console.ReadLine();
                        bool checkIFOnlyLetters = IsAlphabets(userInput);


                            rightInput = false;

                        if (checkIFOnlyLetters == false)
                        {
                            Console.WriteLine($"Your did not type in letter(s), please only type in letter(s): ");
                            goto errorinputretry;
                        }

                        else if (userInput.Length < 1)
                        {
                            Console.WriteLine("\nYou must type in something, can not be blank.");
                            goto errorinputretry;
                        }

                        else if (userInput.Length == 1)
                        {

                            if (guessedWordsString.Contains(userInput))
                            {
                                Console.WriteLine($"\nYou have allready guessed letter: {userInput}.\nTry again (this guess does not count).");
                                goto errorinputretry;
                            }
                            Console.WriteLine($"\nYou guessed letter: {userInput}");

                            char playerGuessLetter = char.Parse(userInput);


                            for (int j = 0; j < wordToGuess.Length; j++)
                            {
                                if (playerGuessLetter == wordToGuess[j])
                                    displayWordToGuess[j] = playerGuessLetter;
                            }

                            wordReveale = new string(displayWordToGuess);

                            if (wordReveale == wordToGuess)
                            {
                                YourAWinner(wordToGuess, nrOfguess);
                                goto correctanswerend;
                            }
                            guessedWordsString = guessedWordsString + userInput + " ,";
                            nrGuessesLeft--;
                        }

                        else if (userInput.Length > 1)
                        {

                            
                            Console.WriteLine($"\nYou guessed the word: {userInput}");


                            if (userInput == wordToGuess)
                            {
                                YourAWinner(wordToGuess, nrOfguess);
                                goto correctanswerend;
                            }
                            else
                            {
                                Console.WriteLine($"Sorry! {userInput} is the wrong word!");
                            }

                            guessedWordsString = guessedWordsString + userInput + " ,";
                            nrGuessesLeft--;
                        }

                        Console.Clear();
                   
                    } // End Try
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong format: ");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("you must enter something:");
                    }





                } while (rightInput); // End of do loop



            } // End for-loop

            Console.WriteLine($"\n\n\n\nYou lost.");

        correctanswerend:
            Console.WriteLine($"Do you want to play again? y/n (n will exit program): ");
            string playAgain = Console.ReadLine();
            if (playAgain == "y") 
                { 
                    goto startgameagain;
                }
            else if (playAgain == "n")
            { }
            else
            {
                Console.WriteLine("! You did not type y or n!");
                goto correctanswerend;
            }

        } // End Main



        public static void YourAWinner(string wordToGuess, int nrOfguess)
        {
            Console.Clear();
            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");
            Console.WriteLine($"!!!! Congratulations! {wordToGuess} is the correct word! You won! !!!!\n");
            Console.WriteLine($"You had {nrOfguess} guesses left.\n\nWell Done!\n\n");

        }

        public static bool IsAlphabets(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }

            for (int i = 0; i < userInput.Length; i++)
            {
                if (!char.IsLetter(userInput[i]))
                    return false;
            }
            return true;
        }



        static void DisplayUpdate(int nrGuess, string wordReveale, string guessedWordsString)
        {
            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");
            Console.WriteLine($"The word to guess: {wordReveale}\n");
            Console.WriteLine($"You have {nrGuess} guesses left.\n");
            Console.WriteLine($"You have allready guessed{guessedWordsString}\n");
            Console.WriteLine("Guess a letter or the word (write one letter, or whole word:");
        }



    } // End program
}
