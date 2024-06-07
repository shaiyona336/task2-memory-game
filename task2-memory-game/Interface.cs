

using System;
using System.Runtime.InteropServices;

namespace task2_memory_game
{
    internal class Interface
    {
        string firstPlayerName;
        string secondPlayerName;
        private bool isFirstPlayerTurn;
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

        public void game()
        {
            UI = new UIOfMomoryGame();
            logicMemoryGame = new LogicMemoryGame();
            string currentCardToOpen;
            var pair = (-1, -1); //convert card to open format to something logic can deal with
           
            bool againstComputer;
            firstPlayerName = UI.getUsername();
            firstPlayer = new HumanPlayerMemoryGame(firstPlayerName);
            againstComputer = UI.againstHumanOrComputer();
            if (againstComputer)
            {
                computerIsPlaying = true;
            }
            else
            {
                computerIsPlaying = false;
                secondPlayerName = UI.getUsername();
                secondPlayer = new HumanPlayerMemoryGame(secondPlayerName);
            }
            //need to get board dimensions here
            /////////////////
            logicMemoryGame.setBoard(4, 6);
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
                Ex02.ConsoleUtils.Screen.Clear();
                logicMemoryGame.getBoard().printBoard(); //print board after placing the first card
                //open second card first player
                pair = askUserForLigalCardToOpen();
                openCardState = logicMemoryGame.openCard(pair.Item1, pair.Item2);
                while (openCardState == '0') //cant flip unmatched card on the first card that we open in the turn, just need to check it flipped the card
                {
                    UI.printIllegalPlaceForCard();
                    pair = askUserForLigalCardToOpen();
                    openCardState = logicMemoryGame.openCard(pair.Item1, pair.Item2);
                }


            }
            Console.ReadLine();
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
                cardToOpen[0] - 'A' <= logicMemoryGame.getBoard().getBoardYDimension() &&
                cardToOpen[1] - '0' >= 0 &&
                cardToOpen[1] - '0' <= logicMemoryGame.getBoard().getBoardXDimension())
            {
                pair = (cardToOpen[1] - '0' - 1, cardToOpen[0] - 'A');
            }
            return pair;
        }
    }
}
