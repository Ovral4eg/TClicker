using Assets.Project.Scripts.Game.State;
using R3;

namespace Assets.Clicker.Scripts.State
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public ReactiveProperty<double> SoftCoins { get; }
        public ReactiveProperty<float> Energy { get; }

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;

            SoftCoins = new ReactiveProperty<double>(gameState.SoftCoins);
            SoftCoins.Skip(1).Subscribe(value => gameState.SoftCoins = value);

            Energy = new ReactiveProperty<float>(gameState.Energy);
            Energy.Skip(1).Subscribe(value => gameState.Energy = value);
        }
    }
}
