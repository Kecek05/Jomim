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
        
        [Title("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        
        private void Awake()
        {
            Show();
            
            SetupButtons();
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
