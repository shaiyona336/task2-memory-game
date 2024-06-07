

using System;
using System.Runtime.InteropServices;

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
        private UIOfMomoryGame UI;
        private LogicMemoryGame logicMemoryGame;
        private const int minimumRowSize = 4;
        private const int minimumColumnSize = 4;
        private const int maximumRowSize = 6;
        private const int maximumColumnSize = 6;

        public void game()
        {
            UI = new UIOfMomoryGame();
            logicMemoryGame = new LogicMemoryGame();
            string currentCardToOpen;
           
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
                secondPlayer = new HumanPlayerMemoryGame(secondPlayerName);
            }
            //need to get board dimensions here
            /////////////////
            logicMemoryGame.setBoard(4, 6);
            UI.printBoard(4, 6, logicMemoryGame.getBoard());

            currentCardToOpen = UI.askUserForCardToOpen();
            while (currentCardToOpen != "Q")
            {


            }
            


            Console.ReadLine();

        }

    }
}
