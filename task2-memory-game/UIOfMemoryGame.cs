using System;

namespace task2_memory_game
{
    internal class UIOfMemoryGame
    {
        private const string k_ChooseYes = "y";
        private const string k_ChooseNo = "n";

        public string GetUsername()
        {
            string name;
            Console.WriteLine("Enter username:");
            name = Console.ReadLine();
            return name;
        }

        public bool AgainstHumanOrComputer()
        {
            bool i_flag;

            System.Console.WriteLine($"do you want to play against a computer: ({k_ChooseYes}/{k_ChooseNo})");
            i_flag = isAnswerYes();
            return i_flag;
        }

        private bool isAnswerYes()
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

        public (int,int) GetBoardSizeFromUser((int,int) i_RowSizeMinAndMax, (int,int) i_ColSizeMinAndMax)
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

        private int getInputFromRange(int i_minimum, int i_maximum)
        {
            int inputInt;
            string inputStr = Console.ReadLine();

            while (!isInputFromRangeLegal(inputStr, i_minimum, i_maximum, out inputInt))
            {
                Console.WriteLine("Invalid input! Please try again.");
                inputStr = Console.ReadLine();
            }
            return inputInt;
        }

        private bool isInputFromRangeLegal(string inputStr, int i_minimum, int i_maximum, out int inputInt)
        {
            bool returnValue = false;

            if (int.TryParse(inputStr, out inputInt))
            {
                if (inputInt >= i_minimum && inputInt <= i_maximum)
                {
                    if (inputInt % 2 == 0)
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }

        public string AskUserForCardToOpen()
        {
            string cardToOpen;
            Console.WriteLine("Pick a card to open: ");
            cardToOpen = Console.ReadLine();
            return cardToOpen;
        }

        public void printIllegalPlaceForCard()
        {
            Console.WriteLine("Illegal place for card");
        }

        public void printIllegalPlaceForCardBorder() 
        {
            Console.WriteLine("Card not in borders");
        }

        public bool EndGameMessageAndAskForAnotherGame(PlayerMemoryGame i_player1, PlayerMemoryGame i_player2)
        {
            bool i_flag;
            Console.WriteLine("THE GAME HAS ENDED");
            printWinnerMessage(i_player1, i_player2);

            printPlayerScore(i_player1);
            printPlayerScore(i_player2);

            Console.WriteLine("DO YOU WANT TO START A NEW GAME: (y/n)");
            
            i_flag = isAnswerYes();
            return i_flag;
        }

        private void printWinnerMessage(PlayerMemoryGame i_player1, PlayerMemoryGame i_player2)
        {
            if (i_player1.Score > i_player2.Score)
            {
                Console.WriteLine("{i_player1.name} WON!");
            }
            else if (i_player2.Score > i_player1.Score)
            {
                Console.WriteLine("{i_player2.name} WON!");
            }
            else
            {
                Console.WriteLine("THE GAME ENDED IN A TIE!");
            }
        }
        
        private void printPlayerScore(PlayerMemoryGame i_player)
        {
            Console.WriteLine("{i_player.name} GOT {i_player.Score} POINTS!");
        }
    }
}
