using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace RacerVsCops
{
    public sealed class PlayerSaveData
    {
        [JsonProperty(PropertyName = "isFirstTime")]
        private bool _isFirstTime = true;
        [JsonProperty(PropertyName = "gameCurrency")]
        private GameCurrency _gameCurrency;
        [JsonProperty(PropertyName = "userSettingsPreferences")]
        private UserSettingsPreferences _userSettingsPreferences;
        [JsonProperty(PropertyName = "inventory")]
        private Inventory _inventory;
        [JsonProperty(PropertyName = "lastUpdateTime")]
        private DateTime _lastUpdateTime;

        private TimeSpan _currentLoginTimeDiff;

        [JsonIgnore] public bool IsFirstTime => _isFirstTime;
        [JsonIgnore] public ref GameCurrency GameCurrency => ref _gameCurrency;
        [JsonIgnore] public ref UserSettingsPreferences UserSettingsPreferences => ref _userSettingsPreferences;
        [JsonIgnore] public ref Inventory Inventory => ref _inventory;
        [JsonIgnore] public DateTime LastUpdateTime => _lastUpdateTime;
        [JsonIgnore] public TimeSpan CurrentLoginTimeDiff => _currentLoginTimeDiff;

        public PlayerSaveData()
        {
            _gameCurrency = new GameCurrency();
            _userSettingsPreferences = new UserSettingsPreferences();
            _inventory = new Inventory();
        }

        public void SetIsFirstTime(bool isFirstTime)
        {
            _isFirstTime = isFirstTime;
        }

        public void UpdateLastUpdateTime()
        {
            _lastUpdateTime = DateTime.UtcNow;
        }

        public void UpdateCurrentLoginTimeDiff(TimeSpan currentLoginTimeDiff)
        {
            _currentLoginTimeDiff = currentLoginTimeDiff;
        }
    }

    [Serializable]
    public sealed class GameCurrency
    {
        [JsonProperty(PropertyName = "CashData")]
        private Dictionary<CashType, long> _cashDict = new Dictionary<CashType, long>();

        /// <summary>
        /// Update the amount of cash the player has.
        /// </summary>
        /// <param name="cashCount">Amount of cash to be increased or deducted.
        /// A positive value will add cash.
        /// A negatve value will deduct cash.</param>
        public void UpdateCash(CashType cashType, long cashAmount)
        {
            if(!_cashDict.ContainsKey(cashType))
            {
                _cashDict.Add(cashType, 0);
            }
            _cashDict[cashType] += cashAmount;   

            //GameHelper.Instance.InvokeAction(GameConstants.CashAmountUpdated, _cashCount);
        }

        public long GetCashByType(CashType cashType)
        {
            if (_cashDict.ContainsKey(cashType))
            {
                return _cashDict[cashType];
            }
            else
            {
                _cashDict.Add(cashType, 0);
            }
            return _cashDict[cashType];
        }

        public long GetTotalCash()
        {
            return _cashDict.Sum(amount => amount.Value);
        }
    }

    [Serializable]
    public sealed class UserSettingsPreferences
    {
        [JsonProperty(PropertyName = "Music")]
        private bool _musicState = true;

        [JsonIgnore] public bool MusicState => _musicState;


        [JsonProperty(PropertyName = "SFX")]
        private bool _sfxState = true;

        [JsonIgnore] public bool SfxState => _sfxState;

        public void UpdateMusicStatus(bool state)
        {
            _musicState = state;
        }

        public void UpdateSFXStatus(bool state)
        {
            _sfxState = state;
        }
    }

    [Serializable]
    public sealed class Inventory
    {
        [JsonProperty(PropertyName = "purchasedCarsDict")]
        private Dictionary<int, List<string>> _purchasedCarsDict = new Dictionary<int, List<string>>();

        [JsonProperty(PropertyName = "currentInUseCarId")]
        private int _currentInUseCarId = 101;

        [JsonIgnore] public Dictionary<int, List<string>> PurchasedCarsDict => _purchasedCarsDict;

        public void AddDefaultCar(string materialCode) 
        {
            if (_purchasedCarsDict.ContainsKey(_currentInUseCarId))
            {
                List<string> materialCodeList = _purchasedCarsDict[_currentInUseCarId];
                if(!materialCodeList.Contains(materialCode))
                {
                    materialCodeList.Add(materialCode);
                }
            }
            else
            {
                List<string> materialCodeList = new List<string>();
                materialCodeList.Add(materialCode);
                _purchasedCarsDict.Add(_currentInUseCarId, materialCodeList);
            }
        }

        public void AddNewPurchasedCar(int purchasedCarId, string materialCode)
        {
            if (!_purchasedCarsDict.ContainsKey(purchasedCarId))
            {
                List<string> materialCodeList = new List<string>();
                materialCodeList.Add(materialCode);
                _purchasedCarsDict.Add(purchasedCarId, materialCodeList);
            }                
        }

        public void AddNewVehicleMaterial(int purchasedCarId, string materialCode)
        {
            if (_purchasedCarsDict.ContainsKey(purchasedCarId))
            {
                List<string> materialCodeList = _purchasedCarsDict[_currentInUseCarId];
                if (!materialCodeList.Contains(materialCode))
                {
                    materialCodeList.Add(materialCode);
                }
            }
        }

        public void SetCurrentInUseCar(int currentInUseCarId)
        {
            _currentInUseCarId = currentInUseCarId;
        }

        public int GetCurrentInUseCarId()
        {
            return _currentInUseCarId;
        }

        //[Serializable]
        //public sealed class VehicleAdditionalData
        //{
        //    [JsonProperty(PropertyName = "vehicleMaterialCode")]
        //    private List<string> _vehicleMaterialCode = new List<string>();

        //    [JsonIgnore]
        //    public List<string> VehicleMaterialCode => _vehicleMaterialCode;

        //    private VehicleAdditionalData() { }

        //    public VehicleAdditionalData(string vehicleMaterialCode)
        //    {
        //        if(!_vehicleMaterialCode.Contains(vehicleMaterialCode))
        //            _vehicleMaterialCode.Add(vehicleMaterialCode);
        //    }   
        //}
    }

}
