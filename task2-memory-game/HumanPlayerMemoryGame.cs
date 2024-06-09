namespace task2_memory_game
{
    internal class HumanPlayerMemoryGame : PlayerMemoryGame
    {
        public string Name { get; }

        private (int, int) k_SomePair = (1, 1);

        public HumanPlayerMemoryGame(string i_namePlayer) 
        {
            Name = i_namePlayer;
        }

        public override (int, int) PickCardOnBoard(BoardMemoryGame board, out bool continueGame)
        {
            (int, int) pair = getCardFromUser(board, out continueGame);
            return pair;
        }

        private (int, int) getCardFromUser(BoardMemoryGame board, out bool o_ContinueGame)
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

                isStringAValidPair = convertStringToPairIfPossible(cardToOpenStr, board, out outPair);

                if (!isStringAValidPair)
                {
                    UIOfMemoryGame.PrintCardNotInBorderWarning();
                }
                else
                {
                    isStringAValidPair = board.IsCardValid(outPair);
                    if (!isStringAValidPair)
                    {
                        UIOfMemoryGame.printIllegalPlaceForCardMessage();
                    }
                }
            }
            return outPair;
        }

        private bool convertStringToPairIfPossible(string i_CardToOpen, BoardMemoryGame board, out (int, int) o_Pair)
        { 
            bool returnValue = false;
            o_Pair = k_SomePair;

            if (isCardAPairOfCharAndInt(i_CardToOpen))
            {
                o_Pair = convertStringToPair(i_CardToOpen);
                if (board.IsPairOnGameBoard(o_Pair))
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
