using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    internal class GameController
    {
        private enum ePlayerTurn
        {
            Player1Turn,
            Player2Turn
        } //Logic

        private PlayerMemoryGame m_Player1; //Logic
        private PlayerMemoryGame m_Player2; //Logic
        public PlayerMemoryGame CurrentlyPlayingPlayer { get; private set; }
        public BoardMemoryGame Board { get; }//Logic
        private ePlayerTurn m_CurrentTurn = ePlayerTurn.Player1Turn;//Logic

        //private MemoryGameCardCords k_SomeCardCords = (-1, -1);

        public GameController(PlayerMemoryGame i_Player1, PlayerMemoryGame i_Player2, BoardMemoryGame i_Board)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            Board = i_Board;

            CurrentlyPlayingPlayer = m_Player1;
            m_CurrentTurn = ePlayerTurn.Player1Turn;
        }

        public void switchTurn() //Logic
        {
            if (m_CurrentTurn == ePlayerTurn.Player1Turn)
            {
                m_CurrentTurn = ePlayerTurn.Player2Turn;
                CurrentlyPlayingPlayer = m_Player2;
            }
            else // if currentTurn == Player2Turn
            {
                m_CurrentTurn = ePlayerTurn.Player1Turn;
                CurrentlyPlayingPlayer = m_Player1;
            }
        }

        public bool FlipCardOnBoard(MemoryGameCardCords i_CardToFlip)
        {
            return Board.FlipCardOnBoard(i_CardToFlip);
        }

        public void GivePointToCurrentlyPlayingPlayer()
        {
            CurrentlyPlayingPlayer.AddPoint();
        }

        public bool IsGameOver()
        {
            return Board.IsBoardFullyRevealed();
        }

        public void SetNewGameBoard((int, int) i_BoardDimensions)
        {
            Board.SetEmptyBoard(i_BoardDimensions);
        }

        public (PlayerMemoryGame, PlayerMemoryGame) getGamePlayers()
        {
            return (m_Player1, m_Player2);
        }
    }
}
