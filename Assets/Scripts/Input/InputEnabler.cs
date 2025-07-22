using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Input
{
    public class InputEnabler : MonoBehaviour
    {
        [SerializeField] [Required] [FoldoutGroup("References")]
        private PlayerIdentifier _playerIdentifier;
        [SerializeField] [Required] [FoldoutGroup("References")]
        private InputReader _inputReader;
        
        private void Start()
        {
            _inputReader.SetPlayerType(_playerIdentifier.ThisPlayerType);
        }
    }
}
