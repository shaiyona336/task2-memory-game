using System;

namespace task2_memory_game
{
    internal class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        public ComputerPlayerMemoryGame() : base()
        {
        }

        public (int,int) PickLocationOnBoard(BoardMemoryGame board)
        {
            Random random = new Random();
            int row = random.Next(board.BoardHeight);
            int col = random.Next(board.BoardWidth);

            while (!board.isCardValid(row, col))
            {
                row = random.Next(board.BoardHeight);
                col = random.Next(board.BoardWidth);
            }

            return (row, col);
        }

    }
}
