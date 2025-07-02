using KeceK.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    /// <summary>
    /// Responsible for changing properties based on this PlayerIdentifier
    /// </summary>
    public class PlayerIdentifierHandler : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("References")] [Required]
        private PlayerIdentifier _playerIdentifier;
    }
}
