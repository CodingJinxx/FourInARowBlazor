using System;
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
                int maxDiag = Math.Max(board.GetLength(0), board.GetLength(1));
            }
            
            
            return winner;
        }
    }
}