using System;
using System.Threading;
//using static task2_memory_game.LogicMemoryGame;

namespace task2_memory_game
{
    public class Interface
    {

        private enum ePlayerTurn
        {
            Player1Turn = 1,
            Player2Turn = 0
        }

        private PlayerMemoryGame player1; 
        private PlayerMemoryGame player2; 
        private PlayerMemoryGame currentlyPlayingPlayer;
        private BoardMemoryGame board;

        private ePlayerTurn currentTurn = ePlayerTurn.Player1Turn;
        private bool continueGame = true;

        private const int k_MinimumRowSize = 4;
        private const int k_MinimumColumnSize = 4;
        private const int k_MaximumRowSize = 6;
        private const int k_MaximumColumnSize = 6;

        private (int, int) k_SomePair = ('a', 1);

        private const bool k_Matched = true;
        private const bool k_DidntMatch = false;

        public Interface()
        {
            string firstPlayerName = UIOfMemoryGame.GetUsername();
            player1 = new HumanPlayerMemoryGame(firstPlayerName);

            bool isComputerPlaying = UIOfMemoryGame.AgainstHumanOrComputer();
            if (isComputerPlaying)
            {
                player2 = new ComputerPlayerMemoryGame();
            }
            else
            {
                string secondPlayerName = UIOfMemoryGame.GetUsername();
                player2 = new HumanPlayerMemoryGame(secondPlayerName);
            }
        }

        private void setUpGame()
        {
            (int, int) boardDimensions = UIOfMemoryGame.GetBoardSizeFromUser((k_MinimumRowSize, k_MaximumRowSize), (k_MinimumColumnSize, k_MaximumColumnSize));
            board = new BoardMemoryGame(boardDimensions);
            board.printBoard();
            board.GeneratePairs();

            currentTurn = ePlayerTurn.Player1Turn;
            currentlyPlayingPlayer = player1;
        }

        public void game()
        {
            (int, int) pair1;
            (int, int) pair2;
            bool didMatch;
            setUpGame();

            //while user didnt typed 'Q'
            while (continueGame)
            {
                didMatch = getPairsFromCurrentPlayerAndFlip(out pair1, out pair2, out continueGame);
                if (!continueGame)
                {
                    break;
                }

                if (didMatch == k_Matched) //need to print the board normally
                {
                    givePointToCurrentlyPlayingPlayer();
                    startNewGameIfNeeded(out continueGame);
                    board.printBoard();
                }
                else //If didn't match
                {
                    revealCardsForTwoSeconds(pair1, pair2);
                    switchTurn();
                }
            }
        }

        private void revealCardsForTwoSeconds((int, int) i_Pair1, (int, int) i_Pair2)
        {
            board.RevealCards(i_Pair1, i_Pair2);
            board.printBoard();
            Thread.Sleep(2000); //2000 miliseconds = 2 seconds
            board.HideCards(i_Pair1, i_Pair2);
            board.printBoard();
        }

        private void startNewGameIfNeeded(out bool continueGame)
        {
            continueGame = true;
            if (board.IsBoardFullyRevealed())
            {
                bool startNewGame = UIOfMemoryGame.EndGameMessageAndAskForAnotherGame(player1, player2);
                if (startNewGame)
                {
                    setUpGame();
                }
                else //if the user don't want to start a new game:
                {
                    continueGame = false; //the same as if the user press Q in a middle of a game, quit
                }
            }
        }

        private bool getPairsFromCurrentPlayerAndFlip(out (int, int) o_Pair1, out (int, int) o_Pair2, out bool o_ContinueGame)
        {
            bool didMatch = false;
            o_Pair2 = k_SomePair;

            o_Pair1 = currentlyPlayingPlayer.PickCardOnBoard(board, out o_ContinueGame);
            if (continueGame)
            {
                board.flipCardOnBoard(o_Pair1.Item1, o_Pair1.Item2);
                if (continueGame)
                {
                    board.printBoard(); //print board after placing the first card
                    o_Pair2 = currentlyPlayingPlayer.PickCardOnBoard(board, out o_ContinueGame);
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
