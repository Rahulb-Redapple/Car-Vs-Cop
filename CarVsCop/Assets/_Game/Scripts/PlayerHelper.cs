using UnityEngine;

namespace RacerVsCops
{
    public class PlayerHelper : MonoBehaviour
    {
        private Player _player;
        private VehicleData _vehicleData;
        private GameplayHelper _gameplayHelper;
        private EssentialHelperData _essentialHelperData;
        private EssentialConfigData _essentialConfigData;
        private CopSpawnHelper _copSpawnHelper;
        private CameraHelper _cameraHelper;
        private GroundGridHelper _groundGridHelper;

        internal void Init(GameplayHelper gameplayHelper, CopSpawnHelper copSpawnHelper, CameraHelper cameraHelper, GroundGridHelper groundGridHelper,  EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            _cameraHelper = cameraHelper;
            _gameplayHelper = gameplayHelper;
            _copSpawnHelper = copSpawnHelper;
            _groundGridHelper = groundGridHelper;
            _essentialHelperData = essentialHelperData;
            _essentialConfigData = essentialConfigData;
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();           
        }

        internal void ReadyToPlay()
        {
            for(int i=0; i < _vehicleData.VehicleConfigs.Count; i++)
            {
                if (_vehicleData.VehicleConfigs[i].vehicleDatum.ID == PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId())
                {
                    VehicleBase playerCar = Instantiate(_vehicleData.VehicleConfigs[i].vehicleDatum.VehiclePrefab, Vector3.zero, Quaternion.identity, transform);
                    _player = playerCar.GetComponent<Player>();
                    playerCar.Init(_gameplayHelper, _essentialHelperData);
                    _player.ReadyToPlay(_cameraHelper);
                    playerCar.gameObject.SetActive(true);
                    _copSpawnHelper.SetTarget(playerCar.transform);
                    _cameraHelper.SetTarget(playerCar.transform);
                    _groundGridHelper.ReadyToPlay(playerCar.transform);
                    break;
                }
            }
        }

        internal void Cleanup()
        {
            _player.Cleanup();
        }
    }
}
