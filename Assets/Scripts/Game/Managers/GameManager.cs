using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        public static event Action OnChangingLevel;

        [SerializeField] [Required] [FoldoutGroup("Exits")]
        private LevelExit levelExitP1;
        [SerializeField] [Required] [FoldoutGroup("Exits")]
        private LevelExit levelExitP2;
        
        private bool _isP1AtExit;
        private bool _isP2AtExit;
        private bool _onChangingLevel = false;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            levelExitP1.OnLevelExitTouched += HandleExitTouchedP1;
            levelExitP2.OnLevelExitTouched += HandleExitTouchedP2;
        }

        private void OnDestroy()
        {
            levelExitP1.OnLevelExitTouched -= HandleExitTouchedP1;
            levelExitP2.OnLevelExitTouched -= HandleExitTouchedP2;
        }

        private void HandleExitTouchedP1(bool isTouching)
        {
            _isP1AtExit = isTouching;
            CheckBothPlayersAtExit();
        }
        
        private void HandleExitTouchedP2(bool isTouching)
        {
            _isP2AtExit = isTouching;
            CheckBothPlayersAtExit();
        }
        
        private void CheckBothPlayersAtExit()
        {
            if (_isP1AtExit && _isP2AtExit && !_onChangingLevel)
            {
                _onChangingLevel = true;
                OnChangingLevel?.Invoke();
            }
        }
    }
}
