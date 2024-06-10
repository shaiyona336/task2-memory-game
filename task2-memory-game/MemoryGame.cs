using System.Threading;
using MemoryGameLogic;

namespace MemoryGameUI
{
    public class MemoryGame
    {
        private GameController m_GameController;
        private bool m_ContinueGame = true; 

        private const int k_TimeToFreezeGameInMilliseconds = 2000; //2000 miliseconds = 2 seconds  
        private const bool v_CardsMatched = true;

        public MemoryGame() 
        {
            string player1Name = MemoryGameInputManager.GetUsername();
            HumanPlayerMemoryGame player1 = new HumanPlayerMemoryGame(player1Name);

            PlayerMemoryGame player2;
            bool isComputerPlaying = MemoryGameInputManager.AskUserIfPlayingAgainstComputer();
            if (isComputerPlaying)
            {
                player2 = new ComputerPlayerMemoryGame();
            }
            else
            {
                string player2Name = MemoryGameInputManager.GetUsername();
                player2 = new HumanPlayerMemoryGame(player2Name);
            }

            BoardMemoryGame gameBoard = setUpNewBoard();
            m_GameController = new GameController(player1, player2, gameBoard);
        }

        public void RunMemoryGame() 
        {
            MemoryGameCardCords currentPlayingCard;
            MemoryGameCardCords? previousPlayedCard = null;

            //while user didnt typed 'Q'
            while (m_ContinueGame)
            {
                printGameBoard();
                currentPlayingCard = m_GameController.PlayCurrentTurn(out bool didCardsMatch, out m_ContinueGame, out bool isGameOver);
                if (!m_ContinueGame)
                {
                    break;
                }

                if (previousPlayedCard != null)
                {
                    if (didCardsMatch != v_CardsMatched)
                    {
                        revealCardsForTwoSeconds(currentPlayingCard, (MemoryGameCardCords)previousPlayedCard);
                    }
                    else if (isGameOver)
                    {
                        printGameBoard();
                        startNewGameIfRequested();
                    }
                    previousPlayedCard = null;
                }
                else
                {
                    previousPlayedCard = currentPlayingCard;
                }
            }
        }

        private (int,int) getBoardSizeFromUser()
        {
            return MemoryGameInputManager.GetBoardSizeFromUser();
        }

        private BoardMemoryGame setUpNewBoard() 
        {
            (int, int) boardDimensions = getBoardSizeFromUser();
            while (!BoardMemoryGame.AreBoardDimensionsLegal(boardDimensions))
            {
                MemoryGameInputManager.PrintIllegalBoardSizeWarning();
                boardDimensions = getBoardSizeFromUser();
            }
            return new BoardMemoryGame(boardDimensions);
        }

        private void revealCardsForTwoSeconds(MemoryGameCardCords i_Card1Cords, MemoryGameCardCords i_Card2Cords) 
        {
            m_GameController.Board.RevealCardsOnBoard(i_Card1Cords, i_Card2Cords);
            printGameBoard();
            Thread.Sleep(k_TimeToFreezeGameInMilliseconds);
            m_GameController.Board.HideCardsOnBoard(i_Card1Cords, i_Card2Cords);
        }

        private void startNewGameIfRequested()
        {
            PlayerMemoryGame winnerPlayer;
            PlayerMemoryGame loserPlayer;
            bool didGameEndInATie;
            m_GameController.getWinnerAndLoser(out winnerPlayer, out loserPlayer, out didGameEndInATie);
            bool startNewGame = MemoryGameInputManager.PrintEndGameMessageAndAskForAnotherGame(winnerPlayer, loserPlayer, didGameEndInATie);
            if (startNewGame)
            {
                (int, int) boardDimensions = getBoardSizeFromUser();
                m_GameController.SetUpNewGame(boardDimensions);
            }
            else //if the user don't want to start a new game:
            {
                m_ContinueGame = false; //the same as if the user press Q in a middle of a game: quit
            }
        }

        private void printGameBoard()
        {
            MemoryGameInputManager.PrintBoard(m_GameController.Board);
        }
    }
}
