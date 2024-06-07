using System;

namespace task2_memory_game
{
    internal class UIOfMomoryGame
    {
        public string getUsername()
        {
            string name;
            Console.WriteLine("Enter username: ");
            name = Console.ReadLine();
            return name;
        }

        public bool againstHumanOrComputer()
        {
            bool i_flag;

            System.Console.WriteLine("do you want to play against a computer: (y/n)");
            i_flag = getIfAnswerIsYes();
            return i_flag;
        }

        public bool getIfAnswerIsYes()
        {
            bool i_flag;
            string i_answer;
            i_answer = Console.ReadLine();
            if (i_answer == "y")
            {
                i_flag = true;
            }
            else
            {
                i_flag = false;
            }
            return i_flag;
        }

        public Tuple<int, int> getBoardSize(int minimumRowSize, int minimumColumnSize, int maximumRowSize, int maximumColumnSize)
        {
            Tuple<int, int> rowAndColumnSize;
            int row;
            int column;

            Console.WriteLine("Enter height of board:");
            row = getInputFromRange(minimumRowSize, maximumRowSize);

            Console.WriteLine("Enter width of board:");
            column = getInputFromRange(minimumColumnSize, maximumColumnSize);

            rowAndColumnSize = new Tuple<int, int>(row, column);
            return rowAndColumnSize;
        }

        private int getInputFromRange(int i_minimum, int i_maximum)
        {
            int inputInt;
            string inputStr = Console.ReadLine();

            while (!(int.TryParse(inputStr, out inputInt) && inputInt >= i_minimum && inputInt <= i_maximum && inputInt % 2 == 0))
            {
                Console.WriteLine("Invalid input! Please try again.");
                inputStr = Console.ReadLine();
            }
            return inputInt;
        }

        public string askUserForCardToOpen()
        {
            string cardToOpen;
            Console.WriteLine("Pick a card to open: ");
            cardToOpen = Console.ReadLine();
            return cardToOpen;
        }

        public void printIllegalPlaceForCard()
        {
            Console.WriteLine("Illigal place for card");
        }

        public void printIllegalPlaceForCardBorder() {
            Console.WriteLine("card not in borders");
        }


        public bool endGameMessageAndAskIfAnotherGame(int amountOfPointsFirstPlayer, int amountOfPointsSecondPlayer)
        {
            bool i_flag;
            Console.WriteLine("THE GAME HAS ENDED");
            //print who won the game
            if (amountOfPointsFirstPlayer > amountOfPointsSecondPlayer)
            {
                Console.WriteLine("FIRST PLAYER WON!");
            }
            else //amountOfPointsFirstPlayer >= amountOfPointsSecondPlayer = second player won
            {
                if (amountOfPointsFirstPlayer > amountOfPointsSecondPlayer)
                {
                    Console.WriteLine("SECOND PLAYER WON!");
                }
            }
            Console.WriteLine("FIRST PLAYER POINTS: " + amountOfPointsFirstPlayer.ToString());
            Console.WriteLine("SECOND PLAYER POINTS: " + amountOfPointsSecondPlayer.ToString());
            //ask if the user want to start a new game
            Console.WriteLine("DO YOU WANT TO START A NEW GAME: (y/n)");
            
            i_flag = getIfAnswerIsYes();
            return i_flag;
        }
    }
}
