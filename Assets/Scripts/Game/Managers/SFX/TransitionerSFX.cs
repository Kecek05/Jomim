using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class TransitionerSFX : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required] private AudioSource _fadeInSFX;
        [SerializeField] [Required] private AudioSource _fadeOutSFX;
    }
}
