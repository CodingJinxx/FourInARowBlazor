using Fluxor;

namespace FourInARow.Store.GameUseCase
{
    public class Feature : Feature<GameState>
    {
        public override string GetName() => "GameState";

        protected override GameState GetInitialState()
        {
            return new GameState(10, 7);
        }
    }
}