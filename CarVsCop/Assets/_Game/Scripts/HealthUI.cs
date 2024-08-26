using SimpleObjectPoolingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class HealthUI : ObjectPoolBase
    {
        [SerializeField] private Image _healthImage;

        [SerializeField] private Sprite _fullSprite;
        [SerializeField] private Sprite _emptySprite;

        internal void Init(Transform parent)
        {
            gameObject.transform.SetParent(parent);
            SetVisibility(true);
        }

        internal void SetHealthUI(bool isFull)
        {
            _healthImage.sprite = isFull ? _fullSprite : _emptySprite;
        }

        internal void Cleanup()
        {
            _objectPooling.ReturnObjectToPool(this, poolObjectType);
        }
    }
}
