using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.General
{
    [RequireComponent(typeof(PlayerIdentifier))]
    public class IdentifierHandler : MonoBehaviour
    {
        [SerializeField] [Title("References")] [Required]
        protected PlayerIdentifier _playerIdentifier;
        [SerializeField]
        private BaseIdentifier[] _baseIdentifiers;

        private void OnValidate()
        {
            GetPlayerIdentifier();
        }

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
        
        [ContextMenu("Get All Identifiers In This Object")]
        private void GetAllIdentifiersInThisObject()
        {
            _baseIdentifiers = GetComponents<BaseIdentifier>();
            GetPlayerIdentifier();
        }
        
        [ContextMenu("Get All Identifiers In Children")]
        private void GetAllIdentifiersInChildren()
        {
            _baseIdentifiers = GetComponentsInChildren<BaseIdentifier>();
            GetPlayerIdentifier();
        }

        private void GetPlayerIdentifier()
        {
            _playerIdentifier = GetComponent<PlayerIdentifier>();
        }
    }
}
