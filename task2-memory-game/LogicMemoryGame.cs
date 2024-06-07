namespace task2_memory_game
{
    internal class LogicMemoryGame
    {
        public BoardMemoryGame Board { get; private set; }
        enum eCardState
        {
            CantFlip = '0',
            FlippedButNotMatchedPair,
            FlippedAndMatchedPair
        }


        public void setEmptyBoard((int,int) i_BoardDimensions)
        {
            Board = new BoardMemoryGame(i_BoardDimensions.Item1, i_BoardDimensions.Item2);
        }

        public char GetLastValidCharacterOnBoard()
        {
            return (char)('A' + Board.BoardHeight + 1);
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

        //the function returns one of three things, '0' if the card that were chosen were invalid to flip, '1' if the card flipped and didnt matched a pair, '2' if the card was flipped and matched a pair
        public char openCard(int i_row, int i_column) 
        {
            bool didFlipAPair;
            eCardState cardToFlipAffectOnBoard = eCardState.CantFlip;

            if (Board.isCardValid(i_row, i_column))
            {
                cardToFlipAffectOnBoard = eCardState.FlippedButNotMatchedPair;
                didFlipAPair = Board.openCardInBoard(i_row, i_column);
                if (didFlipAPair)
                {
                    cardToFlipAffectOnBoard = eCardState.FlippedAndMatchedPair;
                }
            }
            return (char)cardToFlipAffectOnBoard;
        }




    }
}
