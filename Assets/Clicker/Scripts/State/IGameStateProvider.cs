using R3;

namespace Assets.Clicker.Scripts.State
{
    public interface IGameStateProvider
    {
        public GameStateProxy GameState { get; }

        public Observable<GameStateProxy> LoadGameState();

        public Observable<bool> SaveGameState();

        public Observable<bool> ResetGameState();
    }
}
