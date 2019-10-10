using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

// Minimax algorithm examples:
// https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/
// https://ide.geeksforgeeks.org/KTAN36

// A working minimax tic-tac-toe game used to checkout outcomes against
// https://blog.vivekpanyam.com/how-to-build-an-ai-that-wins-the-basics-of-minimax-search/
namespace tic_tac_toe
{
    class Minimax
    {
        public static int Runs = 0;

        public static Position GetBestMove(Board board)
        {
            int bestValue = int.MinValue;
            Position bestMove = new Position(-1, -1);

            for (int row = 0; row < board.BOARD_DIMENSION; row++)
            {
                for (int column = 0; column < board.BOARD_DIMENSION; column++)
                {
                    if (board.IsSpaceEmpty(row, column))
                    {
                        board.UpdateBoard(row, column, (State)Players.PlayerTwo);
                        int moveValue = Minimax.MinimaxCheck(board, 1, false);
                        board.UpdateBoard(row, column, State.Empty);
                        if (moveValue > bestValue)
                        {
                            bestMove = new Position(row, column);
                            bestValue = moveValue;
                        }
                    }
                }
            }

            return bestMove;
        }

        public static int MinimaxCheck(Board board, int depth, bool maxComputer)
        {
            //Thread.Sleep(1000);

            int score = Minimax.GetWin(board);

            // if there is a win or loss
            if (score == 10 || score == -10)
            {
                return score - depth;
            }

            // if the game is over without a winner (draw)
            if (board.HasEmptySpaces() == false)
            {
                return 0 - depth;
            }

            // runs if it's the computer's turn
            if (maxComputer)
            {
                int currentBest = int.MinValue;

                for (int row = 0; row < board.BOARD_DIMENSION; row++)
                {
                    for (int column = 0; column < board.BOARD_DIMENSION; column++)
                    {
                        if (board.IsSpaceEmpty(row, column))
                        {
                            board.UpdateBoard(row, column, (State)Players.PlayerTwo);
                            currentBest = Math.Max(currentBest, Minimax.MinimaxCheck(board, depth + 1, !maxComputer));
                            board.UpdateBoard(row, column, State.Empty);
                        }

                    }
                }

                return currentBest;
            } 
            else // runs if it's the player's turn
            {
                int currentBest = int.MaxValue;

                for (int row = 0; row < board.BOARD_DIMENSION; row++)
                {
                    for (int column = 0; column < board.BOARD_DIMENSION; column++)
                    {
                        if (board.IsSpaceEmpty(row, column))
                        {
                            board.UpdateBoard(row, column, (State)Players.PlayerOne);
                            currentBest = Math.Min(currentBest, Minimax.MinimaxCheck(board, depth + 1, !maxComputer));
                            board.UpdateBoard(row, column, State.Empty);
                        }

                    }
                }

                return currentBest;
            }
        }

        public static int GetWin(Board board)
        {
            if (board.IsTheWinner(Players.PlayerTwo))
            {
                return 10;
            }
            else if (board.IsTheWinner(Players.PlayerOne))
            {
                return -10;
            }
            else
            {
                return 0;
            }
        }
    }
}
