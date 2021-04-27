using FourInARow.Game;

namespace FourInARow.Store.GameUseCase
{
    public class GameState
    {
        public GameBoard Board { get; set; }

        public GameState()
        {
            this.Board = new GameBoard(10, 7);
        }
        
        public GameState(GameBoard state)
        {
            this.Board = state;
        }
    }
}