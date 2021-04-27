using System;
using Index = System.Index;

namespace FourInARow.Game
{
    public class GameBoard
    {
        private EFieldType[,] _state;
        private EFieldType _currentPlayer = EFieldType.BLUE;
        
        public int Width { get => _state.GetLength(0); }
        public int Height { get => _state.GetLength(1); }
        
        public EFieldType this[int x,int y]
        {
            get
            {
                return _state[x, y];
            }
        }

        // min 7x6
        public GameBoard(int width, int height)
        {
            _state = new EFieldType[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _state[x, y] = EFieldType.UNKNOWN;
                }
            }
        }

        public void Play(int col)
        {
            if (col < _state.GetLength(0))
            {
                throw new ArgumentOutOfRangeException();
            }

            int height = _state.GetLength(1);
            for (int y = height-1; y > 0; y--)
            {
                if (y == height - 1 && _state[col, y] != EFieldType.UNKNOWN)
                {
                    throw new ArgumentException("Column Full");
                }

                if (_state[col, y] == EFieldType.UNKNOWN && _state[col, y - 1] != EFieldType.UNKNOWN)
                {
                    _state[col, y] = _currentPlayer;
                    _currentPlayer = _currentPlayer == EFieldType.BLUE ? EFieldType.RED : EFieldType.BLUE;
                    break;
                }

                if (y == 1 && _state[col, y - 1] != EFieldType.UNKNOWN)
                {
                    _state[col, y - 1] = _currentPlayer;
                    _currentPlayer = _currentPlayer == EFieldType.BLUE ? EFieldType.RED : EFieldType.BLUE;
                }
            }
        }
    }
}