using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LocalizationCanvas : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] private Image _localizationButtonSprite;
        [SerializeField] private Sprite _englishSprite;
        [SerializeField] private Sprite _brazilSprite;
        [SerializeField] private Button _localizationButton;
        
        private bool _isEnglish = true;
        
        private void Awake()
        {
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
        
        private void Start()
        {
            _isEnglish = Saver.GetSavedLocalizationIndex() == 0;
            ChangeLocalizationButtonVisual();
        }
        
        private void ChangeLocalizationButtonVisual()
        {
            _localizationButtonSprite.sprite = _isEnglish ? _englishSprite : _brazilSprite;
        }
    }
}
