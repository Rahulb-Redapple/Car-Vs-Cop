using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class Item : ItemBase
    {
        [SerializeField] private Image _iconImage;

        private int _itemID;

        private VehicleConfig _vehicleConfig;

        public override void OnClickUse()
        {
            itemTweenAction?.Invoke();
            GameHelper.Instance.InvokeAction(GameConstants.OnSelectItem, _itemID);

            PlayerDataHandler.Player.Inventory.SetCurrentInUseCar(_vehicleConfig.vehicleDatum.ID);
            GameConstants.CurrentVehicleConfig = _vehicleConfig;
        }

        internal override void Cleanup()
        {
            
        }

        internal override void Init(VehicleConfig vehicleConfig)
        {
            _vehicleConfig = vehicleConfig;
            _iconImage.sprite = _vehicleConfig.vehicleDatum.vehicleIcon;
            _itemID = _vehicleConfig.vehicleDatum.ID;
        }
    }
}
