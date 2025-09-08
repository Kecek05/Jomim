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

        [SerializeField] [FoldoutGroup("Exits")]
        private LevelExit levelExitP1;
        [SerializeField] [FoldoutGroup("Exits")]
        private LevelExit levelExitP2;

        [Title("Settings")] 
        [SerializeField] private GameSettingsSO _gameSettingsSO;
        [SerializeField] private bool _canUnlockALevel = true;
        [SerializeField] [ShowIf(nameof(_canUnlockALevel))]
        private Loader.Scene _levelToUnlock = Loader.Scene.Level1;
        
        private bool _isP1AtExit;
        private bool _isP2AtExit;
        private bool _onChangingLevel = false;
        private bool _canExit = false;
        private bool _isReloadingLevel = false;
        
        private bool _debugChangingLevel = false;
        
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
            
            PauseManager.SetCanChangePauseState(true);
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

        [Button] [HideInEditorMode]
        private void GoNextLevel()
        {
            _onChangingLevel = true;
            OnChangingLevel?.Invoke();
            StartCoroutine(DelayedChangingLevel());
        }

        private IEnumerator DelayedChangingLevel()
        {
            yield return new WaitForSeconds(_gameSettingsSO.DelayToChangeLevel);
            if(_canUnlockALevel)
                Saver.SaveLevelByIndex((int)_levelToUnlock);
            Loader.LoadNextLevel();
        }
        
        [Button("Reload Level")] [HideInEditorMode]
        private void PlayerDeadStateOnOnPlayerDead()
        {
            StartCoroutine(DelayedReloadCurrentLevel());
        }
        
        private IEnumerator DelayedReloadCurrentLevel()
        {
            if (_isReloadingLevel) yield break;
            _isReloadingLevel = true;
            
            yield return new WaitForSeconds(_gameSettingsSO.DelayToReloadLevel);
            Loader.ReloadCurrentScene();
            
        }
        
        private void CollectablesManagerOnOnCollectedAllCoins()
        {
            _canExit = true;
            OnCanExit?.Invoke();
        }
        
        [Button] [HideInEditorMode]
        private void SetCurrentSceneDebugOnly(Loader.Scene currentScene)
        {
            Loader.SetCurrentSceneDebugOnly(currentScene);
        }
    }
}
