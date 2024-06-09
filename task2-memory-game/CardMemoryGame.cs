namespace MemoryGameLogic
{
    public struct CardMemoryGame
    {
        public int PairNum { get; set; }
        public bool IsSeen { get; private set; } 
        
        public CardMemoryGame(int i_NumberOfPair)
        {
            PairNum = i_NumberOfPair;
            IsSeen = false;
        }

        public void RevealCard()
        {
            IsSeen = true;
        }

        public void HideCard()
        {
            IsSeen = false;
        }
    }
}
