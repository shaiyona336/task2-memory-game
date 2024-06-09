using System;
using System.Collections.Generic;

namespace MemoryGameLogic
{
    public class BoardMemoryGame
    {
        public int BoardHeight { get; private set; }
        public int BoardWidth { get; private set; }
        public CardMemoryGame[,] BoardState { get; private set; }
        public bool IsThereARevealedCard { get; private set; }

        private MemoryGameCardCords m_currentlyOpenedCardCords;

        public BoardMemoryGame(int i_BoardHeight, int i_BoardWidth) : this((i_BoardHeight, i_BoardWidth))
        {
        }

        public BoardMemoryGame((int,int) i_BoardDimensions)
        {
            SetEmptyBoard(i_BoardDimensions);
        }

        public void SetEmptyBoard((int, int) i_BoardDimensions)
        {
            BoardHeight = i_BoardDimensions.Item1;
            BoardWidth = i_BoardDimensions.Item2;
            BoardState = new CardMemoryGame[BoardHeight, BoardWidth];
            FillBoardWithNewPairs();
            IsThereARevealedCard = false;
        }

        public void RevealCardsOnBoard(MemoryGameCardCords i_FirstCardCords, MemoryGameCardCords i_SecondCardCords)
        {
            BoardState[i_FirstCardCords.Y, i_FirstCardCords.X].RevealCard();
            BoardState[i_SecondCardCords.Y, i_SecondCardCords.X].RevealCard();
        }

        public void HideCards(MemoryGameCardCords i_FirstCardCords, MemoryGameCardCords i_SecondCardCords)
        {
            BoardState[i_FirstCardCords.Y, i_FirstCardCords.X].HideCard();
            BoardState[i_SecondCardCords.Y, i_SecondCardCords.X].HideCard();
        }

        public void FillBoardWithNewPairs()
        {
            int numberOfPairs = (BoardHeight * BoardWidth) / 2; //size of the board devide by two
            insertPairsToList(numberOfPairs, out List<CardMemoryGame> pairsToShuffle);//save all the cards in a list, shuffle them, and put them in the board
            shufflePairs(pairsToShuffle);

            //put the cards on the board
            for (int row = 0; row < BoardHeight; row++)
            {
                for (int column = 0; column < BoardWidth; column++)
                {
                    BoardState[row, column] = pairsToShuffle[row * BoardWidth + column];
                }
            }
        }

        private void insertPairsToList(int i_NumberOfPairs, out List<CardMemoryGame> o_PairsList)
        {
            o_PairsList = new List<CardMemoryGame>(i_NumberOfPairs * 2);

            for (int pair = 1; pair <= i_NumberOfPairs; pair++)
            {
                CardMemoryGame card1 = new CardMemoryGame(pair);
                CardMemoryGame card2 = new CardMemoryGame(pair);
                o_PairsList.Add(card1);
                o_PairsList.Add(card2);
            }
        }

        private void shufflePairs(List<CardMemoryGame> io_List)
        {
            Random random = new Random();
            int whoToSwitchWith;

            for (int card = 0; card < io_List.Count; card++)
            {
                whoToSwitchWith = random.Next(io_List.Count);
                switchCardsInList(io_List, card, whoToSwitchWith);
            }
        }

        private void switchCardsInList(List<CardMemoryGame> io_List, int i_Card1Index, int i_Card2Index)
        {
            CardMemoryGame tempCard = io_List[i_Card1Index];
            io_List[i_Card1Index] = io_List[i_Card2Index];
            io_List[i_Card2Index] = tempCard;
        }

        public bool FlipCardOnBoard(MemoryGameCardCords i_CardCords) //return if flipped a pair
        {
            bool isFlippedAPair = false;

            if (IsThereARevealedCard == true)
            {
                ref CardMemoryGame currentlyRevealedCard = ref BoardState[m_currentlyOpenedCardCords.Y, m_currentlyOpenedCardCords.X];
                if (BoardState[i_CardCords.Y, i_CardCords.X].PairNum == currentlyRevealedCard.PairNum)
                {
                    isFlippedAPair = true;
                    BoardState[i_CardCords.Y, i_CardCords.X].RevealCard(); // need to flip this card and keep his state like that
                }
                else //if the card flipped wasnt a pair
                {
                    isFlippedAPair = false;
                    currentlyRevealedCard.HideCard(); // need to set the old card that been flipped to not seen again
                }
            }
            else //if (UserOpenedOneCard == false), then need to set the card that is now opened in the middle of a turn
            {
                m_currentlyOpenedCardCords = (i_CardCords.Y, i_CardCords.X);
                BoardState[m_currentlyOpenedCardCords.Y, m_currentlyOpenedCardCords.X].RevealCard();
            }

            IsThereARevealedCard = !IsThereARevealedCard; //if user open card, now need to flip the condition of one card open in a turn
            return isFlippedAPair;
        }

        public bool IsCardHidden(MemoryGameCardCords i_CardCords)
        {
            bool returnValue = false;

            if (IsCardOnGameBoard(i_CardCords) && !BoardState[i_CardCords.Y, i_CardCords.X].IsSeen)
            {
                returnValue = true;
            }

            return returnValue;
        }

        public void PrintBoard()
        {
            MemoryGameUI.MemoryGameInputManager.PrintBoard(this);
        }

        public bool IsBoardFullyRevealed()
        {
            bool returnValue = true;
            for (int boardXDimension = 0; boardXDimension < BoardWidth && returnValue; boardXDimension++)
            {
                for (int boardYDimension = 0; boardYDimension < BoardHeight && returnValue; boardYDimension++)
                {
                    if (!BoardState[boardXDimension, boardYDimension].IsSeen)
                    {
                        returnValue = false;
                        break;
                    }
                }

            }
            return returnValue;
        }

        public bool IsCardOnGameBoard(MemoryGameCardCords i_CardCords)
        {
            bool returnValue = false;

            if (i_CardCords.Y < BoardHeight && i_CardCords.Y >= 0 && i_CardCords.X < BoardWidth && i_CardCords.X >= 0)
            {
                returnValue = true;
            }
            
            return returnValue;
        }


    }
}
