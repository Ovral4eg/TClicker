﻿using Assets.Project.Scripts.Game.State;
using R3;
using UnityEngine;

namespace Assets.Clicker.Scripts.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        //const
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);

        //fields
        private GameState _gameStateOrigin;

        //properties
        public GameStateProxy GameState { get; private set; }

        //public methods
        public Observable<GameStateProxy> LoadGameState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY) == false )
            {
                GameState = CreateGameStateFromSettings();

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);
            }

            return Observable.Return(GameState); 
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();

            return Observable.Return(true);
        }       

        //private methods
        private GameStateProxy CreateGameStateFromSettings()
        {
            _gameStateOrigin = new GameState()
            {
                SoftCoins = 0,
                Energy=10,
            };

            return new GameStateProxy(_gameStateOrigin);
        }
    }
}
