

namespace task2_memory_game
{
    internal class LogicMemoryGame
    {
        private BoardMemoryGame board;
        private bool UserOpenedOneCard = false;
        int withRowCardUserOpen;
        int withColumnCardUserOpen;




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
            char cardToFlipAffectOnBoard = '0';
            if (isCardValid(i_row, i_column))
            {
                cardToFlipAffectOnBoard = '1';
                
                if (UserOpenedOneCard == true)
                {
                    if (board.getBoardState()[i_row, i_column].getNumberOfPair() == board.getBoardState()[withRowCardUserOpen, withColumnCardUserOpen].getNumberOfPair())
                    {
                        board.getBoardState()[i_row, i_column].setIsSeen(true); // need to flip this card and keep his state like that
                    }
                    else //if the card flipped wasnt a pair
                    {
                        board.getBoardState()[withRowCardUserOpen, withColumnCardUserOpen].setIsSeen(false); // need to set the old card that been flipped to not seen again
                    }

                }
                else //if (UserOpenedOneCard == false), then need to set the card that is now opened in the middle of a turn
                {
                    withRowCardUserOpen = i_row;
                    withColumnCardUserOpen = i_column;
                }
                UserOpenedOneCard = !UserOpenedOneCard; //if user open card, now need to flip the condition of one card open in a turn
            }

            return cardToFlipAffectOnBoard;
        }


        public bool isCardValid(int i_row, int i_column)
        {
            MemoryCard[,] boardState = board.getBoardState();
            bool flag = false;
            
            if (i_row <= 0 && i_row >= board.getBoardXDimension() && i_column <= 0 && i_column >= board.getBoardYDimension())
            {
                if (!(boardState[i_row, i_column].getIsSeen()))
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}
