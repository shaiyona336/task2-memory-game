

using System;
using System.Threading;


namespace task2_memory_game
{
    internal class Interface
    {
        private bool isFirstPlayerTurn = true;
        private bool computerIsPlaying;
        HumanPlayerMemoryGame firstPlayer;
        HumanPlayerMemoryGame secondPlayer;
        ComputerPlayerMemoryGame computer;
        private UIOfMomoryGame UI;
        private LogicMemoryGame logicMemoryGame;
        private const int minimumRowSize = 4;
        private const int minimumColumnSize = 4;
        private const int maximumRowSize = 6;
        private const int maximumColumnSize = 6;
        private char openCardState; //0 the card couldnt opened, 1 card opend but not matched a previous pair
        int firstPlayerPoints = 0;
        int secondPlayerPoints = 0;
        bool isGameOver = false;
        bool startNewGame = true; //contain the answer from the user, if the value is true the user want to start a new game

        public void game()
        {
            UI = new UIOfMomoryGame();
            logicMemoryGame = new LogicMemoryGame();
            string currentCardToOpen;
            var pair = (-1, -1); //convert card to open format to something logic can deal with
            var secondPair = (-1, -1);
           
            bool againstComputer;
            string firstPlayerName = UI.getUsername();
            firstPlayer = new HumanPlayerMemoryGame(firstPlayerName);
            againstComputer = UI.againstHumanOrComputer();
            if (againstComputer)
            {
                computerIsPlaying = true;
            }
            else
            {
                computerIsPlaying = false;
                string secondPlayerName = UI.getUsername();
                secondPlayer = new HumanPlayerMemoryGame(secondPlayerName);
            }
            var boardDimensions = UI.getBoardSize(minimumRowSize, minimumColumnSize, maximumRowSize, maximumColumnSize);
            int boardRow = boardDimensions.Item1;
            int boardCol = boardDimensions.Item2;

            logicMemoryGame.setBoard(boardRow, boardCol);
            logicMemoryGame.getBoard().printBoard();
            logicMemoryGame.getBoard().generatePairs();
            //while user didnt typed 'Q'
            while (pair != (-2,-2))
            {
                pair = askUserForLigalCardToOpen();
                //open first card first player
                openCardState = logicMemoryGame.openCard(pair.Item1, pair.Item2);
                while (openCardState == '0') //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
                {
                    UI.printIllegalPlaceForCard();
                    pair = askUserForLigalCardToOpen();
                    openCardState = logicMemoryGame.openCard(pair.Item1, pair.Item2);
                }
                logicMemoryGame.getBoard().printBoard(); //print board after placing the first card
                //open second card first player
                secondPair = askUserForLigalCardToOpen();
                openCardState = logicMemoryGame.openCard(secondPair.Item1, secondPair.Item2);
                while (openCardState == '0') //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
                {
                    UI.printIllegalPlaceForCard();
                    secondPair = askUserForLigalCardToOpen();
                    openCardState = logicMemoryGame.openCard(secondPair.Item1, secondPair.Item2);
                }
                //need to check if openCardState is 1 or 2, if 1 print the board with the 2 cards that the user open for two seconds
                if (openCardState == '2') //need to print the board normally
                {
                    givePoints(isFirstPlayerTurn, ref firstPlayerPoints, ref secondPlayerPoints);
                    logicMemoryGame.getBoard().printBoard();
                    isGameOver = logicMemoryGame.isGameOver();
                    if (isGameOver)
                    {
                        startNewGame = UI.endGameMessageAndAskIfAnotherGame(firstPlayerPoints, secondPlayerPoints);
                        if (!startNewGame)
                        {
                            pair = (-2, -2); //the same as if the user press Q in a middle of a game, quit
                        }
                        else //if the user want to start a new game, need to clear the board
                        {
                            logicMemoryGame.getBoard().generatePairs();
                        }
                    }
                }
                else //openCardState == '1'
                {
                    logicMemoryGame.getBoard().setCardsOpenTwoSeconds(pair, secondPair);
                    logicMemoryGame.getBoard().printBoard();
                    Thread.Sleep(2000); //2000 miliseconds = 2 seconds
                    logicMemoryGame.getBoard().setCardsClosedTwoSeconds(pair, secondPair);
                    logicMemoryGame.getBoard().printBoard();
                }
                switchTurn(ref isFirstPlayerTurn);
            }
        }

        public void givePoints(bool isFirstPlayer, ref int firstPlayerPoints, ref int secondPlayerPoints)
        {
            if (isFirstPlayer)
            {
                firstPlayerPoints++;
            }
            else
            {
                secondPlayerPoints++;
            }
        }


        public void switchTurn(ref bool isFirstPlayer)
        {
            if (isFirstPlayer)
            {
                isFirstPlayer = false; ;
            }
            else
            {
                isFirstPlayer = true;
            }
        }


        private (int,int) askUserForLigalCardToOpen()
        {
            string currentCardToOpen = UI.askUserForCardToOpen();
            var pair = isCardToOpenValidAndConvertFormat(currentCardToOpen);
            while (pair == (-1, -1))
            {
                UI.printIllegalPlaceForCardBorder();
                currentCardToOpen = UI.askUserForCardToOpen();
                pair = isCardToOpenValidAndConvertFormat(currentCardToOpen);
            }
            return pair;
        }


        public (int,int) isCardToOpenValidAndConvertFormat(string cardToOpen)
        {
            var pair = (-1, -1); //-1 -1 means error
            if (cardToOpen == "Q")
            {
                pair = (-2, -2); //quit game
            }
            else if (cardToOpen.Length == 2 &&
                cardToOpen[0] - 'A' >= 0 &&
                cardToOpen[0] - 'A' <= logicMemoryGame.getBoard().getBoardYDimension() + 1 &&
                cardToOpen[1] - '0' >= 0 &&
                cardToOpen[1] - '0' <= logicMemoryGame.getBoard().getBoardXDimension())
            {
                pair = (cardToOpen[1] - '0' - 1, cardToOpen[0] - 'A');
            }
            return pair;
        }
    }
}
