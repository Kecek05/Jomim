using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class GameSFXManager : MonoBehaviour
    {
        [Title("References")] [SerializeField] [Required]
        private AudioSource _exitPoweredSFX;
        [SerializeField] [Required] 
        private AudioSource _nextLevelSFX;
        
        private void Start()
        {
            GameManager.OnChangingLevel += GameManagerOnOnChangingLevel;
            GameManager.OnCanExit += GameManagerOnOnCanExit;
        }

        private void OnDestroy()
        {
            GameManager.OnChangingLevel -= GameManagerOnOnChangingLevel;
            GameManager.OnCanExit -= GameManagerOnOnCanExit;
        }

        [Button("Next Level SFX")]
        private void GameManagerOnOnChangingLevel()
        {
            _nextLevelSFX.Play();
        }
        [Button("Can Exit SFX")]
        private void GameManagerOnOnCanExit()
        {
            _exitPoweredSFX.Play();
        }
    }
}
