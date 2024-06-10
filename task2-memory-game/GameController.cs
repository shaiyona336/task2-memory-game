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

        public MemoryGameCardCords PlayCurrentTurn(out bool o_DidCardsMatch, out bool o_ShouldGameContinue, out bool didGameOver)
        {
            didGameOver = false;
            o_DidCardsMatch = false;
            bool wasThereARevealedCardBeforeCurrentFlip = Board.IsThereARevealedCard;

            MemoryGameCardCords cardToFlip = CurrentlyPlayingPlayer.PickCardOnBoard(Board, out o_ShouldGameContinue);
            if (o_ShouldGameContinue)
            {
                o_DidCardsMatch = Board.FlipCardOnBoard(cardToFlip);

                if (o_DidCardsMatch)
                {
                    GivePointToCurrentlyPlayingPlayer();
                    didGameOver = IsGameOver();
                }
                else if (wasThereARevealedCardBeforeCurrentFlip)
                {
                    switchTurn();
                }
            }

            return cardToFlip;
        }

        public void GivePointToCurrentlyPlayingPlayer()
        {
            CurrentlyPlayingPlayer.AddPoint();
        }

        public bool IsGameOver()
        {
            return Board.IsBoardFullyRevealed();
        }

        public void getWinnerAndLoser(out PlayerMemoryGame o_Winner, out PlayerMemoryGame o_Loser,
            out bool o_DidGameEndInATie)
        {
            o_DidGameEndInATie = false;
            o_Winner = m_Player1;
            o_Loser = m_Player2;

            if (m_Player2.Score > m_Player1.Score)
            {
                o_Winner = m_Player2;
                o_Loser = m_Player1;
            }
            else if (m_Player2.Score == m_Player1.Score)
            {
                o_DidGameEndInATie = true;
            }
        }

        public void SetUpNewGame((int, int) i_NewGameBoardDimensions)
        {
            Board.SetEmptyBoard(i_NewGameBoardDimensions);
        }
    }
}
