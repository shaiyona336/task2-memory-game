﻿using System;

namespace MemoryGameLogic
{
    class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        private Random m_RandomNumGenerator;
        public const string k_ComputerName = "Computer";

        public ComputerPlayerMemoryGame() : base()
        {
            Name = k_ComputerName;
            m_RandomNumGenerator = new Random();
        }

        public override MemoryGameCardCords PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame)
        {
            o_ContinueGame = true;
            MemoryGameCardCords pair1 = generateRandomCardCords(i_Board);

            while (!i_Board.IsCardHidden(pair1))
            {
                pair1 = generateRandomCardCords(i_Board);
            }

            return pair1;
        }

        private MemoryGameCardCords generateRandomCardCords(BoardMemoryGame i_Board)
        {
            int row = m_RandomNumGenerator.Next(i_Board.BoardHeight);
            int col = m_RandomNumGenerator.Next(i_Board.BoardWidth);

            return (row, col);
        }
    }
}