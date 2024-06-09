using System;

namespace task2_memory_game
{
    internal class UIOfMemoryGame
    {
        private const string k_ChooseYes = "y";
        private const string k_ChooseNo = "n";
        private const string k_QuitGame = "Q";

        public static string GetUsername()
        {
            string name;
            Console.WriteLine("Enter username:");
            name = Console.ReadLine();
            return name;
        }

        public static bool AgainstHumanOrComputer()
        {
            bool i_flag;

            System.Console.WriteLine($"do you want to play against a computer: ({k_ChooseYes}/{k_ChooseNo})");
            i_flag = isAnswerYes();
            return i_flag;
        }

        private static bool isAnswerYes()
        {
            bool wasYesChosen;
            string i_answer;
            i_answer = Console.ReadLine();
            if (i_answer == k_ChooseYes)
            {
                wasYesChosen = true;
            }
            else
            {
                wasYesChosen = false;
            }
            return wasYesChosen;
        }

        public static (int,int) GetBoardSizeFromUser((int,int) i_RowSizeMinAndMax, (int,int) i_ColSizeMinAndMax)
        {
            (int,int) rowAndColumnSize;
            int row;
            int column;

            Console.WriteLine("Enter height of board:");
            row = getInputFromRange(i_RowSizeMinAndMax.Item1, i_RowSizeMinAndMax.Item2);

            Console.WriteLine("Enter width of board:");
            column = getInputFromRange(i_ColSizeMinAndMax.Item1, i_ColSizeMinAndMax.Item2);

            rowAndColumnSize = (row, column);
            return rowAndColumnSize;
        }

        private static int getInputFromRange(int i_Minimum, int i_Maximum)
        {
            int inputInt;
            string inputStr = Console.ReadLine();

            while (!isInputFromRangeLegal(inputStr, i_Minimum, i_Maximum, out inputInt))
            {
                Console.WriteLine("Invalid input! Please try again.");
                inputStr = Console.ReadLine();
            }
            return inputInt;
        }

        private static bool isInputFromRangeLegal(string i_InputStr, int i_Minimum, int i_Maximum, out int o_InputInt)
        {
            bool returnValue = false;

            if (int.TryParse(i_InputStr, out o_InputInt))
            {
                if (o_InputInt >= i_Minimum && o_InputInt <= i_Maximum)
                {
                    if (o_InputInt % 2 == 0)
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }

        public static string AskUserForCardToOpen(out bool o_ContinueGame)
        {
            string cardToOpen;
            Console.WriteLine("Pick a card to open: ");
            cardToOpen = Console.ReadLine();

            if (cardToOpen == k_QuitGame)
            {
                o_ContinueGame = false;
            }
            else
            {
                o_ContinueGame = true;
            }

            return cardToOpen;
        }

        public static void printIllegalPlaceForCardMessage()
        {
            Console.WriteLine("Illegal place for card");
        }

        public static void PrintCardNotInBorderWarning() 
        {
            Console.WriteLine("Card not in borders");
        }

        public static bool EndGameMessageAndAskForAnotherGame(PlayerMemoryGame i_Player1, PlayerMemoryGame i_Player2)
        {
            bool shouldStartNewGame;
            Console.WriteLine("THE GAME HAS ENDED!!!");
            printWinnerMessage(i_Player1, i_Player2);

            printPlayerScore(i_Player1);
            printPlayerScore(i_Player2);

            Console.WriteLine("DO YOU WANT TO START A NEW GAME: (y/n)");
            
            shouldStartNewGame = isAnswerYes();
            return shouldStartNewGame;
        }

        private static void printWinnerMessage(PlayerMemoryGame i_Player1, PlayerMemoryGame i_Player2)
        {
            if (i_Player1.Score == i_Player2.Score)
            {
                Console.WriteLine("THE GAME ENDED IN A TIE!");
            }
            else
            {
                PlayerMemoryGame winner;
                if (i_Player1.Score > i_Player2.Score)
                {
                    winner = i_Player1;
                }
                else //if (i_player2.Score > i_player1.Score)
                {
                    winner = i_Player2;
                }
                Console.WriteLine($"{winner.Name} WON THE GAME!");
            }
        }
        
        private static void printPlayerScore(PlayerMemoryGame i_Player)
        {
            Console.WriteLine($"{i_Player.Name} GOT {i_Player.Score} POINTS!");
        }
    }
}
