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
        private PlayerMemoryGame m_CurrentlyPlayingPlayer;//Logic
        private BoardMemoryGame m_Board;//Logic
        private ePlayerTurn m_CurrentTurn = ePlayerTurn.Player1Turn;//Logic

        private MemoryGameCardCords k_SomeCardCords = (-1, -1);

        public GameController(PlayerMemoryGame i_Player1, PlayerMemoryGame i_Player2, BoardMemoryGame i_Board)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_Board = i_Board;

            m_CurrentlyPlayingPlayer = m_Player1;
            m_CurrentTurn = ePlayerTurn.Player1Turn;
        }

        private void switchTurn() //Logic
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

        public bool FlipCardOnBoard(MemoryGameCardCords i_CardToFlip)
        {
            return m_Board.FlipCardOnBoard(i_CardToFlip);
        }

        public void GivePointToCurrentlyPlayingPlayer()
        {
            m_CurrentlyPlayingPlayer.AddPoint();
        }

        public bool IsGameOver()
        {
            return m_Board.IsBoardFullyRevealed();
        }

        public void SetNewGameBoard((int, int) i_BoardDimensions)
        {
            m_Board.SetEmptyBoard(i_BoardDimensions);
        }
    }
}
