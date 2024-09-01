using SimpleObjectPoolingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class MenuScreen : UiScreenBase
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private VehicleContainer _vehicleContainer;

        private VehicleData _vehicleData;

        internal override void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            base.Init(popupHandler, essentialHelperData, essentialConfigData);
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();
            _vehicleContainer.Init(_essentialConfigData);
        }

        internal override void HandleGameStateChangeData(object[] data)
        {
            SetFetchedVehicle();
        }

        private void SetFetchedVehicle()
        {
            GameConstants.CurrentVehicleConfig = _vehicleData.GetVehicleConfig(PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId());
        }

        public void play()
        {
            GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.GAMEPLAY, null });
        } 

        public void Shop()
        {
            _popupHandler.ShowPopup<ShopPopup>(false, new object[] { _vehicleContainer });
        }

        public void Garage()
        {
            _popupHandler.ShowPopup<GaragePopup>(false, new object[] { _vehicleContainer });
        }

        internal override void Cleanup()
        {

        }
    }
}