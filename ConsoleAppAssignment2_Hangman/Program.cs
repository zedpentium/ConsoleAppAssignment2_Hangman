using System;
//using System.Linq;
using System.IO;
using System.Text;

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


            }
            catch (IOException e)

            {
                Console.WriteLine($"An error of reading the file 'listofguesswords.txt'. Error is:\n{e.Message}");
                //Console.WriteLine(e.Message);
                Console.ReadKey();
            }


            foreach (string line in lines)
            {
                listOfGuessWords = line.Split(',');
            }

            //string[] listOfGuessWords = new string[5] { "money", "car", "cat", "dog", "banana" };

            // picking a random word from listOfGuessWords
            Random rndWordPick = new Random();
            int idx = rndWordPick.Next(0, listOfGuessWords.Length);
            string wordToGuess = listOfGuessWords[idx];

            // Declare variables
            int nrGuessesLeft = 10, nrOfguess = 0;

            string userInput = null;
            string guessedWordsString = null;
            string guessedCorrectLetter = null;
            string alertMessage = "";

            bool rightInput = true;
            bool userGuessedWrong = true;

            // Declare arrays
            string[] whatUserGuessed = new string[15];
            char[] displayWordToGuess = new char[wordToGuess.Length];
            char[] charArray = wordToGuess.ToCharArray();
            char[] correctLetters = new char[wordToGuess.Length];
            //correctLetters[0] = '.' ;
            StringBuilder incorrectLetters = new StringBuilder();

            //--------------------------------------------------


            // Start of guesses loop
            for (int nrWord = 0; nrWord < wordToGuess.Length; nrWord++)
            {
                displayWordToGuess[nrWord] = '_';
            }

            for (nrOfguess = 10; nrOfguess > 0; nrOfguess--)
            {
                errorinputretry:
                do
                {
                    string wordReveale = new string(displayWordToGuess);
                    DisplayUpdate(nrOfguess, wordReveale, guessedWordsString);

                //GuessesValidator();


                // Below are a longer Try-loop ------------------------------------

                    try
                    {

                    

                        userInput = Console.ReadLine();
                        bool checkIFOnlyLetters = IsAlphabets(userInput);

                        rightInput = false;

                        if (userInput.Length < 1) // input value check if-loops
                        {
                            Console.Clear();
                            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");

                            alertMessage = "You must type in something, can not be blank.\n";
                            AlertMessageRed(alertMessage);
                            //Console.WriteLine("\nYou must type in something, can not be blank.");
                            goto errorinputretry;
                        }


                        else if (checkIFOnlyLetters == false)
                        {
                            Console.Clear();
                            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n"); 
                            
                            alertMessage = "Your did not type in letter(s), please only type in letter(s): \n";
                            AlertMessageRed(alertMessage);
                            //Console.WriteLine("Your did not type in letter(s), please only type in letter(s): ");
                            goto errorinputretry;
                        }

                        else if (userInput.Length == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");

                            bool allreadyGuessedBool = false;
                            allreadyGuessedBool = AllreadyGuesses(userInput, displayWordToGuess, incorrectLetters);


                            if (allreadyGuessedBool == true)
                            {
                                alertMessage = $"You have allready guessed letter: {userInput}.\nTry again (this guess does not count).";
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
                                //guessedWordsString = guessedWordsString + userInput + " ,";
                                incorrectLetters.Append(userInput + ", ");
                            }
                            else if (userGuessedWrong == false)
                            {
                                alertMessage = $"Great! {userInput} IS in this word.\n";
                                AlertMessageGreen(alertMessage);
                                //Console.WriteLine($"Great! {userInput} IS in this word.\n");
                                //guessedWordsString = guessedWordsString + userInput + " ,";
                                correctLetters = userInput.ToCharArray();
                                //guessedCorrectLetter = correctLetters.ToString();
                            }

                            wordReveale = new string(displayWordToGuess);

                            //foreach (char item in correctLetters)
                            //{
                            //    guessedWordsString = correctLetters.ToString();
                            //}

                            //string sJoin = string.Join(", ", correctLetters);
                            string strCorrectLetters = new string(correctLetters);
                            guessedWordsString = incorrectLetters.ToString() + strCorrectLetters;

                            //Console.WriteLine("What does guessedWordsString contain: " + guessedWordsString);

                            //Console.WriteLine("What does INcorrect contain: " + incorrectLetters.ToString());

                            ////string charToString = new string(correctLetters);

                            //Console.WriteLine("What does displayWordToGuess contain: " + sJoin);

                            //Console.ReadKey();

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
                            Console.Clear();
                            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");

                            if (userInput == wordToGuess)
                            {
                                YourAWinner(wordToGuess, nrOfguess);
                                goto correctanswerend;
                            }
                            else
                            {
                                alertMessage = $"Sorry! {userInput} is the wrong word!\n";
                                AlertMessageGreen(alertMessage);
                                //Console.WriteLine($"Sorry! {userInput} is the wrong word!");
                                //incorrectLetters.Append(userInput + ", ");
                            }

                            // guessedWordsString = guessedWordsString + userInput + " ,";
                            //string sJoin = string.Join(", ", correctLetters);
                            //guessedWordsString = incorrectLetters.ToString() + sJoin;

                            nrGuessesLeft--;
                        } // End of input value check if-loop




                    } // End try

                    // catch of above try
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
                    // End of whole Try-loop



                } while (rightInput); // End of do-while loop



            } // End for-loop

            // ----------------------------


            alertMessage = $"\n\n\n\nYou lost. Sorry.";
            AlertMessageYellow(alertMessage);
            //Console.WriteLine($"\n\n\n\nYou lost. Sorry.");

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
            Console.WriteLine($"This is what uou have allready guessed: {guessedWordsString}\n");
            Console.Write("Guess a letter or the word (write one letter, or whole word: ");
        }


        static bool AllreadyGuesses(string userInput, char[] displayWordToGuess, StringBuilder incorrectLetters)
        {
            string allGCorL = new string(displayWordToGuess);
            string allGIncL = incorrectLetters.ToString();
            bool allreadyGuessedBool;

            if (allGCorL.Contains(userInput))
            {
                allreadyGuessedBool = true;
            }
            else if (allGIncL.Contains(userInput))
            {
                allreadyGuessedBool = true;
            }
            else
            {
                allreadyGuessedBool = false;
            }

            return allreadyGuessedBool;

        }

    } // End program
}
