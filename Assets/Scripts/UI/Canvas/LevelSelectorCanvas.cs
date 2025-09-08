using System;
using System.Collections.Generic;
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
        [SerializeField] private Button _level3Button;
        [SerializeField] private Button _level4Button;
        [SerializeField] private Button _level5Button;
        [SerializeField] private Button _level6Button;
        [SerializeField] private Button _level7Button;
        [SerializeField] private Button _level8Button;
        [SerializeField] private Button _level9Button;
        [SerializeField] private List<GameObject> _levelsButtons;
        [Space(10f)]
        [SerializeField] private Button _backButton;
        
        private void Awake()
        {
            Hide();
            SetupButtons();
        }

        private void SetupButtons()
        {
            DisableAllButtons();
            ListenToButtonEvents();
            EnableButtonsBySave();
        }

        private void EnableButtonsBySave()
        {
            int levelsToShowButton = GetNextLevelIndex();
            for (int i = 0; i < levelsToShowButton; i++)
            {
                _levelsButtons[i].SetActive(true);
            }
        }

        private int GetNextLevelIndex()
        {
            return Saver.GetSavedLevelEnumIndex() + 1;
        }

        private void DisableAllButtons()
        {
            foreach (GameObject buttonObj in _levelsButtons)
            {
                buttonObj.SetActive(false);
            }
        }
        
        private void ListenToButtonEvents()
        {
            _level1Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level1);
            });
            
            _level2Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level2);
            });
            
            _level3Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level3);
            });
            
            _level4Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level4);
            });
            
            _level5Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level5);
            });
            
            _level6Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level6);
            });
            
            _level7Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level7);
            });
            
            _level8Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level8);
            });
            
            _level9Button.onClick.AddListener(() =>
            {
                SelectLevel(Loader.Scene.Level9);
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
    
        #if UNITY_EDITOR
        [Button]
        private void UnlockAllLevelsDebugOnly()
        {
            int levelsInGameIndex = 8;
            Saver.SaveLevelByIndex(levelsInGameIndex);
            DisableAllButtons();
            EnableButtonsBySave();
        }
        [Button]
        private void DeleteSaveDebugOnly()
        {
            Saver.DeleteSavedLevels();
            DisableAllButtons();
            EnableButtonsBySave();
        }
        #endif
    }
}
