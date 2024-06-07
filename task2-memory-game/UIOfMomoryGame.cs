﻿
using System;


namespace task2_memory_game
{
    internal class UIOfMomoryGame
    {
        public string getUsername()
        {
            string name;
            Console.WriteLine("Enter username: ");
            name = Console.ReadLine();
            return name;
        }

        public bool againstHumanOrComputer()
        {
            bool i_flag;

            System.Console.WriteLine("do you want to play against a computer: (y/n)");
            i_flag = getIfAnswerIsYes();
            return i_flag;
        }

        public bool getIfAnswerIsYes()
        {
            bool i_flag;
            string i_answer;
            i_answer = Console.ReadLine();
            if (i_answer == "y")
            {
                i_flag = true;
            }
            else
            {
                i_flag = false;
            }
            return i_flag;
        }

        public Tuple<int, int> getBoardSize(int minimumRowSize, int minimumColumnSize, int maximumRowSize, int maximumColumnSize)
        {
            Tuple<int, int> i_rowAndColumnSize;
            string i_rowInput;
            string i_columnInput;
            int i_row;
            int i_column;
            i_rowInput = Console.ReadLine();
            while (int.TryParse(i_rowInput, out i_row) && i_row >= minimumRowSize && i_row <= maximumRowSize)
            {
                i_rowInput = Console.ReadLine();
            }
            i_columnInput = Console.ReadLine();
            while (int.TryParse(i_columnInput, out i_column) && i_column >= minimumColumnSize && i_column <= maximumColumnSize)
            {
                i_columnInput = Console.ReadLine();
            }

            i_rowAndColumnSize = new Tuple<int, int>(i_row, i_column);

            return i_rowAndColumnSize;

        }



        public string askUserForCardToOpen()
        {
            string cardToOpen;
            Console.WriteLine("Pick a card to open: ");
            cardToOpen = Console.ReadLine();
            return cardToOpen;
        }

        public void printIllegalPlaceForCard()
        {
            Console.WriteLine("Illigal place for card");
        }

        public void printIllegalPlaceForCardBorder() {
            Console.WriteLine("card not in borders");
        }


        public bool endGameMessageAndAskIfAnotherGame(int amountOfPointsFirstPlayer, int amountOfPointsSecondPlayer)
        {
            bool i_flag;
            Console.WriteLine("THE GAME HAS ENDED");
            //print who won the game
            if (amountOfPointsFirstPlayer > amountOfPointsSecondPlayer)
            {
                Console.WriteLine("FIRST PLAYER WON!");
            }
            else //amountOfPointsFirstPlayer >= amountOfPointsSecondPlayer = second player won
            {
                if (amountOfPointsFirstPlayer > amountOfPointsSecondPlayer)
                {
                    Console.WriteLine("SECOND PLAYER WON!");
                }
            }
            Console.WriteLine("FIRST PLAYER POINTS: " + amountOfPointsFirstPlayer.ToString());
            Console.WriteLine("SECOND PLAYER POINTS: " + amountOfPointsSecondPlayer.ToString());
            //ask if the user want to start a new game
            Console.WriteLine("DO YOU WANT TO START A NEW GAME: (y/n)");
            
            i_flag = getIfAnswerIsYes();
            return i_flag;
        }
    }
}
