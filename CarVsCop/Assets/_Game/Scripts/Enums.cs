namespace RacerVsCops
{
    public enum GameStates
    {
        NONE,
        SPLASH,
        LOADING,
        LOGIN,
        MENU,
        SHOP,
        GAMEPLAY,
        RESULT,
        QUIT,
        PAUSE
    }

    public enum VehicleType
    {
        PLAYER,
        COP
    }

    public enum GameplayLoseStatus
    {
        NONE,
        PLAYERDIED
    }

    public enum ColorInterpolationType
    {
        NONE,
        PINGPONG,
        COS
    }

    public enum AudioType
    {
        NONE,
        MUSIC,
        SFX
    }

    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public enum VehicleAudioType
    {
        NONE,
        ENGINE_FIRE,
        ENGINE_RUNNING,
        ENGINE_HIT
    }

    public enum WantedLevel
    {
        NONE,
        FIRST,
        SECOND,
        THIRD,
        FOURTH
    }

    public enum PoolObjectType
    {
        NONE,
        PLAYER,
        COP_SEDAN,
        COP_MUSCLE,
        EXPLOSION,
        GROUND,
        HEALTH_UI,
        STAR_UI
    }
}
