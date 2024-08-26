using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class SettingsPopup : UiPopupBase
    {
        [SerializeField] private AudioHandler _audioHandler;

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            if (isView)
            {
                
            }
        }

        internal override void Cleanup()
        {

        }
    }
}
