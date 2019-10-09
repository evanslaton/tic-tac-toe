using System;

namespace tic_tac_toe
{
    public enum Players { PlayerOne = 1, PlayerTwo };
    class Game
    {
        private static int TURNS = 9;
        private Board Board { get; }
        private Players ActivePlayer { get; set; }
        private bool ContinuePlaying { get; set; }
        private int TurnsTaken { get; set; }

        public Game()
        {
            Board = new Board();
            ActivePlayer = Players.PlayerOne;
            ContinuePlaying = true;
            TurnsTaken = 0;
        }

        public static void PlayGame()
        {
            Game currentGame = new Game();

            while(currentGame.ContinuePlaying && currentGame.TurnsTaken < Game.TURNS)
            {
                currentGame.UpdateGame();
                currentGame.TurnsTaken++;
            }

            currentGame.PrintWinner();
        }

        private void UpdateGame()
        {
            Board.UpdateBoard(GetUserInput(), ActivePlayer);

            if (Board.IsTheWinner(ActivePlayer))
            {
                ContinuePlaying = false;
            }
            else
            {
                ChangeActivePlayer();
            }
        }

        private int GetUserInput()
        {
            string userInput;
            int userInputAsInt;
            bool isInputValid = false;

            do
            {
                this.Board.PrintBoard(this.ActivePlayer);
                Console.WriteLine($"\n{ActivePlayer}, Enter a number from 1-9");
                userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out userInputAsInt)
                    && (userInputAsInt > 0 && userInputAsInt < 10)
                    && Board.IsSpaceEmpty(userInputAsInt))
                {
                    isInputValid = true;
                }
            } while (!isInputValid);

            return userInputAsInt;
        }

        private void ChangeActivePlayer()
        {
            if (ActivePlayer == Players.PlayerOne)
            {
                ActivePlayer = Players.PlayerTwo;
            }
            else
            {
                ActivePlayer = Players.PlayerOne;
            }
        }

        private void PrintWinner()
        {
            if (Board.IsTheWinner(Players.PlayerOne))
            {
                Console.WriteLine($"\n{Players.PlayerOne} is the winner!");
            }
            else if (Board.IsTheWinner(Players.PlayerTwo))
            {
                Console.WriteLine($"\n{Players.PlayerTwo} is the winner!");
            }
            else
            {
                Console.WriteLine($"\nDraw!");
            }
        }
    }
}
