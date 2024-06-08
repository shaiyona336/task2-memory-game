using System;

namespace task2_memory_game
{
    class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        public ComputerPlayerMemoryGame() : base()
        {
        }

        public ((int, int), (int, int)) PickLocationOnBoard(BoardMemoryGame board)
        {
            Random random = new Random();
            int row = random.Next(board.BoardHeight);
            int col = random.Next(board.BoardWidth);
            int row2 = random.Next(board.BoardHeight);
            int col2 = random.Next(board.BoardWidth);

            while (!board.isCardValid(row, col))
            {
                row = random.Next(board.BoardHeight);
                col = random.Next(board.BoardWidth);
            }

            while (row != row2 && col != col2 && !board.isCardValid(row2, col2))
            {
                row2 = random.Next(board.BoardHeight);
                col2 = random.Next(board.BoardWidth);
            }


            return ((row, col), (row2, col2));
        }
    }
}