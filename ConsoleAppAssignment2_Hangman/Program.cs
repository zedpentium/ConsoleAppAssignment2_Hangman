using System;
//using System.Linq;
using System.IO;

namespace ConsoleAppAssignment2_Hangman
{
    class Program
    {


        public static void Main(string[] args)
        {
        startgameagain:

            Console.Clear();
            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");

            string[] lines = null;
            string[] listOfGuessWords = null;

            try
            {
                //using (StreamReader inputFile = new StreamReader(Path.Combine(filPathRead, "listofguesswords.txt")))
                //string path = "C:/Users/overl/Desktop/people.csv";

                lines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\listofguesswords.txt");

                //Console.WriteLine("\nLäsning av fil lyckad!");

            }
            catch (IOException e)

            {
                Console.WriteLine($"An error of reading the file 'listofguesswords.txt'.\nError is:  {e.Message}");
                //Console.WriteLine(e.Message);
                Console.ReadKey();
            }


            foreach (string line in lines)
            {
                listOfGuessWords = line.Split(',');
            }

            //string[] listOfGuessWords = new string[5] { "money", "car", "cat", "dog", "banana" };

            Random rndWordPick = new Random();
            int idx = rndWordPick.Next(0, listOfGuessWords.Length);
            string wordToGuess = listOfGuessWords[idx];

            // Declare Variables & arrays
            string userInput;
            int nrGuessesLeft = 10;
            int nrOfguess = 0;
            string[] whatUserGuessed = new string[15];

            bool userGuessedWrong = true;

            string guessedWordsString = "-> ";

            string alertMessage = "";

            char[] displayWordToGuess = new char[wordToGuess.Length];

            char[] charArray = wordToGuess.ToCharArray();

            for (int nrWord = 0; nrWord < wordToGuess.Length; nrWord++)
            {
                displayWordToGuess[nrWord] = '_';
            }

            bool rightInput = true;

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

                            alertMessage = "Your did not type in letter(s), please only type in letter(s): ";
                            AlertMessageRed(alertMessage);
                            goto errorinputretry;
                        }

                        else if (userInput.Length < 1)
                        {
                            alertMessage = "\nYou must type in something, can not be blank.";
                            AlertMessageRed(alertMessage);
                            //Console.WriteLine("\nYou must type in something, can not be blank.");
                            goto errorinputretry;
                        }

                        else if (userInput.Length == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");

                            if (guessedWordsString.Contains(userInput))
                            {
                                alertMessage = $"\nYou have allready guessed letter: {userInput}.\nTry again (this guess does not count).";
                                AlertMessageGreen(alertMessage);
                                //Console.WriteLine($"\nYou have allready guessed letter: {userInput}.\nTry again (this guess does not count).");
                                goto errorinputretry;
                            }
                            //Console.WriteLine($"\nYou guessed letter: {userInput}");

                            char playerGuessLetter = char.Parse(userInput);

                            userGuessedWrong = true;

                            for (int j = 0; j < wordToGuess.Length; j++)
                            {

                                if (playerGuessLetter == wordToGuess[j])
                                {
                                    displayWordToGuess[j] = playerGuessLetter;
                                    userGuessedWrong = false;
                                }


                            }

                            if (userGuessedWrong == true)
                            {
                                alertMessage = $"Sorry! {userInput} does not exist in this word.\n";
                                AlertMessageGreen(alertMessage);
                                //Console.WriteLine($"Sorry! {userInput} does not exist in this word.\n");
                                guessedWordsString = guessedWordsString + userInput + " ,";
                            }
                            else if (userGuessedWrong == false)
                            {
                                alertMessage = $"Great! {userInput} IS in this word.\n";
                                AlertMessageGreen(alertMessage);
                                //Console.WriteLine($"Great! {userInput} IS in this word.\n");
                                guessedWordsString = guessedWordsString + userInput + " ,";
                            }

                            wordReveale = new string(displayWordToGuess);

                            if (wordReveale == wordToGuess)
                            {
                                YourAWinner(wordToGuess, nrOfguess);
                                goto correctanswerend;
                            }

                            nrGuessesLeft--;
                            //Console.WriteLine("\n\n");
                        }

                        else if (userInput.Length > 1)
                        {


                            //Console.WriteLine($"\nYou guessed the word: {userInput}");


                            if (userInput == wordToGuess)
                            {
                                YourAWinner(wordToGuess, nrOfguess);
                                goto correctanswerend;
                            }
                            else
                            {
                                alertMessage = $"Sorry! {userInput} is the wrong word!";
                                AlertMessageGreen(alertMessage);
                                //Console.WriteLine($"Sorry! {userInput} is the wrong word!");
                            }

                            guessedWordsString = guessedWordsString + userInput + " ,";
                            nrGuessesLeft--;
                        }

                        //Console.Clear();

                    } // End Try
                    catch (FormatException)
                    {
                        alertMessage = "Wrong format of input.";
                        AlertMessageRed(alertMessage);
                        //Console.WriteLine("Wrong format: ");
                    }
                    catch (ArgumentNullException)
                    {
                        //Console.WriteLine("you must enter something:");
                    }





                } while (rightInput); // End of do loop



            } // End for-loop

            alertMessage = $"\n\n\n\nYou lost. Sorry.";
            AlertMessageYellow(alertMessage);
        //onsole.WriteLine($"\n\n\n\nYou lost. Sorry.");

        correctanswerend:

            alertMessage = $"Do you want to play again? y/n (n will exit program): ";
            AlertMessageYellow(alertMessage);
            //Console.WriteLine($"Do you want to play again? y/n (n will exit program): ");

            string playAgain = Console.ReadLine();
            if (playAgain == "y")
            {
                goto startgameagain;
            }
            else if (playAgain == "n")
            { }
            else
            {
                alertMessage = "You did not type y or n!";
                AlertMessageRed(alertMessage); 
                //Console.WriteLine("You did not type y or n!");
                goto correctanswerend;
            }

        } // End Main


        public static void AlertMessageRed(string alertMessage)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(alertMessage);
            Console.ResetColor();
        }

        public static void AlertMessageGreen(string alertMessage)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(alertMessage);
            Console.ResetColor();
        }

        public static void AlertMessageYellow(string alertMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(alertMessage);
            Console.ResetColor();
        }


        public static void YourAWinner(string wordToGuess, int nrOfguess)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");
            Console.WriteLine($"!!!! Congratulations! {wordToGuess} is the correct word! You won! !!!!\n");
            Console.WriteLine($"You had {nrOfguess} guesses left.\n\nWell Done!\n\n");
            Console.ResetColor();

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
            Console.WriteLine($"The word to guess: {wordReveale}\n");
            Console.WriteLine($"You have {nrGuess} guesses left.\n");
            Console.WriteLine($"You have allready guessed{guessedWordsString}\n");
            Console.Write("Guess a letter or the word (write one letter, or whole word: ");
        }



    } // End program
}
