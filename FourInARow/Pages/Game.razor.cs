using System;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components;
using FourInARow.Store.GameUseCase;

namespace FourInARow.Pages
{
    public partial class Game
    {
        [Inject]
        private IState<GameState> GameState { get; set; }
        
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private void PlayAction(int column)
        {
            Dispatcher.Dispatch(new PlayGameAction(column));
        }

        private void ResetAction()
        {
            Dispatcher.Dispatch(new ResetGameAction());
        }
    }
}