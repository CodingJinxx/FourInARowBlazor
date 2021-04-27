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
        
        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine($"{GameState.Value.Board.Width} {GameState.Value.Board.Height}");
        }
    }
}