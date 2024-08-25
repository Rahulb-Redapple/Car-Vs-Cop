using System;

namespace RacerVsCops
{
    public interface IController
    {
        void Init(PopupHandler popupHandler, EssentialHelperData essentialHelperData, EssentialConfigData essentialConfigData, Action<GameStates, object> stateChanged);
        void ChangeGameState(GameStates newGameState, object data = null);
        void RegisterInputs();
        void UnregisterInputs();
        void Cleanup();
    }
}
