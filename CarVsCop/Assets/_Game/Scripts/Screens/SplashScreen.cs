namespace RacerVsCops
{
    public class SplashScreen : UiScreenBase
    {
        private float _stayTime;

        internal override void Cleanup()
        {
            
        }

        internal override void HandleGameStateChangeData(object[] data)
        {
            _stayTime = _essentialConfigData.AccessConfig<PlayConfig>().SplashScreenStayDuration;
            Invoke(nameof(GoToLoadingScreen), _stayTime);
        }

        private void GoToLoadingScreen()
        {
            GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.LOADING, null });
        }
    }
}
