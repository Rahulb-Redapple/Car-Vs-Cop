using SimpleObjectPoolingSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public abstract class ItemBase : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private PoolObjectType _objectType = PoolObjectType.NONE;
        internal PoolObjectType objectType => _objectType;

        [SerializeField] private Toggle _toggle;

        protected Toggle toggle => _toggle;

        protected PopupHandler _popupHandler;
        protected RectTransform rectTransform => _rectTransform;

        private Action _itemTweenAction;

        internal Action itemTweenAction => _itemTweenAction;

        private void Awake()
        {

        }

        public abstract void OnClickUse();

        internal abstract void Init(VehicleConfig itemConfig);

        internal void InitButton(Transform parent, ToggleGroup toggleGroup, PoolObjectType poolObjectType, PopupHandler popupHandler, Action itemDisplayTween)
        {
            _objectType = poolObjectType;
            //_toggle.group = toggleGroup;
            _itemTweenAction = itemDisplayTween;
            transform.SetParent(parent);
            gameObject.SetActive(true);

            _popupHandler = popupHandler;
        }

        private void OnDestroy()
        {

        }

        internal abstract void Cleanup();
    }
}
