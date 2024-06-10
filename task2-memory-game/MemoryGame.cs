using System.Threading;
using MemoryGameLogic;

namespace MemoryGameUI
{
    public class MemoryGame
    {
        private GameController m_GameController;
        private bool m_ContinueGame = true; //UI

        private const int k_MinimumRowSize = 4; //UI
        private const int k_MinimumColumnSize = 4; //UI
        private const int k_MaximumRowSize = 6; //UI 
        private const int k_MaximumColumnSize = 6; //UI
        private const int k_TimeToFreezeGameInMilliseconds = 2000; //2000 miliseconds = 2 seconds  //UI
        private MemoryGameCardCords k_SomeCardCords = (-1, -1);
        private const bool v_CardsMatched = true; //UI

        public MemoryGame() //UI
        {
            string firstPlayerName = MemoryGameInputManager.GetUsername();
            HumanPlayerMemoryGame player1 = new HumanPlayerMemoryGame(firstPlayerName);

            PlayerMemoryGame player2;
            bool isComputerPlaying = MemoryGameInputManager.AskUserIfPlayingAgainstComputer();
            if (isComputerPlaying)
            {
                player2 = new ComputerPlayerMemoryGame();
            }
            else
            {
                string secondPlayerName = MemoryGameInputManager.GetUsername();
                player2 = new HumanPlayerMemoryGame(secondPlayerName);
            }

            BoardMemoryGame gameBoard = setUpNewBoard();
            m_GameController = new GameController(player1, player2, gameBoard);
        }

        public void RunMemoryGame() //UI
        {
            MemoryGameCardCords card1Cords;
            MemoryGameCardCords card2Cords;
            bool didCardsMatch;
            //setUpNewBoard();

            //while user didnt typed 'Q'
            while (m_ContinueGame)
            {
                m_GameController.Board.PrintBoard();
                didCardsMatch = getCardsFromCurrentPlayerAndFlip(out card1Cords, out card2Cords);
                if (!m_ContinueGame)
                {
                    break;
                }

                //UI
                if (didCardsMatch == v_CardsMatched) //need to print the board normally
                {
                    m_GameController.GivePointToCurrentlyPlayingPlayer();
                    m_GameController.Board.PrintBoard();
                    startNewGameIfNeeded();
                }
                else //If didn't match
                {
                    revealCardsForTwoSeconds(card1Cords, card2Cords);
                    m_GameController.switchTurn();
                }
            }
        }

        private BoardMemoryGame setUpNewBoard() //UI
        {
            (int,int) boardDimensions = MemoryGameInputManager.GetBoardSizeFromUser((k_MinimumRowSize, k_MaximumRowSize), (k_MinimumColumnSize, k_MaximumColumnSize));
            return new BoardMemoryGame(boardDimensions);
        }

        private void revealCardsForTwoSeconds(MemoryGameCardCords i_Card1Cords, MemoryGameCardCords i_Card2Cords) //UI
        {
            m_GameController.Board.RevealCardsOnBoard(i_Card1Cords, i_Card2Cords);
            m_GameController.Board.PrintBoard();
            Thread.Sleep(k_TimeToFreezeGameInMilliseconds);
            m_GameController.Board.HideCards(i_Card1Cords, i_Card2Cords);
            m_GameController.Board.PrintBoard();
        }

        private void startNewGameIfNeeded() //UI
        {
            if (m_GameController.IsGameOver())
            {
                (PlayerMemoryGame, PlayerMemoryGame) gamePlayers = m_GameController.getGamePlayers();
                bool startNewGame = MemoryGameInputManager.PrintEndGameMessageAndAskForAnotherGame(gamePlayers);
                if (startNewGame)
                {
                    setUpNewBoard();
                }
                else //if the user don't want to start a new game:
                {
                    m_ContinueGame = false; //the same as if the user press Q in a middle of a game: quit
                }
            }
        }

        private bool getCardsFromCurrentPlayerAndFlip(out MemoryGameCardCords o_Card1Cords,
            out MemoryGameCardCords o_Card2Cords) //UI
        {
            bool didMatch = false;
            o_Card2Cords = k_SomeCardCords;

            o_Card1Cords = m_GameController.CurrentlyPlayingPlayer.PickCardOnBoard(m_GameController.Board, out m_ContinueGame);
            if (m_ContinueGame)
            {
                m_GameController.FlipCardOnBoard(o_Card1Cords);
                if (m_ContinueGame)
                {
                    //Maybe use print of inputManager:
                    m_GameController.Board.PrintBoard(); //print board after placing the first card
                    o_Card2Cords = m_GameController.CurrentlyPlayingPlayer.PickCardOnBoard(m_GameController.Board, out m_ContinueGame);
                    if (m_ContinueGame)
                    {
                        didMatch = m_GameController.FlipCardOnBoard(o_Card2Cords);
                    }
                }
            }

            return didMatch;
        }
    }
}
