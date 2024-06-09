using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2_memory_game
{
    public abstract class PlayerMemoryGame
    {
        public int Score { get; private set; }

        public PlayerMemoryGame()
        {
            Score = 0;
        }

        public void addPoint()
        {
            Score++;
        }

        public abstract (int, int) PickCardOnBoard(BoardMemoryGame board, out bool continueGame);
    }
}
