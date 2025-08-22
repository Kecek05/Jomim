using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LevelSelectorCanvas : MonoBehaviour
    {
        [Title("References")] 
        [SerializeField] private GameObject _levelSelectorParent;
        [Title("Buttons")]
        [SerializeField] private Button _level1Button;
        [SerializeField] private Button _level2Button;
        [Space(10f)]
        [SerializeField] private Button _backButton;
        
        private void Awake()
        {
            Hide();
            
            SetupButtons();
        }

        private void SetupButtons()
        {
            _level1Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level1);
            });
            
            _level2Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level2);
            });
            
            _backButton.onClick.AddListener(() =>
            {
                Hide();
            });
        }
        
        private void SelectLevel(Loader.Scene scene)
        {
            Loader.Load(scene);
        }

        public void Show()
        {
            _levelSelectorParent.SetActive(true);
        }

        public void Hide()
        {
            _levelSelectorParent.SetActive(false);
        }
    }
}
