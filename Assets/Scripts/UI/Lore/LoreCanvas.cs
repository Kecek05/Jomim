using System;
using System.Collections.Generic;
using KeceK.General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LoreCanvas : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required] private Button _previousButton;
        [SerializeField] [Required] private Button _nextButton;
        [SerializeField] [Required] private Button _continueButton;
        [SerializeField] [Required] private TextMeshProUGUI _contentText;
        [SerializeField] [Required] private TextMeshProUGUI _titleText;
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
            
            UpdateLoreDisplayToCurrentIndex();
        }
        

        private void NextLore()
        {
            ChangeLoreIndex(_currentLoreIndex + 1);
            EnableContinueButtonIfAtEnd();
            UpdateLoreDisplayToCurrentIndex();
        }

        private void PreviousLore()
        {
            ChangeLoreIndex(_currentLoreIndex - 1);
            UpdateLoreDisplayToCurrentIndex();
        }
        
        private void ChangeLoreIndex(int change)
        {
            _currentLoreIndex = Mathf.Clamp(_currentLoreIndex + change, 0, _maxLoreIndex);
        }
        
        private void EnableContinueButtonIfAtEnd()
        {
            if (_currentLoreIndex >= _maxLoreIndex)
            {
                _continueButton.gameObject.SetActive(true);
            }
        }

        private void UpdateLoreDisplayToCurrentIndex()
        {
            _contentText.text = _loreTextSOsInOrder[_currentLoreIndex].Content;
            _titleText.text = _loreTextSOsInOrder[_currentLoreIndex].Title;
        }
    }
}
