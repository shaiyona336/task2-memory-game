

using System;
using System.Diagnostics.PerformanceData;

namespace task2_memory_game
{
    internal class Program
    {
        public static void Main()
        {
            Interface game = new Interface();
            game.game();
        }
    }
}
