namespace task2_memory_game
{
    internal class LogicMemoryGame
    {
        public BoardMemoryGame Board { get; private set; }

        public void setEmptyBoard(int i_rows, int i_columns)
        {
            Board = new BoardMemoryGame(i_rows, i_columns);
        }

        public bool isGameOver()
        {
            bool flag = true;
            MemoryCard[,] boardState = Board.BoardState;
            for (int boardXDimension = 0; boardXDimension < Board.BoardWidth && flag; boardXDimension++)
            {
                for (int boardYDimension = 0; boardYDimension < Board.BoardHeight && flag; boardYDimension++)
                {
                    if (boardState[boardXDimension, boardYDimension].IsSeen)
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
            if (Board.isCardValid(i_row, i_column))
            {
                cardToFlipAffectOnBoard = '1';
                isFlippedAPair = Board.openCardInBoard(i_row, i_column);
                if (isFlippedAPair)
                {
                    cardToFlipAffectOnBoard = '2';
                }
            }
            return cardToFlipAffectOnBoard;
        }


    }
}
