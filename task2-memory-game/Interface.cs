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

        private const bool k_Matched = true;
        private const bool k_DidntMatch = false;

        private bool isComputerPlaying;
        private enum ePlayerTurn
        {
            Player1Turn = 1,
            Player2Turn = 0
        }

        private PlayerMemoryGame player1; //change to general player
        private PlayerMemoryGame player2; //change to general player
        private PlayerMemoryGame currentlyPlayingPlayer;

        private ePlayerTurn currentTurn;
        private bool continueGame = true;

        private LogicMemoryGame logicMemoryGame; //Might delete
        private BoardMemoryGame board;

        public Interface()
        {
            currentTurn = ePlayerTurn.Player1Turn;
            setUpGame();
            currentlyPlayingPlayer = player1;
        }

        private void setUpGame()
        {
            string firstPlayerName = UIOfMemoryGame.GetUsername();
            player1 = new HumanPlayerMemoryGame(firstPlayerName);

            isComputerPlaying = UIOfMemoryGame.AgainstHumanOrComputer();
            if (isComputerPlaying)
            {
                player2 = new ComputerPlayerMemoryGame();
            }
            else
            {
                string secondPlayerName = UIOfMemoryGame.GetUsername();
                player2 = new HumanPlayerMemoryGame(secondPlayerName);
            }

            (int, int) boardDimensions = UIOfMemoryGame.GetBoardSizeFromUser((k_MinimumRowSize, k_MaximumRowSize), (k_MinimumColumnSize, k_MaximumColumnSize));
            board = new BoardMemoryGame(boardDimensions);
            logicMemoryGame = new LogicMemoryGame(board);
            board.printBoard();
            board.GeneratePairs();
        }

        public void game()
        {
            (int, int) pair1;
            (int, int) pair2;

            bool didMatch;
            //setUpGame();

            //while user didnt typed 'Q'
            while (continueGame)
            {
                didMatch = getPairsFromCurrentPlayerAndFlip(out pair1, out pair2, out continueGame);
                if (!continueGame)
                {
                    break;
                }

                //need to check if openCardState is 1 or 2, if 1 print the board with the 2 cards that the user open for two seconds
                if (didMatch == k_Matched) //need to print the board normally
                {
                    givePointToCurrentlyPlayingPlayer();
                    board.printBoard();
                    startNewGameIfRequested(out continueGame);
                }
                else //If didn't match
                {
                    revealAndHideCards(pair1, pair2);
                    switchTurn();
                }
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

        private bool getPairsFromCurrentPlayerAndFlip(out (int, int) o_Pair1, out (int, int) o_Pair2, out bool o_ContinueGame)
        {
            o_ContinueGame = true;
            bool didMatch = false;
            o_Pair2 = k_SomePair;

            o_Pair1 = logicMemoryGame.getCardFromPlayer(currentlyPlayingPlayer, out continueGame);
            if (continueGame)
            {
                board.flipCardOnBoard(o_Pair1.Item1, o_Pair1.Item2);
                if (continueGame)
                {
                    board.printBoard(); //print board after placing the first card
                    o_Pair2 = logicMemoryGame.getCardFromPlayer(currentlyPlayingPlayer, out continueGame);
                    if (continueGame)
                    {
                        didMatch = board.flipCardOnBoard(o_Pair2.Item1, o_Pair2.Item2);
                    }
                }
            }

            return didMatch;
        }

        public void givePointToCurrentlyPlayingPlayer()
        {
            currentlyPlayingPlayer.addPoint();
        }

        private void switchTurn()
        {
            if (currentTurn == ePlayerTurn.Player1Turn)
            {
                currentTurn = ePlayerTurn.Player2Turn;
                currentlyPlayingPlayer = player2;
            }
            else // if currentTurn == Player2Turn
            {
                currentTurn = ePlayerTurn.Player1Turn;
                currentlyPlayingPlayer = player1;
            }
        }
    }
}
