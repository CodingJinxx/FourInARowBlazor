using System;
using System.Runtime.CompilerServices;
using Fluxor;
using FourInARow.Game;

namespace FourInARow.Store.GameUseCase
{
    public static class Reducers 
    {
        [ReducerMethod]
        public static GameState ReduceResetAction(GameState state, ResetGameAction action)
        {
            return new GameState(state.Board.GetLength(0), state.Board.GetLength(1));
        }
        
        [ReducerMethod]
        public static GameState ReducePlayAction(GameState state, PlayGameAction action)
        {
            EFieldType[,] board = (EFieldType[,])state.Board.Clone();
            EFieldType currentPlayer = state.CurrentPlayer;
            for (int y = 0, h = board.GetLength(1); y < h; y++)
            {
                if (board[action.Column, y] == EFieldType.UNKNOWN)
                {
                    board[action.Column, y] = currentPlayer;
                    currentPlayer = currentPlayer == EFieldType.RED ? EFieldType.BLUE : EFieldType.RED;

                    return state with
                    {
                        Board = board,
                        CurrentPlayer = currentPlayer,
                        Winner = CheckBoardForWinner(board)
                    };
                }
            }
            throw new ArgumentException("Column already filled");
        }
        
        private static EFieldType CheckBoardForWinner(EFieldType[,] board)
        {
            var winner = EFieldType.UNKNOWN;
            // Vertical
            for (int x = 0; x < board.GetLength(0); x++)
            {
                int count = 1;
                EFieldType prevEFieldType = board[x, 0];
                if(prevEFieldType == EFieldType.UNKNOWN) continue; 
                for (int y = 1; y < board.GetLength(1); y++)
                {
                    if (prevEFieldType == board[x, y])
                    {
                        count++;
                    }
                    else
                    {
                        count = 1;
                        prevEFieldType = board[x, y];
                        if(prevEFieldType == EFieldType.UNKNOWN) break; 
                    }

                    if (count >= 4)
                    {
                        winner = prevEFieldType;
                    }
                }
            }
            // Horizontal
            for (int y = 0; y < board.GetLength(1); y++)
            {
                int count = 1;
                EFieldType prevEFieldType = board[0, y];
                for (int x = 1; x < board.GetLength(0); x++)
                {
                    if (prevEFieldType == EFieldType.UNKNOWN)
                    {
                        prevEFieldType = board[x, y];
                        count = 1;
                        continue;
                    }
                    if (prevEFieldType == board[x, y])
                    {
                        count++;
                    }
                    else
                    {
                        count = 1;
                        prevEFieldType = board[x, y];
                        if(prevEFieldType == EFieldType.UNKNOWN) continue; 
                    }

                    if (count >= 4)
                    {
                        winner = prevEFieldType;
                    }
                }
            }

            // Diagonal l -> r
            {
                EFieldType DiagonalChecker(int X, int Y)
                {
                    Console.WriteLine($"Diag: {X} {Y}");
                    EFieldType Winner = EFieldType.UNKNOWN;
                    EFieldType prevCell = board[X, Y];
                    int count = 1;
                    while (Y > 0 && X < board.GetLength(0) - 1)
                    {
                        if (prevCell == EFieldType.UNKNOWN)
                        {
                            X++;
                            Y--;

                            prevCell = board[X, Y];
                            count = 1;
                        }
                        else
                        {
                            X++;
                            Y--;

                            if (prevCell == board[X, Y])
                            {
                                count++;
                            }
                            else
                            {
                                count = 1;
                            }
                        }

                        if (count >= 4)
                        {
                            Winner = prevCell;
                        }
                    }

                    return Winner;
                }

                int X = 0;
                int Y = 0;
                for (int i = 0; i < board.GetLength(1) + board.GetLength(0) - 1; i++)
                {
                    var Winner = DiagonalChecker(X, Y);
                    if (Winner != EFieldType.UNKNOWN)
                    {
                        winner = Winner;
                    }
                    else
                    {
                        if (Y < board.GetLength(1) - 1)
                        {
                            Y++;
                        }
                        else if (X < board.GetLength(0) - 1)
                        {
                            X++;
                        }
                    }
                }
            }

            // Diagonal r - l
            {
                EFieldType DiagonalChecker(int X, int Y)
                {
                    Console.WriteLine($"Diag: {X} {Y}");
                    EFieldType Winner = EFieldType.UNKNOWN;
                    EFieldType prevCell = board[X, Y];
                    int count = 1;
                    while (Y > 0 && X > 0)
                    {
                        if (prevCell == EFieldType.UNKNOWN)
                        {
                            X--;
                            Y--;

                            prevCell = board[X, Y];
                            count = 1;
                        }
                        else
                        {
                            X--;
                            Y--;

                            if (prevCell == board[X, Y])
                            {
                                count++;
                            }
                            else
                            {
                                count = 1;
                            }
                        }

                        if (count >= 4)
                        {
                            Winner = prevCell;
                        }
                    }

                    return Winner;
                }
                
                int X = board.GetLength(0) - 1;
                int Y = 0;
                for (int i = 0; i < board.GetLength(1) + board.GetLength(0) - 1; i++)
                {
                    var Winner = DiagonalChecker(X, Y);
                    if (Winner != EFieldType.UNKNOWN)
                    {
                        winner = Winner;
                    }
                    else
                    {
                        if (Y < board.GetLength(1) - 1)
                        {
                            Y++;
                        }
                        else if (X >= 0)
                        {
                            X--;
                        }
                    }
                }
            }
            
            
            return winner;
        }
    }
}