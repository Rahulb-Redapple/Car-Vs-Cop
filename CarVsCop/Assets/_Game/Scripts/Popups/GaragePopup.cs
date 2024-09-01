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

        private List<Player> _carList = new List<Player>();
        private List<Player> _purchasedCarsList = new List<Player>();
        private List<ColorItem> _colorItemList = new List<ColorItem>();

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
                _vehicleContainer.PopulateVehicles();
                _carList = _vehicleContainer.GetVehicleList();

                FilterOutPurchasedCars();
                SetItemStatus();
            }
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
                    _vehicleCategory = _carList[_selectedCar].VehicleConfig.vehicleDatum.VehicleCategory;
                    InitiateColors(_vehicleCategory);
                    HandleEquipButton(_vehicleCategory);
                    //UpdateUi(_carList[_selectedCar].VehicleConfig);
                }
            }
        }

        private void FilterOutPurchasedCars()
        {
            foreach(Player player in _carList)
            {
                if(PlayerDataHandler.Player.Inventory.PurchasedCarsDict.ContainsKey(player.VehicleID))
                {
                    if (!_purchasedCarsList.Contains(player))
                    {
                        _purchasedCarsList.Add(player);
                    }               
                }
            }
        }

        public void Next()
        {
            _purchasedCarsList[_selectedCar].SetVisibility(false);
            _purchasedCarsList[_selectedCar].Rotator.Cleanup();
            _selectedCar++;

            if (_selectedCar == _purchasedCarsList.Count)
                _selectedCar = 0;

            _vehicleCategory = _purchasedCarsList[_selectedCar].VehicleConfig.vehicleDatum.VehicleCategory;
            _purchasedCarsList[_selectedCar].SetVisibility(true);
            _purchasedCarsList[_selectedCar].Rotator.ReadyToRotate(true);

            InitiateColors(_vehicleCategory);
            HandleEquipButton(_vehicleCategory);
            //UpdateUi(_carList[_selectedCar].VehicleConfig);        
        }

        public void Previous()
        {
            _purchasedCarsList[_selectedCar].SetVisibility(false);
            _purchasedCarsList[_selectedCar].Rotator.Cleanup();
            _selectedCar--;
            if (_selectedCar == -1)
                _selectedCar = _purchasedCarsList.Count - 1;

            _vehicleCategory = _purchasedCarsList[_selectedCar].VehicleConfig.vehicleDatum.VehicleCategory;
            _purchasedCarsList[_selectedCar].SetVisibility(true);
            _purchasedCarsList[_selectedCar].Rotator.ReadyToRotate(true);

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
            _purchasedCarsList[_selectedCar].MaterialHandler.SetMaterial(material);
        }

        private void HandleEquipButton(VehicleCategory vehicleCategory)
        {
            _equipButton.interactable = (int)vehicleCategory == PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId() ? false : true;
            GameConstants.CurrentVehicleConfig = _purchasedCarsList[_selectedCar].VehicleConfig;
        }

        public void OnEquip()
        {
            PlayerDataHandler.Player.Inventory.SetCurrentInUseCar(_purchasedCarsList[_selectedCar].VehicleID);
            HandleEquipButton((VehicleCategory)PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId());
            Debug.Log($"Equipped vehiche ID :: " +
                $"{PlayerDataHandler.Player.Inventory.GetCurrentInUseCarId()} Name :: " +
                $"{_purchasedCarsList[_selectedCar].VehicleConfig.vehicleDatum.VehicleName}");
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
            if (!Equals(_purchasedCarsList.Count, 0))
            {
                _purchasedCarsList.ForEach(x =>
                {
                    x.Rotator.Cleanup();
                    x.SetVisibility(false);
                });
            }

            ClearColorList();
        }
    }
}
