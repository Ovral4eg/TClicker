using Assets.Clicker.Scripts.GameScene.Game;
using Assets.Clicker.Scripts.GameScene.View;
using Assets.Clicker.Scripts.State;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene
{
    public class GameSceneEntryPoint:MonoBehaviour
    {
        [SerializeField] private GameSceneRootUi  _sceneUiRootPrefab;
        [SerializeField] private GameHandler _gameHandler;
        private IGameStateProvider _gameStateProvider;
        public void Run(IGameStateProvider gameStateProvider, UiRootView uiRoot )
        {
            _gameStateProvider = gameStateProvider;

            var sceneUi = Instantiate(_sceneUiRootPrefab);
            uiRoot.AttachSceneUi(sceneUi.gameObject);

            _gameHandler.Init(sceneUi, gameStateProvider.GameState);
        }

        private void OnApplicationQuit()
        {
            var result = _gameStateProvider.SaveGameState();
            Debug.Log($"save result {result}");
        }
    }

    
}
