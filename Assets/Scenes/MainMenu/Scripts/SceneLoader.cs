using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using MainMenu.Buttons;
using MainMenu.Save;


namespace MainMenu.Loader
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Slider _loadingProgress;
        [SerializeField] private LoadingScreen _loadingScreen;
        
        private const int MAIN_MENU_INDEX = 0;

        private AsyncOperation _loadingSceneOperation;

        public delegate void LaunchSceneHandler();
        public event LaunchSceneHandler OnSceneLoadEndEvent;

        public void LoadMainMenu()
        {
            LoadScene(MAIN_MENU_INDEX);
        }
        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnEnable()
        {
            if (_loadingScreen == null)
            {
                throw new Exception("LoadingScreen does not exist");
            }

            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        

        private void LoadScene(int index)
        {
            LoadSceneAsync(index);
        }
        private async void LoadSceneAsync(int index)
        {
            _loadingScreen.Activate();
            await Task.Delay(500);

            _loadingSceneOperation = SceneManager.LoadSceneAsync(index);
            _loadingSceneOperation.allowSceneActivation = false;

            while (_loadingSceneOperation.progress < 0.9f &&
                _loadingProgress.value < _loadingProgress.maxValue)
            {
                _loadingProgress.value = _loadingSceneOperation.progress * 100f;
                await Task.Delay(1);
            }

            OnSceneLoadEndEvent?.Invoke();
            _loadingSceneOperation.allowSceneActivation = true;

        }
        


        private void SubscribeOnEvents()
        {
            SelectionLevelButton.OnClickEvent += LoadScene;
        }
        private void UnsubscribeFromEvents()
        {
            SelectionLevelButton.OnClickEvent -= LoadScene;
        }

    }
}
