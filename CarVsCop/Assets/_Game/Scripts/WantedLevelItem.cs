using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class WantedLevelItem : MonoBehaviour
    {
        [SerializeField] private Image _starImage;
        [SerializeField] private Sprite _fullStarImage;
        [SerializeField] private Sprite _emptyStarImage;

        [SerializeField] private float blinkDuration;
        [SerializeField] private float _decreaseRate;

        private bool _canStartBlink = false;

        private Coroutine _blinkingCoroutine;

        private float _currBlinkDuration;
 
        internal void Init()
        {
            _canStartBlink = true;
            _currBlinkDuration = blinkDuration;
            _blinkingCoroutine = StartCoroutine(ActivateStar());
        }

        private IEnumerator ActivateStar()
        {
            while(_canStartBlink)
            {
                if(_currBlinkDuration > 0f)
                {
                    _currBlinkDuration -= Time.deltaTime / _decreaseRate;

                    UpdateStarSprite(_emptyStarImage);
                    yield return Utility.GetWaitForSeconds(0.2f);                    
                    UpdateStarSprite(_fullStarImage);
                    yield return Utility.GetWaitForSeconds(0.2f);
                }
                else
                {
                    _canStartBlink = false;
                }               
            }
        }

        private void UpdateStarSprite(Sprite sprite)
        {
            _starImage.sprite = sprite;
        }

        internal void Cleanup()
        {
            if(!Equals(_blinkingCoroutine, null))
            {
                StopCoroutine(_blinkingCoroutine);
            }
            UpdateStarSprite(_emptyStarImage);;
        }
    }
}
