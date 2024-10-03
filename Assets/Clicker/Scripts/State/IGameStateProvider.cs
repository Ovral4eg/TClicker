using Assets.Project.Scripts.Game.State;

namespace Assets.Clicker.Scripts.State
{
    public interface IGameStateProvider
    {
        public GameState GameState { get; }

        public GameState LoadGameState();

        public bool SaveGameState();

        public bool ResetGameState();

    }
}
