using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SimpleObjectPoolingSystem;
using System.Linq;
using System;

namespace RacerVsCops
{
    [Serializable]
    internal sealed class CarUiData
    {
        [SerializeField] private TMP_Text _carNameText;
        [SerializeField] private TMP_Text _carPriceText;
        [SerializeField] private TMP_Text _carSpeedText;
        [SerializeField] private TMP_Text _carHealthText;

        internal TMP_Text CarNameText => _carNameText;
        internal TMP_Text CarPriceText => _carPriceText;
        internal TMP_Text CarSpeedText => _carSpeedText;
        internal TMP_Text CarHealthText => _carHealthText;
    }
    public class ShopPopup : UiPopupBase
    {
        [SerializeField] private CarUiData _carUiData;
        [SerializeField] private Transform _vehicleParent;
        [SerializeField] private Transform _itemDisplayRawImage;

        private VehicleData _vehicleData;
        private VehicleContainer _vehicleContainer;

        private List<Player> _carList = new List<Player>();

        private int _selectedCar = 0;

        internal override void Init(PopupHandler popupHandler, EssentialConfigData essentialConfigData, EssentialHelperData essentialHelperData)
        {
            base.Init(popupHandler, essentialConfigData, essentialHelperData);
            _vehicleData = _essentialConfigData.AccessConfig<VehicleData>();
        }

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            if (isView)
            {
                _vehicleContainer = (VehicleContainer)data[0];  

                GameHelper.Instance.StartListening(GameConstants.OnSelectItem, HandleCurrentSelectedItem);
                _vehicleContainer.PopulateVehicles();
                PopulateVehicles();
                SetItemStatus();
            }
        }

        private void PopulateVehicles()
        {
            if(!Equals(_carList.Count, 0))
                return;
            
            _carList = _vehicleContainer.GetVehicleList();
        }

        private void SetItemStatus()
        {
            Debug.Log($"Current in use car :: {PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId()}");

            for (int i = 0; i < _carList.Count; i++)
            {
                if (_carList[i].VehicleID == PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId())
                {
                    _carList[i].SetVisibility(true);
                    _carList[i].Rotator.ReadyToRotate(true);
                    _selectedCar = i;

                    UpdateUi(_carList[_selectedCar].VehicleConfig);
                }
            }
        }

        internal void HideShop()
        {
            _popupHandler.HidePopup();
            Cleanup();
        }

        private void HandleCurrentSelectedItem(object obj)
        {
            for(int i=0; i<_carList.Count; i++)
            {
                if (_carList[i].VehicleID == (int)obj)
                {
                    _carList[i].gameObject.SetActive(true);
                }
                else
                {
                    _carList[i].gameObject.SetActive(false);
                }
            }
        }

        public void Next()
        {
            _carList[_selectedCar].SetVisibility(false);
            _carList[_selectedCar].Rotator.Cleanup();
            _selectedCar++;
            if (_selectedCar == _carList.Count)
                _selectedCar = 0;
            _carList[_selectedCar].SetVisibility(true);
            _carList[_selectedCar].Rotator.ReadyToRotate(true);

            UpdateUi(_carList[_selectedCar].VehicleConfig);
        }

        public void Previous() 
        {
            _carList[_selectedCar].SetVisibility(false);
            _carList[_selectedCar].Rotator.Cleanup();
            _selectedCar--;
            if (_selectedCar == -1)
                _selectedCar = _carList.Count - 1;
            _carList[_selectedCar].SetVisibility(true);
            _carList[_selectedCar].Rotator.ReadyToRotate(true);

            UpdateUi(_carList[_selectedCar].VehicleConfig);
        }

        private void UpdateUi(VehicleConfig vehicleConfig)
        {
            _carUiData.CarNameText.text = $"Name {vehicleConfig.vehicleDatum.VehicleName}";
            _carUiData.CarPriceText.text = $"Price {vehicleConfig.vehicleDatum.VehiclePrice}";
            _carUiData.CarSpeedText.text = $"Speed {vehicleConfig.vehicleSetting.MoveSpeed}";
            _carUiData.CarHealthText.text = $"Health {vehicleConfig.vehicleSetting.TotalHealth}";
        }

        public void CloseShop()
        {
            HideShop();
        }

        internal override void Cleanup()
        {
            if(!Equals(_carList.Count, 0))
            {
                _carList.ForEach(x => 
                {
                    x.Rotator.Cleanup();
                    x.SetVisibility(false);
                });
            }
        }
    }
}
