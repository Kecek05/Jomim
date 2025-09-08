using System;
using System.Collections;
using System.Collections.Generic;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class MainMenuCanvas : MonoBehaviour
    {
        [Title("References")] [SerializeField] 
        private GameObject _mainMenuParent;
        [SerializeField] private LevelSelectorCanvas _levelSelectorCanvas;
        [SerializeField] private Image _localizationButtonSprite;
        [SerializeField] private Sprite _englishSprite;
        [SerializeField] private Sprite _brazilSprite;
        
        [Title("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _localizationButton;
        
        private bool _isEnglish = true;
        
        private void Awake()
        {
            Show();
            
            SetupButtons();
        }

        private void Start()
        {
            _isEnglish = Saver.GetSavedLocalizationIndex() == 0;
            ChangeLocalizationButtonVisual();
        }

        private void SetupButtons()
        {
            _playButton.onClick.AddListener(() =>
            {
                _levelSelectorCanvas.Show();
            });
            
            _exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
            
            _localizationButton.onClick.AddListener(() =>
            {
                _isEnglish = !_isEnglish;
                int localizationIndex = _isEnglish ? 0 : 1;
                ChangeLocalizationButtonVisual();
                _localizationButton.interactable = false;
                StartCoroutine(LocalizationManager.SetLocalizationByIndex(localizationIndex, () =>
                {
                    _localizationButton.interactable = true;
                }));
            });
        }

        private void ChangeLocalizationButtonVisual()
        {
            _localizationButtonSprite.sprite = _isEnglish ? _englishSprite : _brazilSprite;
        }
        
        public void Show()
        {
            _mainMenuParent.SetActive(true);
        }
        
        public void Hide()
        {
            _mainMenuParent.SetActive(false);
        }
    }
}
