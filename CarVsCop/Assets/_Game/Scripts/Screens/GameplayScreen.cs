using SimpleObjectPoolingSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class GameplayScreen : UiScreenBase
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private TMP_Text _copKilledText;

        [SerializeField] private Transform _healthUiParent;
        [SerializeField] private Transform _wantedLevelUiParent;        

        private int _wantedLevel = 0;
        private int _totalWantedLevel = 0;
        private int _currentCopsKilled = 0;
        private int _playerTotalHealth = 0;
        private int _playerCurrentHealth = 0;

        private ObjectPooling _objectPooling;
        private GameplayConfig _gameplayConfig;

        private List<HealthUI> _healthItemList = new List<HealthUI>();
        private List<WantedLevelItem> _wantedLevelItems = new List<WantedLevelItem>();

        internal override void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            base.Init(popupHandler, essentialHelperData, essentialConfigData);
            GameHelper.Instance.StartListening(GameConstants.CurrentCopKilledCount, HandleCurrentCopsKilled);
            GameHelper.Instance.StartListening(GameConstants.CurrentScore, HandleCurrentScore);           
            GameHelper.Instance.StartListening(GameConstants.CurrentCoin, HandleCurrentCoin);
            GameHelper.Instance.StartListening(GameConstants.UpdatePlayerHealth, HandlePlayerHealth);

            _objectPooling = _essentialHelperData.AccessData<ObjectPooling>();  
            _gameplayConfig = _essentialConfigData.AccessConfig<PlayConfig>().gameplayConfig;
        }

        internal override void HandleGameStateChangeData(object[] data)
        {
            ResetData();
            HandleCurrentCoin(0);
            HandleCurrentScore(0);
            SetupWantedLevelAtInitial();
            SetupHealthAtInitial();
            HandleCurrentCopsKilled(0);
        }

        private void HandleCurrentCopsKilled(object obj)
        {
            _currentCopsKilled = (int)obj;
            _copKilledText.text = _currentCopsKilled.ToString();        

            switch(_currentCopsKilled)
            {
                case 0:
                    HandleWantedLevel(_wantedLevel);
                    GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.FIRST);
                    break;

                    case 10:
                    HandleWantedLevel(_wantedLevel);
                    GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.SECOND);
                    break;

                    case 20:
                    HandleWantedLevel(_wantedLevel);
                    GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.THIRD);
                    break;

                    case 30:
                    HandleWantedLevel(_wantedLevel);
                    GameHelper.Instance.InvokeAction(GameConstants.UpdateCopType, WantedLevel.FOURTH);
                    break;

            }
        }

        private void SetupWantedLevelAtInitial()
        {
            _totalWantedLevel = _gameplayConfig.TotalWantedLevel;

            for (int i = 0; i < _totalWantedLevel; i++)
            {
                WantedLevelItem wantedLevel = _objectPooling.GetObjectFromPool(PoolObjectType.STAR_UI).GetComponent<WantedLevelItem>();
                wantedLevel.Init(_wantedLevelUiParent);
                _wantedLevelItems.Add(wantedLevel);
            }
        }

        private void HandleWantedLevel(int index)
        {
            if (index < _totalWantedLevel)
            {
                for (int i = 0; i <= index; i++)
                {
                    _wantedLevelItems[i].InitiateBlink();
                }
                _wantedLevel++;
            }
        }

        private void SetupHealthAtInitial()
        {
            _playerTotalHealth = GameConstants.CurrentVehicleConfig.vehicleSetting.TotalHealth;
            _playerCurrentHealth = _playerTotalHealth;

            for (int i = 0; i < _playerTotalHealth; i++)
            {
                HealthUI healthItem = _objectPooling.GetObjectFromPool(PoolObjectType.HEALTH_UI).GetComponent<HealthUI>();
                healthItem.Init(_healthUiParent);
                _healthItemList.Add(healthItem);              
            }
        }

        private void HandleCurrentScore(object obj)
        {
            _scoreText.text = ((int)obj).ToString();
        }
        
        private void HandleCurrentCoin(object obj)
        {
            _coinText.text = ((int)obj).ToString();
        }

        private void HandlePlayerHealth(object obj)
        {
            _playerCurrentHealth = ((int)obj);

            for (int i = 0; i < _healthItemList.Count; i++)
            {
                if (i < _playerCurrentHealth)
                {
                    _healthItemList[i].SetHealthUI(true);
                }
                else
                {
                    _healthItemList[i].SetHealthUI(false);
                }
            }
        }

        private void ClearAllLists()
        {
            if (!Equals(_healthItemList.Count, 0))
            {
                _healthItemList.ForEach(x => x.Cleanup());
                _healthItemList.Clear();
            }

            if (!Equals(_wantedLevelItems, 0))
            {
                _wantedLevelItems.ForEach(x => x.Cleanup());    
                _wantedLevelItems.Clear();
            }
        }

        internal void ResetData()
        {
            _currentCopsKilled = 0;
            _wantedLevel = 0;
            _totalWantedLevel = 0;
            _currentCopsKilled = 0;
           _playerTotalHealth = 0;
           _playerCurrentHealth = 0;
           ClearAllLists();
        }

        internal override void Cleanup()
        {
            GameHelper.Instance.StopListening(GameConstants.CurrentCoin, HandleCurrentCoin);
            GameHelper.Instance.StopListening(GameConstants.CurrentScore, HandleCurrentScore);
            GameHelper.Instance.StopListening(GameConstants.UpdatePlayerHealth, HandlePlayerHealth);
            GameHelper.Instance.StopListening(GameConstants.CurrentCopKilledCount, HandleCurrentCopsKilled);
        }
    }
}