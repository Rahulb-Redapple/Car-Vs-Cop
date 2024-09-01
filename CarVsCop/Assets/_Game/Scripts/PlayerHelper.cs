using SimpleObjectPoolingSystem;
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
        private ObjectPooling _objectPooling;

        internal void Init(GameplayHelper gameplayHelper, CopSpawnHelper copSpawnHelper, CameraHelper cameraHelper, GroundGridHelper groundGridHelper,  EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            _cameraHelper = cameraHelper;
            _gameplayHelper = gameplayHelper;
            _copSpawnHelper = copSpawnHelper;
            _groundGridHelper = groundGridHelper;
            _essentialHelperData = essentialHelperData;
            _essentialConfigData = essentialConfigData;
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();   
            _objectPooling = _essentialHelperData.AccessData<ObjectPooling>();
        }

        internal void ReadyToPlay()
        {
            _player = Instantiate(_vehicleData.GetVehicleConfig(PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId()).vehicleDatum.VehiclePrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Player>();
            _player.Init(_gameplayHelper, _essentialHelperData);
            _player.Init(_objectPooling);
            _player.ReadyToPlay(_cameraHelper);
            //_player.MaterialHandler.SetMaterial()
            _player.SetVisibility(true);
            _copSpawnHelper.SetTarget(_player.transform);
            _cameraHelper.SetTarget(_player.transform);
            _groundGridHelper.ReadyToPlay(_player.transform);
        }

        internal void Cleanup()
        {
            _player.Cleanup();
        }
    }
}
