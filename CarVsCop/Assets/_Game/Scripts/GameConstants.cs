namespace RacerVsCops
{
    internal static class GameConstants
    {
        internal static GameStates CurrentGameState = GameStates.NONE;
        internal static VehicleConfig CurrentVehicleConfig;

        #region Action_Keys
        internal const string CurrentCoin = nameof(CurrentCoin);
        internal const string CurrentScore = nameof(CurrentScore);
        internal const string OnSelectItem = nameof(OnSelectItem);
        internal const string UpdateCopType = nameof(UpdateCopType);
        internal const string ChangeGameState = nameof(ChangeGameState);
        internal const string UpdatePlayerHealth = nameof(UpdatePlayerHealth);
        internal const string CurrentCopKilledCount = nameof(CurrentCopKilledCount);
        internal const string UpdateSpawnedCopList = nameof(UpdateSpawnedCopList);
        #endregion Action_Keys
    }
}
