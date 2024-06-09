namespace MemoryGameLogic
{
    public abstract class PlayerMemoryGame
    {
        public int Score { get; private set; }
        public string Name { get; protected set; }

        public PlayerMemoryGame()
        {
            Score = 0;
        }

        public void AddPoint()
        {
            Score++;
        }

        public abstract (int, int) PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame);
    }
}
