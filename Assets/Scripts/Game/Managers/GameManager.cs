using System;
using System.Collections;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        public static event Action OnChangingLevel;
        public static event Action OnCanExit;

        [SerializeField] [Required] [FoldoutGroup("Exits")]
        private LevelExit levelExitP1;
        [SerializeField] [Required] [FoldoutGroup("Exits")]
        private LevelExit levelExitP2;
        
        [Title("Settings")] [SerializeField]
        private float _delayToChangeLevel = 1.5f;
        [SerializeField]
        private float _delayToReloadLevel = 1f;
        
        private bool _isP1AtExit;
        private bool _isP2AtExit;
        private bool _onChangingLevel = false;
        private bool _canExit = false;
        
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
            CollectablesManager.OnCollectedAllCoins += CollectablesManagerOnOnCollectedAllCoins;
            PlayerDeadState.OnPlayerDead += PlayerDeadStateOnOnPlayerDead;
        }

        private void OnDestroy()
        {
            levelExitP1.OnLevelExitTouched -= HandleExitTouchedP1;
            levelExitP2.OnLevelExitTouched -= HandleExitTouchedP2;
            CollectablesManager.OnCollectedAllCoins -= CollectablesManagerOnOnCollectedAllCoins;
            PlayerDeadState.OnPlayerDead -= PlayerDeadStateOnOnPlayerDead;
        }

        private void HandleExitTouchedP1(bool isTouching)
        {
            _isP1AtExit = isTouching;
            CheckBothPlayersAtExitAndCallChangeLevel();
        }
        
        private void HandleExitTouchedP2(bool isTouching)
        {
            _isP2AtExit = isTouching;
            CheckBothPlayersAtExitAndCallChangeLevel();
        }
        
        private void CheckBothPlayersAtExitAndCallChangeLevel()
        {
            if (_isP1AtExit && _isP2AtExit && !_onChangingLevel && _canExit)
            {
                GoNextLevel();
            }
        }

        [Button]
        private void GoNextLevel()
        {
            _onChangingLevel = true;
            StartCoroutine(DelayedChangingLevel());
            OnChangingLevel?.Invoke();
        }

        private IEnumerator DelayedChangingLevel()
        {
            yield return new WaitForSeconds(_delayToChangeLevel);
            Loader.LoadNextLevel();
        }
        
        [Button]
        private void PlayerDeadStateOnOnPlayerDead()
        {
            StartCoroutine(DelayedReloadCurrentLevel());
        }
        
        private IEnumerator DelayedReloadCurrentLevel()
        {
            yield return new WaitForSeconds(_delayToReloadLevel);
            Loader.ReloadCurrentScene();
            
        }
        
        private void CollectablesManagerOnOnCollectedAllCoins()
        {
            _canExit = true;
            OnCanExit?.Invoke();
        }
    }
}
