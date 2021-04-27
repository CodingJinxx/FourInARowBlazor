using System;
using FourInARow.Game;

namespace FourInARow.Store.GameUseCase
{
    public record GameState 
    {
        public EFieldType[,] Board { get; init; }
        public EFieldType CurrentPlayer { get; init; }
        public EFieldType Winner { get; init; }
        
        public GameState(int width, int height)
        {
            this.Board = new EFieldType[width, height];
            CurrentPlayer = EFieldType.RED;
            Winner = EFieldType.UNKNOWN;
        }
    }
}