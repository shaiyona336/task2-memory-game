using MemoryGameLogic;
using System.Runtime.ConstrainedExecution;

namespace MemoryGameUI
{
    internal class HumanPlayerMemoryGame : PlayerMemoryGame
    {
        private MemoryGameCardCords k_SomePair = (-1, -1);

        public HumanPlayerMemoryGame(string i_PlayerName)
        {
            Name = i_PlayerName;
        }

        public override MemoryGameCardCords PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame)
        {
            string cardToOpenStr;
            MemoryGameCardCords outPair = k_SomePair;
            bool isStringAValidPair = false;
            o_ContinueGame = true;

            while (!isStringAValidPair)
            {
                cardToOpenStr = MemoryGameInputManager.AskUserForCardToOpen(out o_ContinueGame);
                if (!o_ContinueGame)
                {
                    break;
                }
                else if (!isCardAPairOfCharAndInt(cardToOpenStr))
                {
                    MemoryGameInputManager.PrintIllegalInputFromUserMessage();
                }
                else if (!convertStringToPairIfPossible(cardToOpenStr, i_Board, out outPair))
                {
                    MemoryGameInputManager.PrintCardNotInBorderWarning();
                }
                else if (!i_Board.IsCardHidden(outPair))
                {
                    MemoryGameInputManager.PrintIllegalPlaceForCardMessage();
                }
                else
                {
                    isStringAValidPair = true;
                }
            }
            return outPair;
        }

        private bool convertStringToPairIfPossible(string i_CardToOpen, BoardMemoryGame i_Board, out MemoryGameCardCords o_Pair)
        {
            bool returnValue = false;
            o_Pair = k_SomePair;

            if (isCardAPairOfCharAndInt(i_CardToOpen))
            {
                o_Pair = convertStringToPair(i_CardToOpen);
                if (i_Board.IsCardOnGameBoard(o_Pair))
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

        private MemoryGameCardCords convertStringToPair(string i_CardToConvert)
        {
            int charPartOfCard = i_CardToConvert[0] - 'A';
            int intPartOfCard = (int)char.GetNumericValue(i_CardToConvert[1]) - 1;

            return (intPartOfCard, charPartOfCard);
        }

    }
}
