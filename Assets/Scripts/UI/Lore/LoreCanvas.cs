using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LoreCanvas : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _contentText;
        [SerializeField] private TextMeshProUGUI _titleText;
        [Space(5f)]
        [SerializeField] private List<LoreTextSO> _loreTextSOsInOrder;
        
        private int _currentLoreIndex = 0;
        
        private int _maxLoreIndex => _loreTextSOsInOrder.Count - 1;
        
        private void NextLore()
        {
            
        }

        private void PreviousLore()
        {
            
        }
    }
}
