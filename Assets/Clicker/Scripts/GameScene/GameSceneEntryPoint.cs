using Assets.Clicker.Scripts.State;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene
{
    public class GameSceneEntryPoint:MonoBehaviour
    {
        private IGameStateProvider _gameState;
        public void Run(IGameStateProvider gameState)
        {

        }
    }
}
