

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

        public bool isGameOver()
        {
            bool flag = true;
            MemoryCard[,] boardState = board.getBoardState();
            for (int boardXDimension = 0; boardXDimension < board.getBoardXDimension() && flag; boardXDimension++)
            {
                for (int boardYDimension = 0; boardYDimension < board.getBoardYDimension() && flag; boardYDimension++)
                {
                    if (boardState[boardXDimension, boardYDimension].getIsSeen())
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public char openCard(int i_row, int i_column) //the function returns one of three things, '0' if the card that were chosen were invalid to flip, '1' if the card flipped and didnt matched a pair, '2' if the card was flipped and matched a pair
        {
            bool isFlippedAPair;
            char cardToFlipAffectOnBoard = '0';
            if (board.isCardValid(i_row, i_column))
            {
                cardToFlipAffectOnBoard = '1';
                isFlippedAPair = board.openCard(i_row, i_column);
                if (isFlippedAPair)
                {
                    cardToFlipAffectOnBoard = '2';
                }
            }
            return cardToFlipAffectOnBoard;
        }


    }
}
