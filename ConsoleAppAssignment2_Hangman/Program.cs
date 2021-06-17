using System;
using System.IO;
using System.Text;

namespace ConsoleAppAssignment2_Hangman
{
    public class Program
    {
        public static string alertText;

        public static void Main(string[] args)
        {
            bool restartGame;

            string[] lines = null;
            string[] listOfGuessWords = null;

            //string[] listOfGuessWords = new string[5] { "money", "car", "cat", "dog", "banana" };  old code before readin from file

            // --- Start Read in words from file
            try
            {
                //using (StreamReader inputFile = new StreamReader(Path.Combine(filPathRead, "listofguesswords.txt")))
                //string path = "C:/Users/overl/Desktop/people.csv";

                lines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\listofguesswords.txt");

            }
            catch (IOException e)

            {
                Console.WriteLine($"An error of reading the file 'listofguesswords.txt'. Error is:\n{e.Message}");
                Console.ReadKey();
            }

            foreach (string line in lines)
            {
                listOfGuessWords = line.Split(',');
            }
            // --- End read in words from flie

            do
            {

                GameTitleClear();

                // picking a random word from listOfGuessWords
                Random rndWordPick = new Random();
                int idx = rndWordPick.Next(0, listOfGuessWords.Length);
                string wordToGuess = listOfGuessWords[idx];

                // Declare variables
                int nrGuessesLeft = 10;
                string userInput = null;
                string guessedWordsString = null;
                string wordReveale = null;


                //bool rightInput = true;
                bool userGuessedWrong = true;

                // Declare arrays
                string[] whatUserGuessed = new string[15];
                char[] displayWordToGuess = new char[wordToGuess.Length];
                char[] charArray = wordToGuess.ToCharArray();
                char[] correctLetters = new char[wordToGuess.Length];
                StringBuilder incorrectLetters = new StringBuilder();

                // make the displayword have same amount of _ as there are letters in the word
                for (int nrWord = 0; nrWord < wordToGuess.Length; nrWord++) 
                {
                    displayWordToGuess[nrWord] = '_';
                }


                // Start of guesses loop
                do
                {
                    wordReveale = new string(displayWordToGuess);
                    DisplayUpdate(nrGuessesLeft, wordReveale, guessedWordsString);

                    userInput = Console.ReadLine();
                    bool checkIFOnlyLetters = IsAlphabets(userInput);

                    if (userInput.Length < 1) // input value check if-loops
                    {
                        GameTitleClear();

                        alertText = "You must type in something, can not be blank.\n";
                        AlertMessage(alertText, ConsoleColor.DarkRed);
                    }
                    else if (checkIFOnlyLetters == false)
                    {
                        GameTitleClear();

                        alertText = "Your did not type in letter(s), please only type in letter(s): \n";
                        AlertMessage(alertText, ConsoleColor.DarkRed);
                    }
                    else if (userInput.Length == 1)
                    {
                        GameTitleClear();

                        bool allreadyGuessedBool = AllreadyGuesses(userInput, displayWordToGuess, incorrectLetters);


                        if (allreadyGuessedBool == true)
                        {
                            alertText = $"You have allready guessed letter: {userInput}.\nTry again (this guess does not count).";
                            AlertMessage(alertText, ConsoleColor.DarkGreen);
                        }

                        char playerGuessLetter = char.Parse(userInput);

                        // is the guessed letter in the word? And if yes, then input that in correct position in displayWordToGuess
                        for (int j = 0; j < wordToGuess.Length; j++)
                        {
                            if (playerGuessLetter == wordToGuess[j])
                            {
                                displayWordToGuess[j] = playerGuessLetter;
                                userGuessedWrong = false;
                            }
                            else
                            {
                                userGuessedWrong = true;
                            }
                        }

                        // tell user if guessed letter is correct or not
                        if (userGuessedWrong == true && allreadyGuessedBool == false)
                        {
                            alertText = $"Sorry! {userInput} does not exist in this word.\n";
                            AlertMessage(alertText, ConsoleColor.DarkGreen);
                            incorrectLetters.Append(userInput + ", ");
                            nrGuessesLeft--;
                        }
                        else if (userGuessedWrong == false && allreadyGuessedBool == false)
                        {
                            alertText = $"Great! {userInput} IS in this word.\n";
                            AlertMessage(alertText, ConsoleColor.DarkGreen);
                            correctLetters = userInput.ToCharArray();
                            nrGuessesLeft--;
                        }
                        
                        wordReveale = new string(displayWordToGuess); // wordReveale is used to check if user gotten the whole word
                        string strCorrectLetters = new string(correctLetters); // char array to string for output
                        guessedWordsString = incorrectLetters.ToString() + strCorrectLetters; // StringBuilder to string for output

                        if (wordReveale == wordToGuess)
                        {
                            YourAWinner(wordToGuess, nrGuessesLeft);
                            break;
                        }


                    }
                    else if (userInput.Length > 1)
                    {
                        GameTitleClear();

                        if (userInput == wordToGuess)
                        {
                            YourAWinner(wordToGuess, nrGuessesLeft);
                            break;
                        }
                        else
                        {
                            alertText = $"Sorry! {userInput} is the wrong word!\n";
                            AlertMessage(alertText, ConsoleColor.DarkGreen);
                        }

                        nrGuessesLeft--;

                    } // End of input value check if-loop


                } while (nrGuessesLeft > 0);  // End do-loop

                if (nrGuessesLeft == 0)
                {
                    GameTitleClear();
                    alertText = $"\nYou lost. Sorry.\n";
                    AlertMessage(alertText, ConsoleColor.Yellow);
                }

                alertText = $"Do you want to play again? y/n (n will exit program): ";
                AlertMessage(alertText, ConsoleColor.Yellow);

                string playAgain = Console.ReadLine();
                restartGame = WantToPlayAgain(playAgain);

            } while (restartGame == true); // end of do loop.

  

        } // ---------- End Main  -------------------


        static void GameTitleClear()
        {
            Console.Clear();
            Console.WriteLine("--- Hangman Console Game ---\n   --- Eric R Edition ---\n\n");
        }

        public static bool WantToPlayAgain(string playAgain)
        {
            bool yesOrNo = false;

            do
            {

                if (playAgain == "y")
                {
                    yesOrNo = true;
                    return true;
                }
                else if (playAgain == "n")
                {
                    yesOrNo = true;
                    return false;
                }
                else
                {
                    alertText = "You did not type y or n!";
                    AlertMessage(alertText, ConsoleColor.DarkRed);
                    yesOrNo = false;
                    return true;
                }

            } while (yesOrNo == false);

        }

        
        public static void AlertMessage(string alertText, ConsoleColor alertMessColor)
        {
            Console.ForegroundColor = alertMessColor;
            Console.WriteLine(alertText);
            Console.ResetColor();
        }


        public static void YourAWinner(string wordToGuess, int nrGuessesLeft)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            GameTitleClear();
            //Console.WriteLine($"!!!! Congratulations! {wordToGuess} is the correct word! You won! !!!!\n");
            //Console.WriteLine($"You had {nrGuessesLeft} guesses left.\n\nWell Done!\n\n");
            alertText = $"!!!! Congratulations! { wordToGuess} is the correct word! You won!!!!!\nYou had {nrGuessesLeft} guesses left.\n\nWell Done!\n\n";
            AlertMessage(alertText, ConsoleColor.Yellow);
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



        static void DisplayUpdate(int nrGuessesLeft, string wordReveale, string guessedWordsString)
        {
            Console.WriteLine($"The word to guess: {wordReveale}\n");
            Console.WriteLine($"You have {nrGuessesLeft} guesses left.\n");
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
