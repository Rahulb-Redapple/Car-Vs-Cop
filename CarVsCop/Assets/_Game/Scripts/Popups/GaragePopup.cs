using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RacerVsCops
{
    public class GaragePopup : UiPopupBase
    {
        [SerializeField] private CarUiData _carUiData;

        private VehicleContainer _vehicleContainer;

        private List<Player> _carList = new List<Player>();
        private List<Player> _purchasedCarsList = new List<Player>();

        private int _selectedCar = 0;

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

                    //UpdateUi(_carList[_selectedCar].VehicleConfig);
                }
            }
        }

        private void FilterOutPurchasedCars()
        {
            foreach(Player player in _carList)
            {
                if(PlayerDataHandler.Player.Inventory.PurchasedCars.Any(x => x == player.VehicleID))
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
            _purchasedCarsList[_selectedCar].SetVisibility(true);
            _purchasedCarsList[_selectedCar].Rotator.ReadyToRotate(true);

            //UpdateUi(_carList[_selectedCar].VehicleConfig);        
        }

        public void Previous()
        {
            _purchasedCarsList[_selectedCar].SetVisibility(false);
            _purchasedCarsList[_selectedCar].Rotator.Cleanup();
            _selectedCar--;
            if (_selectedCar == -1)
                _selectedCar = _purchasedCarsList.Count - 1;
            _purchasedCarsList[_selectedCar].SetVisibility(true);
            _purchasedCarsList[_selectedCar].Rotator.ReadyToRotate(true);

            //UpdateUi(_carList[_selectedCar].VehicleConfig);
        }

        public void HideGarage()
        {
            _popupHandler.HidePopup();
            Cleanup();
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
        }
    }
}
