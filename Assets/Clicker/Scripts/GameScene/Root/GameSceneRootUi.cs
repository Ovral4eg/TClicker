using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class GameSceneRootUi:MonoBehaviour
    {
        [SerializeField] private Transform _windowsContainer;
        public void AttachSceneUi(GameObject sceneUi)
        {
            sceneUi.transform.SetParent(_windowsContainer, false);
        }
    }
}
