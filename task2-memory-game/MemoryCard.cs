using System;


namespace task2_memory_game
{
    public struct MemoryCard
    {
        public int PairNum { get; set; }
        public bool IsSeen { get; set; } 
        
        public MemoryCard(int i_numberOfPair)
        {
            PairNum = i_numberOfPair;
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
