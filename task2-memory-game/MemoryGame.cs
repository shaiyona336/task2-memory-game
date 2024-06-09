using System.Threading;
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
        private const int k_TimeToFreezeGameInMilliseconds = 2000; //2000 miliseconds = 2 seconds
        private MemoryGameCardCords k_SomeCardCords = (1, 1);
        private const bool v_CardsMatched = true;

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
            MemoryGameCardCords card1Cords;
            MemoryGameCardCords card2Cords;
            bool didCardsMatch;
            setUpGame();

            //while user didnt typed 'Q'
            while (m_ContinueGame)
            {
                didCardsMatch = getCardsFromCurrentPlayerAndFlip(out card1Cords, out card2Cords);
                if (!m_ContinueGame)
                {
                    break;
                }

                if (didCardsMatch == v_CardsMatched) //need to print the board normally
                {
                    givePointToCurrentlyPlayingPlayer();
                    m_Board.PrintBoard();
                    startNewGameIfNeeded();
                }
                else //If didn't match
                {
                    revealCardsForTwoSeconds(card1Cords, card2Cords);
                    switchTurn();
                }
            }
        }

        private void setUpGame()
        {
            (int,int) boardDimensions = MemoryGameInputManager.GetBoardSizeFromUser((k_MinimumRowSize, k_MaximumRowSize), (k_MinimumColumnSize, k_MaximumColumnSize));
            m_Board = new BoardMemoryGame(boardDimensions);
            m_Board.PrintBoard();

            m_CurrentTurn = ePlayerTurn.Player1Turn;
            m_CurrentlyPlayingPlayer = m_Player1;
        }

        private void revealCardsForTwoSeconds(MemoryGameCardCords i_Card1Cords, MemoryGameCardCords i_Card2Cords)
        {
            m_Board.RevealCardsOnBoard(i_Card1Cords, i_Card2Cords);
            m_Board.PrintBoard();
            Thread.Sleep(k_TimeToFreezeGameInMilliseconds); 
            m_Board.HideCards(i_Card1Cords, i_Card2Cords);
            m_Board.PrintBoard();
        }

        private void startNewGameIfNeeded()
        {
            if (m_Board.IsBoardFullyRevealed())
            {
                bool startNewGame = MemoryGameInputManager.PrintEndGameMessageAndAskForAnotherGame(m_Player1, m_Player2);
                if (startNewGame)
                {
                    setUpGame();
                }
                else //if the user don't want to start a new game:
                {
                    m_ContinueGame = false; //the same as if the user press Q in a middle of a game: quit
                }
            }
        }

        private bool getCardsFromCurrentPlayerAndFlip(out MemoryGameCardCords o_Card1Cords,
            out MemoryGameCardCords o_Card2Cords)
        {
            bool didMatch = false;
            o_Card2Cords = k_SomeCardCords;

            o_Card1Cords = m_CurrentlyPlayingPlayer.PickCardOnBoard(m_Board, out m_ContinueGame);
            if (m_ContinueGame)
            {
                m_Board.FlipCardOnBoard(o_Card1Cords);
                if (m_ContinueGame)
                {
                    m_Board.PrintBoard(); //print board after placing the first card
                    o_Card2Cords = m_CurrentlyPlayingPlayer.PickCardOnBoard(m_Board, out m_ContinueGame);
                    if (m_ContinueGame)
                    {
                        didMatch = m_Board.FlipCardOnBoard(o_Card2Cords);
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
