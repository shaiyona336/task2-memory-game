

namespace task2_memory_game
{
    internal class Interface
    {
        string firstPlayerName;
        string secondPlayerName;
        private bool isFirstPlayerTurn;
        private bool computerIsPlaying;
        HumanPlayerMemoryGame firstPlayer;
        HumanPlayerMemoryGame secondPlayer;
        ComputerPlayerMemoryGame computer;
        UIOfMomoryGame UI;
        LogicMemoryGame logicMemoryGame;
        private const int minimumRowSize = 4;
        private const int minimumColumnSize = 4;
        private const int maximumRowSize = 6;
        private const int maximumColumnSize = 6;

        void game()
        {
            bool againstComputer;
            firstPlayerName = UI.getUsername();
            firstPlayer = new HumanPlayerMemoryGame(firstPlayerName);
            againstComputer = UI.againstHumanOrComputer();
            if (againstComputer)
            {
                computerIsPlaying = true;
            }
            else
            {
                computerIsPlaying = false;
                secondPlayerName = UI.getUsername();
                secondPlayer = new HumanPlayerMemoryGame(firstPlayerName);
            }

        }

    }
}
