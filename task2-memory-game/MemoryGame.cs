﻿using System.Threading;
using MemoryGameLogic;

namespace MemoryGameUI
{
    public class MemoryGame
    {
        private enum ePlayerTurn
        {
            Player1Turn,
            Player2Turn
        }

        private PlayerMemoryGame m_Player1; 
        private PlayerMemoryGame m_Player2; 
        private PlayerMemoryGame m_CurrentlyPlayingPlayer;
        private BoardMemoryGame m_Board;

        private ePlayerTurn m_CurrentTurn = ePlayerTurn.Player1Turn;
        private bool m_ContinueGame = true;

        private const int k_MinimumRowSize = 4;
        private const int k_MinimumColumnSize = 4;
        private const int k_MaximumRowSize = 6;
        private const int k_MaximumColumnSize = 6;
        private (int, int) k_SomePair = (1, 1);
        private const bool v_Matched = true;

        public MemoryGame()
        {
            string firstPlayerName = MemoryGameInputManager.GetUsername();
            m_Player1 = new HumanPlayerMemoryGame(firstPlayerName);

            bool isComputerPlaying = MemoryGameInputManager.AskUserIfPlayingAgainstComputer();
            if (isComputerPlaying)
            {
                m_Player2 = new ComputerPlayerMemoryGame();
            }
            else
            {
                string secondPlayerName = MemoryGameInputManager.GetUsername();
                m_Player2 = new HumanPlayerMemoryGame(secondPlayerName);
            }
        }

        public void RunMemoryGame()
        {
            (int, int) pair1;
            (int, int) pair2;
            bool didMatch;
            setUpGame();

            //while user didnt typed 'Q'
            while (m_ContinueGame)
            {
                didMatch = getPairsFromCurrentPlayerAndFlip(out pair1, out pair2, out m_ContinueGame);
                if (!m_ContinueGame)
                {
                    break;
                }

                if (didMatch == v_Matched) //need to print the board normally
                {
                    givePointToCurrentlyPlayingPlayer();
                    m_Board.PrintBoard();
                    startNewGameIfNeeded(out m_ContinueGame);
                }
                else //If didn't match
                {
                    revealCardsForTwoSeconds(pair1, pair2);
                    switchTurn();
                }
            }
        }

        private void setUpGame()
        {
            (int, int) boardDimensions = MemoryGameInputManager.GetBoardSizeFromUser((k_MinimumRowSize, k_MaximumRowSize), (k_MinimumColumnSize, k_MaximumColumnSize));
            m_Board = new BoardMemoryGame(boardDimensions);
            m_Board.PrintBoard();

            m_CurrentTurn = ePlayerTurn.Player1Turn;
            m_CurrentlyPlayingPlayer = m_Player1;
        }

        private void revealCardsForTwoSeconds((int, int) i_Pair1, (int, int) i_Pair2)
        {
            m_Board.RevealCards(i_Pair1, i_Pair2);
            m_Board.PrintBoard();
            Thread.Sleep(2000); //2000 miliseconds = 2 seconds
            m_Board.HideCards(i_Pair1, i_Pair2);
            m_Board.PrintBoard();
        }

        private void startNewGameIfNeeded(out bool o_ContinueGame)
        {
            o_ContinueGame = true;
            if (m_Board.IsBoardFullyRevealed())
            {
                bool startNewGame = MemoryGameInputManager.PrintEndGameMessageAndAskForAnotherGame(m_Player1, m_Player2);
                if (startNewGame)
                {
                    setUpGame();
                }
                else //if the user don't want to start a new game:
                {
                    o_ContinueGame = false; //the same as if the user press Q in a middle of a game, quit
                }
            }
        }

        private bool getPairsFromCurrentPlayerAndFlip(out (int, int) o_Pair1, out (int, int) o_Pair2, out bool o_ContinueGame)
        {
            bool didMatch = false;
            o_Pair2 = k_SomePair;

            o_Pair1 = m_CurrentlyPlayingPlayer.PickCardOnBoard(m_Board, out o_ContinueGame);
            if (m_ContinueGame)
            {
                m_Board.FlipCardOnBoard(o_Pair1.Item1, o_Pair1.Item2);
                if (m_ContinueGame)
                {
                    m_Board.PrintBoard(); //print board after placing the first card
                    o_Pair2 = m_CurrentlyPlayingPlayer.PickCardOnBoard(m_Board, out o_ContinueGame);
                    if (m_ContinueGame)
                    {
                        didMatch = m_Board.FlipCardOnBoard(o_Pair2.Item1, o_Pair2.Item2);
                    }
                }
            }

            return didMatch;
        }

        private void givePointToCurrentlyPlayingPlayer()
        {
            m_CurrentlyPlayingPlayer.AddPoint();
        }

        private void switchTurn()
        {
            if (m_CurrentTurn == ePlayerTurn.Player1Turn)
            {
                m_CurrentTurn = ePlayerTurn.Player2Turn;
                m_CurrentlyPlayingPlayer = m_Player2;
            }
            else // if currentTurn == Player2Turn
            {
                m_CurrentTurn = ePlayerTurn.Player1Turn;
                m_CurrentlyPlayingPlayer = m_Player1;
            }
        }
    }
}