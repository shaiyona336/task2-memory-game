namespace task2_memory_game
{
    internal class LogicMemoryGame
    {
        private (char, int) k_SomePair = ('a',1);
        public BoardMemoryGame Board { get; private set; }
        public enum eCardState
        {
            CantFlip = '0',
            FlippedButDidntMatch,
            FlippedAndMatched
        }

        public void setEmptyBoard((int, int) i_BoardDimensions)
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

        public eCardState CheckIfPairValidAndFlipIfItIs(ref (char, int) io_Pair, out bool o_ContinueGame)
        {
            eCardState openCardState = tryFlippingPair(io_Pair.Item1 - 'A', io_Pair.Item2);
            o_ContinueGame = true;

            while (openCardState == eCardState.CantFlip) //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
            {
                UIOfMemoryGame.printIllegalPlaceForCard();
                io_Pair = getCardFromUser(out o_ContinueGame);
                openCardState = tryFlippingPair(io_Pair.Item1 - 'A', io_Pair.Item2);
            }
            return openCardState;
        }

        //the function returns one of three things, '0' if the card that were chosen were invalid to flip, '1' if the card flipped and didnt matched a pair, '2' if the card was flipped and matched a pair
        public eCardState tryFlippingPair(int i_row, int i_column)
        {
            eCardState stateAfterFlipAttempt = eCardState.CantFlip;

            if (Board.isCardValid(i_row, i_column))
            {
                stateAfterFlipAttempt = eCardState.FlippedButDidntMatch;
                bool didFlipAPair = Board.flipCardOnBoard(i_row, i_column);
                if (didFlipAPair)
                {
                    stateAfterFlipAttempt = eCardState.FlippedAndMatched;
                }
            }
            return stateAfterFlipAttempt;
        }

        public (char, int) getCardFromUser(out bool o_ContinueGame)
        {
            string cardToOpenStr;
            (char, int) outPair = k_SomePair;
            bool isStringAValidPair = false;
            o_ContinueGame = true;

            while (!isStringAValidPair)
            {
                cardToOpenStr = UIOfMemoryGame.AskUserForCardToOpen(out o_ContinueGame);
                if (!o_ContinueGame)
                {
                    break;
                }

                isStringAValidPair = convertStringToPairIfPossible(cardToOpenStr, out outPair);
                if (!isStringAValidPair)
                {
                    UIOfMemoryGame.PrintCardNotInBorderWarning();
                }
            }
            return outPair;
        }

        private bool convertStringToPairIfPossible(string i_CardToOpen, out (char, int) o_Pair)
        {
            bool returnValue = false;
            o_Pair = k_SomePair;

            if (isCardAPairOfCharAndInt(i_CardToOpen))
            {
                o_Pair = convertStringToPair(i_CardToOpen);
                if (isPairOnGameBoard(o_Pair))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        //Method assumes that the string given is actually a valid pair
        private (char, int) convertStringToPair(string i_CardToConvert)
        {
            char charPartOfCard = i_CardToConvert[0];
            int intPartOfCard = (int)char.GetNumericValue(i_CardToConvert[1]);

            return (charPartOfCard, intPartOfCard);
        }

        private bool isCardAPairOfCharAndInt(string i_CardToCheck)
        {
            bool returnValue = true;
            char checkIfUpper = i_CardToCheck[0];
            char checkIfDigit = i_CardToCheck[1];

            if (i_CardToCheck.Length != 2 ||
                char.IsLower(checkIfUpper) ||
                !char.IsDigit(checkIfDigit))
            {
                returnValue = false;
            }

            return returnValue;
        }

        private bool isPairOnGameBoard((char, int) i_Pair)
        {
            char lastValidCharOnBoard = GetLastValidCharacterOnBoard();
            int boardWidth = Board.BoardWidth;
            bool returnValue = true;

            if (i_Pair.Item1 > lastValidCharOnBoard || i_Pair.Item1 < 'A' || i_Pair.Item2 > boardWidth || i_Pair.Item2 < 0)
            {
                returnValue = false;
            }
            return returnValue;
        }


    }
}
