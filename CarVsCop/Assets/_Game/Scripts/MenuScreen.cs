using SimpleObjectPoolingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class MenuScreen : UiScreenBase
    {
        [SerializeField] private Button _shopButton;

        private VehicleData _vehicleData;

        internal override void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            base.Init(popupHandler, essentialHelperData, essentialConfigData);
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();    
        }

        internal override void HandleGameStateChangeData(object[] data)
        {
            PlayerDataHandler.Player.Inventory.SetCurrentInUseCar(101);
            SetFetchedVehicle();

        }

        private void SetFetchedVehicle()
        {
            for(int i=0; i<_vehicleData.VehicleConfigs.Count; i++)
            {
                if (_vehicleData.VehicleConfigs[i].vehicleDatum.ID == PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId())
                {
                    GameConstants.CurrentVehicleConfig = _vehicleData.VehicleConfigs[i];
                    break;
                }
            }
        }

        public void play()
        {
            GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.GAMEPLAY, null });
        } 

        public void Shop(int shopID)
        {
            _popupHandler.ShowPopup<ShopScreen>(false, new object[] { PoolObjectType.ITEM });
        }

        internal override void Cleanup()
        {

        }
    }
}