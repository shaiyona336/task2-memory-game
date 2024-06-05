
using System;


namespace task2_memory_game
{
    internal class UIOfMomoryGame
    {
        public string getUsername()
        {
            string name;
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
            Tuple<int, int> i_rowAndColumnSize;
            string i_rowInput;
            string i_columnInput;
            int i_row;
            int i_column;

            i_rowInput = Console.ReadLine();
            while (int.TryParse(i_rowInput, out i_row) && i_row >= minimumRowSize && i_row <= maximumRowSize)
            {
                i_rowInput = Console.ReadLine();
            }
            i_columnInput = Console.ReadLine();
            while (int.TryParse(i_columnInput, out i_column) && i_column >= minimumColumnSize && i_column <= maximumColumnSize)
            {
                i_columnInput = Console.ReadLine();
            }

            i_rowAndColumnSize = new Tuple<int, int>(i_row, i_column);

            return i_rowAndColumnSize;

        }

        public void lineOfEquals(int i_columns)
        {
            Console.Write("  ");
            for (int amountOfEquals = 0; amountOfEquals < i_columns * 2 + 1; amountOfEquals++)
            {
                Console.Write('=');
            }
            Console.WriteLine();
        }

        public void printBoard(int i_rows, int i_columns)
        {
            Console.Write("  ");
            for (int latter = 0; latter < i_columns; latter++)
            {
                Console.Write((char)('A' + latter) + " ");
            }
            Console.WriteLine();
            //top border
            lineOfEquals(i_columns);
            //rows
            for (int row = 1; row <= i_rows; row++)
            {
                Console.Write(row + "| ");
                for (int col = 0; col < i_columns; col++)
                {
                    Console.Write("| ");
                }
                Console.WriteLine();
                lineOfEquals(i_columns);
            }
        }


    }
}
