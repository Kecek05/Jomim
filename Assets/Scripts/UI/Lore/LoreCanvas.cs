using System;
using System.Collections.Generic;
using KeceK.General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LoreCanvas : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required] private Button _previousButton;
        [SerializeField] [Required] private Button _nextButton;
        [SerializeField] [Required] private Button _continueButton;
        [SerializeField] [Required] private LocalizeStringEvent _contentText;
        [Space(5f)]
        [SerializeField] private List<LoreTextSO> _loreTextSOsInOrder;
        
        private int _currentLoreIndex = 0;
        
        private int _maxLoreIndex => _loreTextSOsInOrder.Count - 1;

        private void Awake()
        {
            _previousButton.onClick.AddListener(PreviousLore);
            _nextButton.onClick.AddListener(NextLore);
            _continueButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenu);
                _continueButton.interactable = false;
            });
            _continueButton.gameObject.SetActive(false);

            DisableButtonsIfNeeded();
            UpdateLoreDisplayToCurrentIndex();
        }
        

        private void NextLore()
        {
            ChangeLoreIndex(1);
            DisableButtonsIfNeeded();
            EnableContinueButtonIfAtEnd();
            UpdateLoreDisplayToCurrentIndex();
        }

        private void PreviousLore()
        {
            ChangeLoreIndex(-1);
            DisableButtonsIfNeeded();
            UpdateLoreDisplayToCurrentIndex();
        }
        
        private void ChangeLoreIndex(int valueToChange)
        {
            _currentLoreIndex = Mathf.Clamp(_currentLoreIndex + valueToChange, 0, _maxLoreIndex);
        }
        
        private void DisableButtonsIfNeeded()
        {
            _previousButton.gameObject.SetActive(_currentLoreIndex > 0);
            _nextButton.gameObject.SetActive(_currentLoreIndex < _maxLoreIndex);
        }
        
        private void EnableContinueButtonIfAtEnd()
        {
            _continueButton.gameObject.SetActive(_currentLoreIndex >= _maxLoreIndex);
        }

        private void UpdateLoreDisplayToCurrentIndex()
        {
            _contentText.SetEntry(_loreTextSOsInOrder[_currentLoreIndex].TitleKey);
        }
    }
}
