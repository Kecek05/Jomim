using KeceK.Game;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class PauseCanvas : MonoBehaviour
    {
        [Title("References")] 
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _exitButton;
        
        private void Awake()
        {
            Hide();
            SetupButtons();
        }

        private void OnEnable()
        {
            PauseManager.OnPauseStateChanged += PauseManagerOnOnPauseStateChanged;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseStateChanged -= PauseManagerOnOnPauseStateChanged;
        }

        private void PauseManagerOnOnPauseStateChanged(bool isPaused)
        {
            if(isPaused)
                Show();
            else
                Hide();
        }

        private void SetupButtons()
        {
            _resumeButton.onClick.AddListener(() =>
            {
                PauseManager.UnPauseGame();
                Hide();
            });
            
            _restartButton.onClick.AddListener(() =>
            {
                PauseManager.UnPauseGame();
                PauseManager.SetCanChangePauseState(false);
                Loader.ReloadCurrentScene();
            });
            
            _mainMenuButton.onClick.AddListener(() =>
            {
                PauseManager.UnPauseGame();
                PauseManager.SetCanChangePauseState(false);
                Loader.Load(Loader.Scene.MainMenu);
            });
            
            _exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        private void Hide()
        {
            _pausePanel.SetActive(false);
        }
        
        private void Show()
        {
            _pausePanel.SetActive(true);
        }
    }
}
