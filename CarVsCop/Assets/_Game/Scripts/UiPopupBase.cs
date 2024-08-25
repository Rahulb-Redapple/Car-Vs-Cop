using UnityEngine;

namespace RacerVsCops
{
    [DisallowMultipleComponent]
    //[RequireComponent(typeof(AnimationPopup))]
    public abstract class UiPopupBase : MonoBehaviour
    {
        private AnimationPopup _animationPopup;

        protected PopupHandler _popupHandler;
        protected EssentialConfigData _essentialConfigData;
        protected EssentialHelperData _essentialHelperData;
        //protected AnimationPopup animationPopup => _animationPopup;

        internal virtual void Init(PopupHandler popupHandler, EssentialConfigData essentialConfigData, EssentialHelperData essentialHelperData)
        {
            _popupHandler = popupHandler;
            _essentialConfigData = essentialConfigData;
            _essentialHelperData = essentialHelperData;
            //_animationPopup = GetComponent<AnimationPopup>();
        }

        internal void SetPopupVisibility(bool isView)
        {
            //Action OnCompleteAction = () => gameObject.SetActive(isView);

            //if (isView)
            //{
            //    OnCompleteAction();
            //    _animationPopup.StartTween();
            //}
            //else
            //    _animationPopup.StopTween(OnCompleteAction);

            gameObject.SetActive(isView);
        }

        internal abstract void HandlePopupToggleData(bool isView, object[] data);

        internal abstract void Cleanup();
    }
}
