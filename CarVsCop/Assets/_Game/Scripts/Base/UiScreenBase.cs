using UnityEngine;

namespace RacerVsCops
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Canvas))]
    public abstract class UiScreenBase : MonoBehaviour
    {
        private Canvas _canvas;
        protected PopupHandler _popupHandler;
        protected EssentialHelperData _essentialHelperData;
        protected EssentialConfigData _essentialConfigData;

        internal virtual void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData)
        {
            GetCanvas();
            _popupHandler = popupHandler;
            _essentialHelperData = essentialHelperData;
            _essentialConfigData = essentialConfigData;
        }

        private void GetCanvas()
        {
            _canvas = GetComponent<Canvas>();
        }

        internal void SetScreenCanvasVisibility(bool isView)
        {
            if (Equals(_canvas, null))
            {
                GetCanvas();
            }
            _canvas.enabled = isView;
        }

        internal abstract void HandleGameStateChangeData(object[] data);

        internal abstract void Cleanup();
    }
}
