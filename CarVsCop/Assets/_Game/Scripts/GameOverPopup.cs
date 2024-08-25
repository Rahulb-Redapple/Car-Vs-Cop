namespace RacerVsCops
{
    public class GameOverPopup : UiPopupBase
    {

        internal override void Cleanup()
        {
            
        }

        internal override void HandlePopupToggleData(bool isView, object[] data)
        {
            
        }

        public void GoToHome()
        {
            _popupHandler.HidePopup();
            GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.MENU, null });
        }
    }
}
