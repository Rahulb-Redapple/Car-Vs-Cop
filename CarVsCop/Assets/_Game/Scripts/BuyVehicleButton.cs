using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class BuyVehicleButton : MonoBehaviour
    {
        private int _currentVehicleID;

        private EssentialConfigData _essentialConfigData;
        private PopupHandler _popupHandler;
        private VehicleData _vehicleData;
        private VehicleColorConfig _vehicleColorConfig;

        internal void Init(EssentialConfigData essentialConfigData, PopupHandler popupHandler)
        {
            _essentialConfigData = essentialConfigData;
            _popupHandler = popupHandler;
            _vehicleColorConfig = _essentialConfigData.AccessConfig<VehicleColorConfig>();
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();
        }

        internal void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        internal void EvaluateVehiclePurchase(int vehicleId)
        {
            _currentVehicleID = vehicleId;
            CheckVehicleInInventory();
        }

        private void CheckVehicleInInventory()
        {
            if (isVehicleInInventory()) SetVisibility(false);
            else
                SetVisibility(true);
        }

        private bool isVehicleInInventory()
        {
            return PlayerDataHandler.Player.Inventory.PurchasedCarsDict.ContainsKey(_currentVehicleID);
        }

        public void OnClickBuy()
        {
            if (PlayerDataHandler.Player.GameCurrency.GetTotalCash() >= _vehicleData.GetVehicleConfig(_currentVehicleID).vehicleDatum.VehiclePrice)
            {
                _popupHandler.ShowPopup<BuyConfirmationPopup>(true, new object[] { this });
            }
            else
            {
                _popupHandler.ShowPopup<MessagePopup>(true, new object[] { "You have insufficient cash" });
            }

        }

        internal void ProcessPurchase()
        {
            int vehiclePrice = _vehicleData.GetVehicleConfig(_currentVehicleID).vehicleDatum.VehiclePrice;

            PlayerDataHandler.Player.GameCurrency.UpdateCash(CashType.SPEND, -(long)vehiclePrice);

            string materialCode()
            {
                List<ColorConfig.ColorData> colorData = _vehicleColorConfig.GetColorData(_vehicleData.GetVehicleConfig(_currentVehicleID).vehicleDatum.VehicleCategory);
                foreach (ColorConfig.ColorData color in colorData)
                {
                    if (color.Price <= 0)
                    {
                        return color.MaterialCode;
                    }
                }
                return string.Empty;
            }

            PlayerDataHandler.Player.Inventory.AddNewPurchasedCar(_currentVehicleID, materialCode());
            CheckVehicleInInventory();

            Debug.Log($"New Balance :: {PlayerDataHandler.Player.GameCurrency.GetTotalCash()}");
        }

        internal void Cleanup()
        {

        }
    }
}
