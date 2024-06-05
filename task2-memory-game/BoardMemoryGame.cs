

namespace task2_memory_game
{
    internal class BoardMemoryGame
    {
        private int m_sizeRowBoard;
        private int m_sizeColumnBoard;
        private int[,] boardState;

        BoardMemoryGame(int i_sizeRowBoard, int i_sizeColumnBoard)
        {
            m_sizeRowBoard = i_sizeRowBoard;
            m_sizeColumnBoard = i_sizeColumnBoard;
            boardState = new int[m_sizeRowBoard, m_sizeColumnBoard];
        }
    }
}
