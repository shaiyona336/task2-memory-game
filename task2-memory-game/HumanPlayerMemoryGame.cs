namespace task2_memory_game
{
    internal class HumanPlayerMemoryGame : PlayerMemoryGame
    {
        private (int, int) k_SomePair = (1, 1);

        public HumanPlayerMemoryGame(string i_PlayerName) 
        {
            Name = i_PlayerName;
        }

        public override (int, int) PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame)
        {
            (int, int) pair = getCardFromUser(i_Board, out o_ContinueGame);
            return pair;
        }

        private (int, int) getCardFromUser(BoardMemoryGame i_Board, out bool o_ContinueGame)
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

                isStringAValidPair = convertStringToPairIfPossible(cardToOpenStr, i_Board, out outPair);
                if (!isStringAValidPair)
                {
                    UIOfMemoryGame.PrintCardNotInBorderWarning();
                }
                else
                {
                    isStringAValidPair = i_Board.IsCardValid(outPair);
                    if (!isStringAValidPair)
                    {
                        UIOfMemoryGame.PrintIllegalPlaceForCardMessage();
                    }
                }
            }
            return outPair;
        }

        private bool convertStringToPairIfPossible(string i_CardToOpen, BoardMemoryGame i_Board, out (int, int) o_Pair)
        { 
            bool returnValue = false;
            o_Pair = k_SomePair;

            if (isCardAPairOfCharAndInt(i_CardToOpen))
            {
                o_Pair = convertStringToPair(i_CardToOpen);
                if (i_Board.IsPairOnGameBoard(o_Pair))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private bool isCardAPairOfCharAndInt(string i_CardToCheck)
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

        private (int, int) convertStringToPair(string i_CardToConvert)
        {
            int charPartOfCard = i_CardToConvert[0] - 'A';
            int intPartOfCard = (int)char.GetNumericValue(i_CardToConvert[1]) - 1;

            return (intPartOfCard, charPartOfCard);
        }

    }
}
