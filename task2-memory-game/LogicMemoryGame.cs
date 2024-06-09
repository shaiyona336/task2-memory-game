namespace task2_memory_game
{
    internal class LogicMemoryGame
    {
        private (int, int) k_SomePair = (1, 1);
        public BoardMemoryGame Board { get; private set; }

        public enum eCardState
        {
            CantFlip = '0',
            FlippedButDidntMatch,
            FlippedAndMatched
        }

        public void setEmptyBoard((int, int) i_BoardDimensions) //move to board
        {
            Board = new BoardMemoryGame(i_BoardDimensions.Item1, i_BoardDimensions.Item2);
        }

        public bool isGameOver() //move to board
        {
            bool returnValue = true;
            MemoryCard[,] boardState = Board.BoardState;
            for (int boardXDimension = 0; boardXDimension < Board.BoardWidth && returnValue; boardXDimension++)
            {
                for (int boardYDimension = 0; boardYDimension < Board.BoardHeight && returnValue; boardYDimension++)
                {
                    if (boardState[boardXDimension, boardYDimension].IsSeen)
                    {
                        returnValue = false;
                        break;
                    }
                }
                
                if (!returnValue)
                {
                    break;
                }
            }
            return returnValue;
        }

        public eCardState CheckIfPairValidAndFlipIfItIs(ref (int, int) io_Pair, out bool o_ContinueGame)
        {
            o_ContinueGame = true;
            eCardState openCardState = tryFlippingPair(io_Pair);

            while (openCardState == eCardState.CantFlip) //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
            {
                UIOfMemoryGame.printIllegalPlaceForCardMessage();
                io_Pair = getCardFromUser(out o_ContinueGame);
                openCardState = tryFlippingPair(io_Pair);
            }
            return openCardState;
        }

        //the function returns one of three things, '0' if the card that were chosen were invalid to flip, '1' if the card flipped and didnt matched a pair, '2' if the card was flipped and matched a pair
        public eCardState tryFlippingPair((int, int) i_Pair)
        {
            eCardState stateAfterFlipAttempt = eCardState.CantFlip;

            if (Board.IsCardValid(i_Pair.Item1, i_Pair.Item2))
            {
                stateAfterFlipAttempt = eCardState.FlippedButDidntMatch;
                bool didFlipAPair = Board.flipCardOnBoard(i_Pair.Item1, i_Pair.Item2);
                if (didFlipAPair)
                {
                    stateAfterFlipAttempt = eCardState.FlippedAndMatched;
                }
            }
            return stateAfterFlipAttempt;
        }

        public (int, int) getCardFromUser(out bool o_ContinueGame)//move to player
        {
            string cardToOpenStr;
            (int, int) outPair = k_SomePair;
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

        private bool convertStringToPairIfPossible(string i_CardToOpen, out (int, int) o_Pair)//move to player
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
        private (int, int) convertStringToPair(string i_CardToConvert) //move to player
        {
            int charPartOfCard = i_CardToConvert[0] - 'A';
            int intPartOfCard = (int)char.GetNumericValue(i_CardToConvert[1]) - 1;

            return (intPartOfCard, charPartOfCard);
        }

        private bool isCardAPairOfCharAndInt(string i_CardToCheck) //move to player
        {
            bool returnValue = true;
            if (i_CardToCheck.Length < 2)
            {
                returnValue = false;
            }
            else
            {
                char checkIfUpper = i_CardToCheck[0];
                char checkIfDigit = i_CardToCheck[1];

                if (i_CardToCheck.Length > 2 || char.IsLower(checkIfUpper) || !char.IsDigit(checkIfDigit))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        private bool isPairOnGameBoard((int, int) i_Pair) //move to board
        {
            bool returnValue = true;

            if (i_Pair.Item1 > Board.BoardHeight || i_Pair.Item1 < 0 || i_Pair.Item2 > Board.BoardWidth || i_Pair.Item2 < 0)
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
