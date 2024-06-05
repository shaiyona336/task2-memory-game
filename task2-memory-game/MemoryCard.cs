using System;


namespace task2_memory_game
{
    public struct MemoryCard
    {
        private int m_numberOfPair;
        private bool m_isSeen;
        

        public MemoryCard(int i_numberOfPair)
        {
            m_numberOfPair = i_numberOfPair;
            m_isSeen = false;
        }


        public bool getIsSeen()
        {
            return m_isSeen;
        }
        public int getNumberOfPair()
        {
            return m_numberOfPair;
        }
        public void setNumberOfPair(int i_numberOfPair)
        {
            m_numberOfPair = i_numberOfPair;
        }
    }
}
