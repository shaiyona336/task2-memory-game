
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
            string i_answer;

            System.Console.WriteLine("do you want to play against a computer: (y/n)");
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

            Console.WriteLine("Enter width of board:");
            row = getInputFromRange(minimumRowSize, maximumRowSize);

            Console.WriteLine("Enter height of board:");
            column = getInputFromRange(minimumColumnSize, maximumColumnSize);

            rowAndColumnSize = new Tuple<int, int>(row, column);
            return rowAndColumnSize;
        }

        private int getInputFromRange(int i_minimum, int i_maximum)
        {
            int inputInt;
            string inputStr = Console.ReadLine();

            while (!(int.TryParse(inputStr, out inputInt) && inputInt >= i_minimum && inputInt <= i_maximum))
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



    }
}
