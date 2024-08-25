using System.Collections;
using UnityEngine;
using System;
using UnityEditor.Playables;

namespace RacerVsCops
{
    public class GameplayHelper : MonoBehaviour
    {
        private bool _isGameContinue = false;
        internal bool IsGameContinue => _isGameContinue;

        private CopSpawnHelper _copSpawnHelper;
        private PopupHandler _popupHandler;

        [SerializeField] private int maxCopCount = 4;

        private int currCopCount = 0;
        private int currentCopsKilled = 0;
        private float currentTimer = 0;
        private int currentGameplayCoinEarned = 0;

        private GameplayLoseStatus _gameplayLoseStatus = GameplayLoseStatus.NONE;

        private Coroutine gameProgressionCoroutine;

        internal void Init(PopupHandler popupHandler, CopSpawnHelper copSpawnHelper)
        {
            _popupHandler = popupHandler;
            _copSpawnHelper = copSpawnHelper;
        }

        internal void InitiateGameplay()
        {
            currentTimer = 0;
            currentCopsKilled = 0;
            _isGameContinue = true;
            currCopCount = maxCopCount;
            gameProgressionCoroutine = StartCoroutine(GameplayProgression());
        }

        internal void GetCopCount(int count)
        {
            currCopCount += count;
            UpdateCurrentCopsKilled();
        }

        private void UpdateCurrentCopsKilled()
        {
            currentCopsKilled++;
            GameHelper.Instance.InvokeAction(GameConstants.CurrentCopKilledCount, currentCopsKilled);
        }

        internal void UpdateCoinEarned(int amount)
        {
            currentGameplayCoinEarned += amount;
            GameHelper.Instance.InvokeAction(GameConstants.CurrentCoin, currentGameplayCoinEarned);
        }

        private IEnumerator GameplayProgression()
        {
            while (_isGameContinue)
            {
                yield return Utility.GetWaitForSeconds(1f);

                for (int i = 0; i < currCopCount; i++)
                {
                    //yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
                    yield return Utility.GetWaitForSeconds(2f);
                    _copSpawnHelper.SpawnCops();
                    currCopCount--;
                }
            }
            InitiateGameEnd();
            yield break;
        }

        private void Update()
        {
            if (_isGameContinue)
            {
                gameObject.SetActive(true);
                if (currentTimer >= 0f)
                {
                    currentTimer += Time.deltaTime;
                    GameHelper.Instance.InvokeAction(GameConstants.CurrentScore, (int)currentTimer);
                }
                else
                {
                    _isGameContinue = false;
                }
            }
        }

        private void InitiateGameEnd()
        {
            switch (_gameplayLoseStatus)
            {
                case GameplayLoseStatus.PLAYERDIED:
                    GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.RESULT, "Player died" });
                    break;

                case GameplayLoseStatus.NONE:
                    GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.RESULT, null });
                    break;
            }
        }

        internal void InitiateGameplayEnd(bool isImmediateEnd = false, GameplayLoseStatus gameplayLoseStatus = GameplayLoseStatus.NONE)
        {
            _gameplayLoseStatus = gameplayLoseStatus;

            Action gameplayEndAction = () => {
                _isGameContinue = false;
                //GameHelper.Instance.StopListening(GameConstants.GameplayPause, HandleGameplayPauseStatus);
            };
            if (!isImmediateEnd)
            {
                StartCoroutine(DelayToIndicateGameEnd(gameplayEndAction));
            }
            else
            {
                gameplayEndAction();
            }
        }

        private IEnumerator DelayToIndicateGameEnd(Action gameplayEndAction)
        {
            yield return null;
            gameplayEndAction();
        }

        internal void DetermineResult(string resultMessage)
        {
            _popupHandler.ShowPopup<GameOverPopup>(false, resultMessage);
        }

        internal void Cleanup()
        {
            if (!Equals(gameProgressionCoroutine, null))
            {
                StopCoroutine(gameProgressionCoroutine);
            }
            _isGameContinue = false;
        }
    }
}
