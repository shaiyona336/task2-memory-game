

namespace task2_memory_game
{
    internal class LogicMemoryGame
    {
        private BoardMemoryGame board;


  

        public BoardMemoryGame getBoard()
        {
            return board;
        }


        public void setBoard(int i_rows, int i_columns)
        {
            board = new BoardMemoryGame(i_rows, i_columns);

        }


       

    }
}
