using SimpleObjectPoolingSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class GaragePopup : UiPopupBase
    {
        [SerializeField] private CarUiData _carUiData;
        [SerializeField] private Transform _colorContentParent;
        [SerializeField] private Button _equipButton;

        private VehicleCategory _vehicleCategory;
        private VehicleContainer _vehicleContainer;
        private VehicleColorConfig _vehicleColorConfig;
        private ObjectPooling _objectPooling;

        private List<VehicleConfig> _carListConfigs = new List<VehicleConfig>();
        private List<VehicleConfig> _purchasedCarsListConfigs = new List<VehicleConfig>();
        private List<ColorItem> _colorItemList = new List<ColorItem>();

        private List<Player> _purchasedCarList = new List<Player>();

        private int _selectedCar = 0;

        internal override void Init(PopupHandler popupHandler, EssentialConfigData essentialConfigData, EssentialHelperData essentialHelperData)
        {
            base.Init(popupHandler, essentialConfigData, essentialHelperData);
            _vehicleColorConfig = _essentialConfigData.AccessConfig<VehicleColorConfig>();
            _objectPooling = _essentialHelperData.AccessData<ObjectPooling>();
        }

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            if (isView)
            {
                _vehicleContainer = (VehicleContainer)data[0];
                _carListConfigs = _vehicleContainer.GetAllVehicleConfigs();
                FilterOutPurchasedCars();
                _vehicleContainer.PopulateVehicles(_purchasedCarsListConfigs);
                _purchasedCarList = _vehicleContainer.GetVehicleList();

                SetItemStatus();
            }
        }

        private void SetItemStatus()
        {
            Debug.Log($"Current in use car :: {PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId()}");

            for (int i = 0; i < _purchasedCarList.Count; i++)
            {
                if (_purchasedCarList[i].VehicleID == PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId())
                {
                    _purchasedCarList[i].SetVisibility(true);
                    _purchasedCarList[i].Rotator.ReadyToRotate(true);
                    _selectedCar = i;
                    _vehicleCategory = _purchasedCarList[_selectedCar].VehicleConfig.vehicleDatum.VehicleCategory;
                    InitiateColors(_vehicleCategory);
                    HandleEquipButton(_vehicleCategory);
                    //UpdateUi(_carList[_selectedCar].VehicleConfig);
                }
            }
        }

        private void FilterOutPurchasedCars()
        {
            foreach(VehicleConfig player in _carListConfigs)
            {
                if(PlayerDataHandler.Player.Inventory.PurchasedCarsDict.ContainsKey(player.vehicleDatum.ID))
                {
                    if (!_purchasedCarsListConfigs.Contains(player))
                    {
                        _purchasedCarsListConfigs.Add(player);
                    }               
                }
            }
        }

        public void Next()
        {
            _purchasedCarList[_selectedCar].SetVisibility(false);
            _purchasedCarList[_selectedCar].Rotator.Cleanup();
            _selectedCar++;

            if (_selectedCar == _purchasedCarsListConfigs.Count)
                _selectedCar = 0;

            _vehicleCategory = _purchasedCarList[_selectedCar].VehicleConfig.vehicleDatum.VehicleCategory;
            _purchasedCarList[_selectedCar].SetVisibility(true);
            _purchasedCarList[_selectedCar].Rotator.ReadyToRotate(true);

            InitiateColors(_vehicleCategory);
            HandleEquipButton(_vehicleCategory);
            //UpdateUi(_carList[_selectedCar].VehicleConfig);        
        }

        public void Previous()
        {
            _purchasedCarList[_selectedCar].SetVisibility(false);
            _purchasedCarList[_selectedCar].Rotator.Cleanup();
            _selectedCar--;
            if (_selectedCar == -1)
                _selectedCar = _purchasedCarsListConfigs.Count - 1;

            _vehicleCategory = _purchasedCarList[_selectedCar].VehicleConfig.vehicleDatum.VehicleCategory;
            _purchasedCarList[_selectedCar].SetVisibility(true);
            _purchasedCarList[_selectedCar].Rotator.ReadyToRotate(true);

            InitiateColors(_vehicleCategory);
            HandleEquipButton(_vehicleCategory);
            //UpdateUi(_carList[_selectedCar].VehicleConfig);
        }

        private void InitiateColors(VehicleCategory vehicleCategory)
        {
            ClearColorList();

            foreach (ColorConfig colorConfig in _vehicleColorConfig.ColorConfigsList)
            {
                if(colorConfig.VehicleCategory == _vehicleCategory)
                {
                    foreach(ColorConfig.ColorData colorData in colorConfig.ColorDataList)
                    {
                        ColorItem colorItem = _objectPooling.GetObjectFromPool(PoolObjectType.COLOR_ITEM).GetComponent<ColorItem>();
                        colorItem.gameObject.transform.SetParent(_colorContentParent);
                        colorItem.SetColorData(colorData, SetMaterialToVehicle);
                        _colorItemList.Add(colorItem);
                    }
                }
            }
        }

        private void SetMaterialToVehicle(Material material)
        {
            _purchasedCarList[_selectedCar].MaterialHandler.SetMaterial(material);
        }

        private void HandleEquipButton(VehicleCategory vehicleCategory)
        {
            _equipButton.interactable = (int)vehicleCategory == PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId() ? false : true;
            GameConstants.CurrentVehicleConfig = _purchasedCarList[_selectedCar].VehicleConfig;
        }

        public void OnEquip()
        {
            PlayerDataHandler.Player.Inventory.SetCurrentInUseCar(_purchasedCarList[_selectedCar].VehicleID);
            HandleEquipButton((VehicleCategory)PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId());
            Debug.Log($"Equipped vehiche ID :: " +
                $"{PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId()} Name :: " +
                $"{_purchasedCarList[_selectedCar].VehicleConfig.vehicleDatum.VehicleName}");
        }

        public void HideGarage()
        {
            _popupHandler.HidePopup();
            Cleanup();
        }

        private void ClearColorList()
        {
            if (!Equals(_colorItemList.Count, 0))
            {
                _colorItemList.ForEach(x =>
                {
                    x.Cleanup();
                });
                _colorItemList.Clear();
            }
        }

        internal override void Cleanup()
        {
            if (!Equals(_purchasedCarList.Count, 0))
            {
                _purchasedCarList.ForEach(x =>
                {
                    x.Rotator.Cleanup();
                    x.SetVisibility(false);
                });
            }

            ClearColorList();
            _vehicleContainer.Cleanup();
        }
    }
}
