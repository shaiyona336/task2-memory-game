using System;

namespace task2_memory_game
{
    class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        private Random m_RandomNumGenerator;
        public const string k_ComputerName = "Computer";

        public ComputerPlayerMemoryGame() : base()
        {
            Name = k_ComputerName;
            m_RandomNumGenerator = new Random();
        }

        public override (int, int) PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame)
        {
            o_ContinueGame = true;
            (int, int) pair1 = generateRandomPair(i_Board);

            while (!i_Board.IsCardValid(pair1))
            {
                pair1 = generateRandomPair(i_Board);
            }

            return pair1;
        }

        private (int, int) generateRandomPair(BoardMemoryGame i_Board)
        {
            int row = m_RandomNumGenerator.Next(i_Board.BoardHeight);
            int col = m_RandomNumGenerator.Next(i_Board.BoardWidth);

            return (row, col);
        }
    }
}