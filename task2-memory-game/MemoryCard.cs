namespace task2_memory_game
{
    public struct MemoryCard
    {
        public int PairNum { get; set; }
        public bool IsSeen { get; private set; } 
        
        public MemoryCard(int i_NumberOfPair)
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
