using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.General
{
    [RequireComponent(typeof(PlayerIdentifier))]
    public class IdentifierHandler : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("References")] [Required]
        protected PlayerIdentifier _playerIdentifier;
        [SerializeField] [FoldoutGroup("Identifiers")]
        private BaseIdentifier[] _baseIdentifiers;
        
        protected void OnEnable()
        {
            Initialize();
        }
        
        [Button, HideInEditorMode]
        protected virtual void Initialize()
        {
            foreach (BaseIdentifier baseIdentifier in _baseIdentifiers)
            {
                baseIdentifier.TriggerIdentify(_playerIdentifier.ThisPlayerType);
            }
        }
    }
}
