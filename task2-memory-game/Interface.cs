using System;
using System.Threading;

namespace task2_memory_game
{
    internal class Interface
    {
        private bool isFirstPlayerTurn = true;
        private bool computerIsPlaying;//?
        ComputerPlayerMemoryGame computer;//?
        HumanPlayerMemoryGame player1;//change to general player
        HumanPlayerMemoryGame player2;//change to general player
        private UIOfMemoryGame UI;//Might make all methods static
        private LogicMemoryGame logicMemoryGame;//Might make all methods static

        private const int minimumRowSize = 4;
        private const int minimumColumnSize = 4;
        private const int maximumRowSize = 6;
        private const int maximumColumnSize = 6;

        private const string k_QuitGame = "Q";

        private char openCardState; //0 the card couldnt opened, 1 card opend but not matched a previous pair
        int firstPlayerPoints = 0;//?
        int secondPlayerPoints = 0;//?

        public Interface()
        {
            UI = new UIOfMemoryGame();
            logicMemoryGame = new LogicMemoryGame();
        }

        public void game()
        {
            string currentCardToOpen;
            var pair = (-1, -1); //convert card to open format to something logic can deal with
            var secondPair = (-1, -1);

            string firstPlayerName = UI.GetUsername();
            player1 = new HumanPlayerMemoryGame(firstPlayerName);
            bool againstComputer = UI.AgainstHumanOrComputer();
            if (againstComputer)
            {
                computerIsPlaying = true;
            }
            else
            {
                computerIsPlaying = false;
                string secondPlayerName = UI.GetUsername();
                player2 = new HumanPlayerMemoryGame(secondPlayerName);
            }

            (int, int) boardDimensions = UI.GetBoardSizeFromUser((minimumRowSize, maximumRowSize), (minimumColumnSize, maximumColumnSize));
            logicMemoryGame.setEmptyBoard(boardDimensions);
            logicMemoryGame.Board.printBoard();
            logicMemoryGame.Board.GeneratePairs();

            //while user didnt typed 'Q'
            while (pair != (-2, -2))
            {
                pair = askUserForLegalCardToOpen();



                //Need To make into 1 function:
                /////////////////////////////////////////

                //open first card first player
                openCardState = logicMemoryGame.openCard(pair.Item1, pair.Item2);
                while (openCardState == '0') //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
                {
                    UI.printIllegalPlaceForCard();
                    pair = askUserForLegalCardToOpen();
                    openCardState = logicMemoryGame.openCard(pair.Item1, pair.Item2);
                }
                logicMemoryGame.Board.printBoard(); //print board after placing the first card


                //open second card first player
                secondPair = askUserForLegalCardToOpen();
                openCardState = logicMemoryGame.openCard(secondPair.Item1, secondPair.Item2);
                while (openCardState == '0') //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
                {
                    UI.printIllegalPlaceForCard();
                    secondPair = askUserForLegalCardToOpen();
                    openCardState = logicMemoryGame.openCard(secondPair.Item1, secondPair.Item2);
                }
                ////////////////////////////////////



                //need to check if openCardState is 1 or 2, if 1 print the board with the 2 cards that the user open for two seconds
                if (openCardState == '2') //need to print the board normally
                {
                    if (isFirstPlayerTurn)
                    {
                        givePointToPlayer(player1);
                    }
                    else
                    {
                        givePointToPlayer(player2);
                    }

                    logicMemoryGame.Board.printBoard();
                    bool isGameOver = logicMemoryGame.isGameOver();
                    if (isGameOver)
                    {
                        bool startNewGame = UI.EndGameMessageAndAskForAnotherGame(player1, player2);
                        if (startNewGame)
                        {
                            logicMemoryGame.Board.GeneratePairs();
                        }
                        else //if the user don't want to start a new game:
                        {
                            pair = (-2, -2); //the same as if the user press Q in a middle of a game, quit
                        }
                    }
                }
                else //openCardState == '1'
                {
                    logicMemoryGame.Board.RevealCards(pair, secondPair);
                    logicMemoryGame.Board.printBoard();
                    Thread.Sleep(2000); //2000 miliseconds = 2 seconds
                    logicMemoryGame.Board.HideCards(pair, secondPair);
                    logicMemoryGame.Board.printBoard();
                }
                switchTurn();
            }
        }

        public void givePointToPlayer(PlayerMemoryGame io_Player)
        {
            io_Player.addPoint();
        }


        public void switchTurn()
        {
            isFirstPlayerTurn = !isFirstPlayerTurn;
        }

        private (char, int) askUserForLegalCardToOpen()
        {
            string currentCardToOpen = UI.AskUserForCardToOpen();

            bool isCardValid = isCardToOpenValidAndConvertFormat(currentCardToOpen, out (char, int) pair);
            while (!isCardValid) //before was "if pair = (-1,-1)"
            {
                UI.printIllegalPlaceForCardBorder();
                currentCardToOpen = UI.AskUserForCardToOpen();
                isCardValid = isCardToOpenValidAndConvertFormat(currentCardToOpen, out pair);
            }
            return pair;
        }

        private bool isCardToOpenValidAndConvertFormat(string i_CardToOpen, out (char, int) o_Pair)
        {
            bool returnValue = false;
            o_Pair = ('Z', -1); //For now...
            if (i_CardToOpen == k_QuitGame)
            {
                o_Pair = ('Z', 1); //For now...
            }

            if (isCardAPairOfCharAndInt(i_CardToOpen))
            {
                o_Pair = convertStringToPair(i_CardToOpen);
                if (isPairOnGameBoard(o_Pair))
                {
                    returnValue = true;
                }
            }

            return returnValue;

            //if (cardToOpen == k_QuitGame)
            //{
            //    pair = (-2, -2); //quit game
            //}
            //else if (cardToOpen.Length == 2 &&
            //    cardToOpen[0] - 'A' >= 0 &&
            //    cardToOpen[0] - 'A' <= logicMemoryGame.Board.BoardHeight + 1 &&
            //    char.GetNumericValue(cardToOpen[1]) >= 0 &&
            //    char.GetNumericValue(cardToOpen[1]) <= logicMemoryGame.Board.BoardWidth)
            //{
            //    pair = (cardToOpen[1] - '0' - 1, cardToOpen[0] - 'A');
            //}
            //return pair;
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
            char lastValidCharOnBoard = logicMemoryGame.GetLastValidCharacterOnBoard();
            int boardWidth = logicMemoryGame.Board.BoardWidth;
            bool returnValue = true;

            if (i_Pair.Item1 > lastValidCharOnBoard || i_Pair.Item1 < 'A' || i_Pair.Item2 > boardWidth || i_Pair.Item2 < 0)
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
