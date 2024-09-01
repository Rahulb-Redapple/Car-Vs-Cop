using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RacerVsCops
{
    public class MessagePopup : UiPopupBase
    {
        [SerializeField] private TMP_Text _messageText;

        internal override void Cleanup()
        {
            _messageText.text = string.Empty;
        }

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            if (isView) 
            {
                _messageText.text = (string)data[0];
            }
        }

        public void HidePopup()
        {
            _popupHandler.HidePopup();
        }
    }
}
