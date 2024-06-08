namespace task2_memory_game
{
    internal class HumanPlayerMemoryGame : PlayerMemoryGame
    {
        public string PlayerName { get; }

        public HumanPlayerMemoryGame(string i_namePlayer) 
        {
            PlayerName = i_namePlayer;
        }

        //public override ((int, int), (int, int)) PickTwoCardsOnBoard(BoardMemoryGame board)
        //{

        //}

    }
}
