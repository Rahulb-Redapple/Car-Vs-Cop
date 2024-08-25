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
    public class ShopScreen : UiPopupBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private Transform _vehicleParent;
        [SerializeField] private Transform _itemDisplayRawImage;
        [SerializeField] private ToggleGroup _contentToggleGroup;
        [SerializeField] private Vector3 _displayImageMaxScale;
        [SerializeField] private float  _vehicleParentRotSpeed;
        [SerializeField] private Ease _inEaseType;
        [SerializeField] private Ease _outEaseType;

        private VehicleData _playerVehicleData;

        private List<Item> pooledItems = new List<Item>();

        private PoolObjectType type = PoolObjectType.NONE;

        private ObjectPooling _objectPooling;

        private List<VehicleBase> _carList = new List<VehicleBase>();

        internal override void Cleanup()
        {
            HandleCurrentSelectedItem(0);
        }

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            if (isView)
            {
                GameHelper.Instance.StartListening(GameConstants.OnSelectItem, HandleCurrentSelectedItem);

                _playerVehicleData = _essentialConfigData.AccessConfig<VehicleData>();
                _objectPooling = _essentialHelperData.AccessData<ObjectPooling>();

                InstantiateAllCars();

                GetItemFromPool((PoolObjectType)data[0]);
            }
        }

        private void Update()
        {
            _vehicleParent.transform.Rotate(Vector3.up * _vehicleParentRotSpeed * Time.deltaTime);
        }

        private void GetItemFromPool(object obj)
        {
            ReturnPooledItems();

            AnimateItemDisplay();

            type = (PoolObjectType)obj;

            switch (type)
            {
                case PoolObjectType.ITEM:

                    HandleItemsData(_playerVehicleData.VehicleConfigs.ToList());
                    break;
            }

            HandleToggles((int)obj);
        }

        private void HandleItemsData(List<VehicleConfig> itemConfigs)
        {
            for (int i = 0; i < itemConfigs.Count; i++)
            {
                GameObject ob = _objectPooling.GetObjectFromPool(PoolObjectType.ITEM);
                Item item = ob.GetComponent<Item>();
                item.Init(itemConfigs[i]);
                item.InitButton(_contentParent, _contentToggleGroup, type, _popupHandler, () => { AnimateItemDisplay(); });
                pooledItems.Add(item);
            }
            //SetItemStatus();
        }

        private void HandleToggles(int index)
        {
            //shopToggleList[index].ToggleName.enabled = false;

            //for (int i = 0; i < shopToggleList.Count; i++)
            //{
            //    HandleEachToggleState(i, false);
            //}
            HandleEachToggleState(index, true);
        }

        private void HandleEachToggleState(int i, bool state)
        {
            //shopToggleList[i].Toggle.SetIsOnWithoutNotify(true);
            //shopToggleList[i].ToggleName.enabled = true;
        }

        private void ReturnPooledItems()
        {
            if (pooledItems.Count != 0)
            {
                for (int i = 0; i < pooledItems.Count; i++)
                {
                    ItemBase item = pooledItems[i].GetComponent<ItemBase>();
                    item.Cleanup();
                    _objectPooling.ReturnObjectToPool(pooledItems[i].gameObject, item.objectType);
                }

                pooledItems.Clear();
            }
        }

        //private void SetItemStatus()
        //{
        //    Debug.Log($"Current in use {type} ID :: {PlayerDataHandler.Player.Inventory.GetCurrentInUseItemId(type)}");

        //    if (pooledItems.Count != 0)
        //    {
        //        for (int i = 0; i < pooledItems.Count; i++)
        //        {
        //            ItemToggle itemToggle = pooledItems[i].GetComponent<ItemToggle>();
        //            if (Equals(itemToggle, null))
        //                return;

        //            if (PlayerDataHandler.Player.Inventory.PurchasedHoops.Contains(itemToggle.itemID))
        //            {
        //                if (itemToggle.itemID == PlayerDataHandler.Player.Inventory.GetCurrentInUseItemId(type))
        //                {
        //                    itemToggle.SetToggleViewStatus(true);
        //                }
        //                else
        //                {
        //                    itemToggle.SetToggleViewStatus(false);
        //                }
        //            }
        //        }
        //    }
        //}

        private void AnimateItemDisplay()
        {
            _itemDisplayRawImage.transform.DOScale(_displayImageMaxScale, 0.15f).SetEase(_inEaseType).onComplete = () => { _itemDisplayRawImage.transform.DOScale(Vector3.one, 0.15f).SetEase(_outEaseType); };
        }

        internal void HideShop()
        {
            _popupHandler.HidePopup();
            ReturnPooledItems();
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

        private void InstantiateAllCars()
        {
            if (_carList.Count != 0)
                return;

            else
            {
                for (int i = 0; i < _playerVehicleData.VehicleConfigs.Count; i++)
                {
                    VehicleBase ob = Instantiate(_playerVehicleData.VehicleConfigs[i].vehicleDatum.VehiclePrefab, Vector3.zero, Quaternion.Euler(0, 135, 0));
                    ob.transform.SetParent(_vehicleParent);
                    ob.gameObject.SetActive(false);
                    _carList.Add(ob);
                }
            }           
        }

        public void CloseShop()
        {
            HideShop();
        }
    }
}
