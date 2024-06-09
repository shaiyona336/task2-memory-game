using MemoryGameLogic;
using System.Runtime.ConstrainedExecution;

namespace MemoryGameUI
{
    internal class HumanPlayerMemoryGame : PlayerMemoryGame
    {
        private MemoryGameCardCords k_SomeCardCords = (-1, -1);

        public HumanPlayerMemoryGame(string i_PlayerName)
        {
            Name = i_PlayerName;
        }

        public override MemoryGameCardCords PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame)
        {
            string cardToOpenStr;
            MemoryGameCardCords outPair = k_SomeCardCords;
            bool isStringAValidPair = false;
            o_ContinueGame = true;

            while (!isStringAValidPair)
            {
                cardToOpenStr = MemoryGameInputManager.AskUserForCardToOpen(out o_ContinueGame);
                if (!o_ContinueGame)
                {
                    break;
                }
                else if (!isStringAPairOfCharAndInt(cardToOpenStr))
                {
                    MemoryGameInputManager.PrintIllegalInputFromUserMessage();
                }
                else if (!convertStringToCardIfPossible(cardToOpenStr, out outPair, i_Board))
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

        private bool convertStringToCardIfPossible(string i_CardStrToConvert, out MemoryGameCardCords o_CardCordsAfterConvert,
            BoardMemoryGame i_BoardOfRequestedCard)
        {
            bool returnValue = false;
            o_CardCordsAfterConvert = k_SomeCardCords;

            if (isStringAPairOfCharAndInt(i_CardStrToConvert))
            {
                o_CardCordsAfterConvert = convertStringToCardCords(i_CardStrToConvert);
                if (i_BoardOfRequestedCard.IsCardOnGameBoard(o_CardCordsAfterConvert))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private bool isStringAPairOfCharAndInt(string i_StrToCheck)
        {
            bool returnValue = true;

            if (i_StrToCheck.Length < 2)
            {
                returnValue = false;
            }
            else
            {
                char checkIfUpper = i_StrToCheck[0];
                char checkIfDigit = i_StrToCheck[1];

                if (i_StrToCheck.Length > 2 || char.IsLower(checkIfUpper) || !char.IsDigit(checkIfDigit))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        //Assumes that the given string is a valid Card 
        private MemoryGameCardCords convertStringToCardCords(string i_CardToConvert)
        {
            int charPartOfCard = i_CardToConvert[0] - 'A';
            int intPartOfCard = (int)char.GetNumericValue(i_CardToConvert[1]) - 1;

            return (intPartOfCard, charPartOfCard);
        }

    }
}
