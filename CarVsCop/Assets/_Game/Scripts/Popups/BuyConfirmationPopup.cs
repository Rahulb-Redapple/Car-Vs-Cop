using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RacerVsCops
{
    public class BuyConfirmationPopup : UiPopupBase
    {
        [SerializeField] private TMP_Text _messageText;

        private BuyVehicleButton _buyVehicleButton;

        internal override void Cleanup()
        {
            
        }

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            if (isView)
            {
                _buyVehicleButton = (BuyVehicleButton)data[0];  
            }
        }

        public void OnAccept()
        {
            _buyVehicleButton.ProcessPurchase();
            _popupHandler.HidePopup();
        }

        public void HidePopup()
        {
            _popupHandler.HidePopup();
        }
    }
}
