using Assets.Project.Scripts.Game.State;
using UnityEngine;

namespace Assets.Clicker.Scripts.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        //const
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);

        //properties
        public GameState GameState { get; private set; }

        //public methods
        public GameState LoadGameState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY) == false )
            {
                GameState = CreateGameStateFromSettings();

                Debug.Log($"create default game state");

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                GameState = JsonUtility.FromJson<GameState>(json);
            }

            return GameState;
        }

        public bool SaveGameState()
        {
            var json = JsonUtility.ToJson(GameState, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return true;
        }

        public bool ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();

            return true;
        }       

        //private methods
        private GameState CreateGameStateFromSettings()
        {
            var gameState = new GameState()
            {
                softCoins = 0
            };

            return gameState;
        }
    }
}
