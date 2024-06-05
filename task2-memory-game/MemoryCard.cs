using System;


namespace task2_memory_game
{
    public struct MemoryCard
    {
        private int numberOfPair;
        private bool isSeen;
        

        public MemoryCard(int numberOfPair)
        {
            this.numberOfPair = numberOfPair;
            isSeen = false;
        }


        public bool getIsSeen()
        {
            return isSeen;
        }
        public int getNumberOfPair()
        {
            return numberOfPair;
        }
    }
}
