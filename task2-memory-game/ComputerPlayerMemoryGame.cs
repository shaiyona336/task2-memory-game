using System;

namespace task2_memory_game
{
    class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        private Random m_RandomNumGenerator;

        public const string Name = "computer";


        public ComputerPlayerMemoryGame() : base()
        {
            m_RandomNumGenerator = new Random();
        }

        public override (int, int) PickCardOnBoard(BoardMemoryGame board, out bool continueGame)
        {
            continueGame = true;
            (int, int) pair1 = generateRandomPair(board);

            while (!board.IsCardValid(pair1))
            {
                pair1 = generateRandomPair(board);
            }

            return pair1;
        }

        private (int, int) generateRandomPair(BoardMemoryGame board)
        {
            int row = m_RandomNumGenerator.Next(board.BoardHeight);
            int col = m_RandomNumGenerator.Next(board.BoardWidth);

            return (row, col);
        }
    }
}