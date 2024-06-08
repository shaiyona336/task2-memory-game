using System;
using System.Threading;
using static task2_memory_game.LogicMemoryGame;

namespace task2_memory_game
{
    public class Interface
    {
        private const int k_MinimumRowSize = 4;
        private const int k_MinimumColumnSize = 4;
        private const int k_MaximumRowSize = 6;
        private const int k_MaximumColumnSize = 6;
        private (int, int) k_SomePair = ('a', 1);

        private bool isComputerPlaying = false;
        ComputerPlayerMemoryGame computer;
        private enum ePlayerTurn
        {
            Player1Turn,
            Player2Turn
        }

        private HumanPlayerMemoryGame player1;//change to general player
        private HumanPlayerMemoryGame player2;//change to general player
        private ePlayerTurn currentTurn;
        private bool continueGame = true;

        private LogicMemoryGame logicMemoryGame;//Might make all methods static

        public Interface()
        {
            currentTurn = ePlayerTurn.Player1Turn;
            logicMemoryGame = new LogicMemoryGame();
        }

        private void setUpGame()
        {
            //string currentCardToOpen;
            //var pair = (-1, -1); //convert card to open format to something logic can deal with
            //var secondPair = (-1, -1);

            string firstPlayerName = UIOfMemoryGame.GetUsername();
            player1 = new HumanPlayerMemoryGame(firstPlayerName);
            bool againstComputer = UIOfMemoryGame.AgainstHumanOrComputer();
            if (!againstComputer)
            {
                string secondPlayerName = UIOfMemoryGame.GetUsername();
                player2 = new HumanPlayerMemoryGame(secondPlayerName);
            }
            else
            {
                isComputerPlaying = true;
                computer = new ComputerPlayerMemoryGame();
            }

            (int, int) boardDimensions = UIOfMemoryGame.GetBoardSizeFromUser((k_MinimumRowSize, k_MaximumRowSize), (k_MinimumColumnSize, k_MaximumColumnSize));
            logicMemoryGame.setEmptyBoard(boardDimensions);
            logicMemoryGame.Board.printBoard();
            logicMemoryGame.Board.GeneratePairs();
        }

        public void game()
        {
            (int, int) pair1 = (0, 0);
            (int, int) pair2 = (0, 0);
            eCardState openCardState;
            setUpGame();

            //while user didnt typed 'Q'
            while (continueGame)
            {
                if (isComputerPlaying && currentTurn == ePlayerTurn.Player2Turn)
                {
                    (pair1, pair2) = computer.PickLocationOnBoard(logicMemoryGame.Board);

                    openCardState = flipTwoPairs(ref pair1, ref pair2, out continueGame, true);


                }
                else
                {
                    openCardState = flipTwoPairs(ref pair1, ref pair2, out continueGame, false);
                }
                if (!continueGame)
                {
                    break;
                }

                //need to check if openCardState is 1 or 2, if 1 print the board with the 2 cards that the user open for two seconds
                if (openCardState == eCardState.FlippedAndMatched) //need to print the board normally
                {
                    givePointToCurrentlyPlayingPlayer();
                    logicMemoryGame.Board.printBoard();
                    startNewGameIfRequested(out continueGame);
                }
                else //openCardState == '1'
                {
                    revealAndHideCards(pair1, pair2);
                }
                switchTurn();
            }
        }

        private void revealAndHideCards((int, int) i_Pair1, (int, int) i_Pair2)
        {
            logicMemoryGame.Board.RevealCards(i_Pair1, i_Pair2);
            logicMemoryGame.Board.printBoard();
            Thread.Sleep(2000); //2000 miliseconds = 2 seconds
            logicMemoryGame.Board.HideCards(i_Pair1, i_Pair2);
            logicMemoryGame.Board.printBoard();
        }

        private void startNewGameIfRequested(out bool continueGame)
        {
            continueGame = true;
            if (logicMemoryGame.isGameOver())
            {
                bool startNewGame = UIOfMemoryGame.EndGameMessageAndAskForAnotherGame(player1, player2);
                if (startNewGame)
                {
                    logicMemoryGame.Board.GeneratePairs();
                }
                else //if the user don't want to start a new game:
                {
                    continueGame = false; //the same as if the user press Q in a middle of a game, quit
                }
            }
        }

        private eCardState flipTwoPairs(ref (int, int) o_Pair1, ref (int, int) o_Pair2, out bool o_ContinueGame, bool isComputerPlayingThisTurn)
        {
            o_ContinueGame = true;
            eCardState openCardState = eCardState.CantFlip;
            if (!isComputerPlayingThisTurn)
            {
                o_Pair2 = k_SomePair;

                o_Pair1 = logicMemoryGame.getCardFromUser(out continueGame);
            }
            if (continueGame)
            {
                logicMemoryGame.CheckIfPairValidAndFlipIfItIs(ref o_Pair1, out continueGame);
                if (continueGame)
                {
                    logicMemoryGame.Board.printBoard(); //print board after placing the first card
                    if (!isComputerPlayingThisTurn)
                    {
                        o_Pair2 = logicMemoryGame.getCardFromUser(out continueGame);
                    }
                    if (continueGame)
                    {
                        openCardState = logicMemoryGame.CheckIfPairValidAndFlipIfItIs(ref o_Pair2, out continueGame);
                    }
                }
            }

            return openCardState;
        }

        public void givePointToCurrentlyPlayingPlayer()
        {
            if (currentTurn == ePlayerTurn.Player1Turn)
            {
                player1.addPoint();
            }
            else
            {
                player2.addPoint();
            }
        }


        public void switchTurn()
        {
            if (currentTurn == ePlayerTurn.Player1Turn)
            {
                currentTurn = ePlayerTurn.Player2Turn;
            }
            else // if currentTurn == Player2Turn
            {
                currentTurn = ePlayerTurn.Player1Turn;
            }
        }
    }
}