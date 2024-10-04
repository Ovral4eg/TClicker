using Assets.Clicker.Scripts.GameScene;
using Assets.Clicker.Scripts.State;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using R3;
using Assets.Clicker.Scripts.Utils;

namespace Assets.Clicker.Scripts.Root
{
    internal class EntryPoint
    {
        private static EntryPoint _instance;
        private Coroutines _coroutines;
        private UiRootView _uiRoot;
        private IGameStateProvider _gameStateProvider;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            //опции приложения
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.runInBackground = true;

            _instance = new EntryPoint();
            _instance.Run();
        }

        private EntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUiRoot = Resources.Load<UiRootView>("UiRoot");
            _uiRoot = Object.Instantiate(prefabUiRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);

            //выбрать способ загрузки
            _gameStateProvider = new PlayerPrefsGameStateProvider();
        }

        private void Run()
        {
            StringHelper.CalculateLetters();

            _coroutines.StartCoroutine(LoadAndStartGame());
        }

        private IEnumerator LoadAndStartGame()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);

            yield return LoadScene(Scenes.GAME);

            var isGameStateLoaded = false;
            _gameStateProvider.LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameSceneEntryPoint>();
            sceneEntryPoint.Run(_gameStateProvider, _uiRoot);

            _uiRoot.HideLoadingScreen();
        }      

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}