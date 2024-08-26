using SimpleObjectPoolingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class WantedLevelItem : ObjectPoolBase
    {
        [SerializeField] private Image _starImage;
        [SerializeField] private Sprite _fullStarImage;
        [SerializeField] private Sprite _emptyStarImage;

        [SerializeField] private float _blinkDuration = 0.5f;
        [SerializeField] private float _decreaseRate = 0.2f;

        private bool _canStartBlink = false;

        private Coroutine _blinkingCoroutine;

        private float _currBlinkDuration;
 
        internal void Init(Transform parent)
        {
            SetVisibility(true);
            gameObject.transform.SetParent(parent);
        }

        internal void InitiateBlink()
        {
            _canStartBlink = true;
            _currBlinkDuration = _blinkDuration;
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
            UpdateStarSprite(_emptyStarImage);

            _objectPooling.ReturnObjectToPool(this, poolObjectType);    
        }
    }
}
