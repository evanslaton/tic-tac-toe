using System;

namespace tic_tac_toe
{
    public enum Players { PlayerOne = 1, PlayerTwo };
    class Game
    {
        internal static int TURNS = 9;
        private Board Board { get; }
        private Players ActivePlayer { get; set; }
        private int TurnsTaken { get; set; }

        public Game()
        {
            Board = new Board();
            ActivePlayer = Players.PlayerOne;
            TurnsTaken = 0;
        }

        public static void PlayGame()
        {
            Game currentGame = new Game();
            State state;

            while(currentGame.TurnsTaken < Game.TURNS)
            {
                state = currentGame.UpdateGame(currentGame.TurnsTaken);
                if (state == (State)currentGame.ActivePlayer) break;
                currentGame.ChangeActivePlayer();
                currentGame.TurnsTaken++;
            }

            currentGame.PrintWinner(currentGame.TurnsTaken);

        }

        private State UpdateGame(int turnsTaken)
        {
            Board.UpdateBoard(GetUserInput(), ActivePlayer);

            return Board.IsTheWinner(ActivePlayer, turnsTaken);
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

        private void PrintWinner(int turnsTaken)
        {
            if (Board.IsTheWinner(Players.PlayerOne, turnsTaken) == State.O)
            {
                Console.WriteLine($"\n{Players.PlayerOne} is the winner!");
            }
            else if (Board.IsTheWinner(Players.PlayerTwo, turnsTaken) == State.X)
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
