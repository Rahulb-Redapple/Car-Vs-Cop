using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private ApplicationHandler _applicationHandler;
        [SerializeField] private PopupHandler _popupHandler;
        [SerializeField] private EssentialHelperData _essentialHelperData;

        //[SerializeField] private InputHandler _inputHandler;
        //[SerializeField] private AudioHandler _audioHandler;

        private EssentialConfigData _essentialConfigData;
        private PlayerSaveData _playerSaveData;

        private VehicleData _vehicleData;
        private VehicleConfig _vehicleConfig;
        private VehicleColorConfig _vehicleColorConfig;

        private void Awake()
        {
            _playerSaveData = PlayerDataHandler.LoadPlayerData();
            Application.targetFrameRate = 60;
            _essentialConfigData = Resources.Load<EssentialConfigData>(nameof(EssentialConfigData));
            _essentialConfigData.Init();
            _essentialHelperData.Init();
            //_inputHandler.Init();
            _popupHandler.Init(_essentialConfigData, _essentialHelperData);
            //_audioHandler.Init();
            _applicationHandler.Init(_popupHandler, _essentialHelperData, _essentialConfigData);

            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();
            _vehicleColorConfig = _essentialConfigData.AccessConfig<VehicleColorConfig>();
            _vehicleConfig = _vehicleData.GetVehicleConfig(PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId());
            List<ColorConfig.ColorData> materialColorCodeList = _vehicleColorConfig.GetColorData(_vehicleConfig.vehicleDatum.VehicleCategory);
            PlayerDataHandler.Player.Inventory.AddDefaultCar(materialColorCodeList[Random.Range(0, materialColorCodeList.Count)].MaterialCode);
        }

        private void SaveGame()
        {
            if (_playerSaveData.IsFirstTime)
            {
                _playerSaveData.SetIsFirstTime(false);
            }
            PlayerDataHandler.SavePlayerData();
        }

        private void OnDisable()
        {
            SaveGame();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveGame();
            }
        }
    }
}
