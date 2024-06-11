namespace MemoryGameLogic
{
    internal class GameController
    {
        private enum ePlayerTurn
        {
            Player1Turn,
            Player2Turn
        }

        private PlayerMemoryGame m_Player1;
        private PlayerMemoryGame m_Player2; 
        public PlayerMemoryGame CurrentlyPlayingPlayer { get; private set; }
        public BoardMemoryGame Board { get; }
        private ePlayerTurn m_CurrentTurn = ePlayerTurn.Player1Turn;

        public GameController(PlayerMemoryGame i_Player1, PlayerMemoryGame i_Player2, BoardMemoryGame i_Board)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            Board = i_Board;

            CurrentlyPlayingPlayer = m_Player1;
            m_CurrentTurn = ePlayerTurn.Player1Turn;
        }

        public void switchTurn() 
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

        public MemoryGameCardCords PlayCurrentTurn(MemoryGameCardCords i_CurrentlyPlayingCard, out bool o_DidCardsMatch,
            out bool o_DidGameOver)
        {
            o_DidGameOver = false;
            //o_DidCardsMatch = false;
            bool wasThereARevealedCardBeforeCurrentFlip = Board.IsThereARevealedCard;

            o_DidCardsMatch = Board.FlipCardOnBoard(i_CurrentlyPlayingCard);
            if (o_DidCardsMatch)
            {
                GivePointToCurrentlyPlayingPlayer();
                o_DidGameOver = IsGameOver();
            }
            else if (wasThereARevealedCardBeforeCurrentFlip)
            {
                switchTurn();
            }

            return i_CurrentlyPlayingCard; //Will not need if I choose this
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
            if (m_Player2 is ComputerPlayerMemoryGame)
            {
                ((ComputerPlayerMemoryGame)m_Player2).DidNewGameStarted = true;
            }
        }
    }
}
