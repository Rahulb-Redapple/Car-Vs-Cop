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
        PLAYERDIED,
        NONE
    }

    public enum ColorInterpolationType
    {
        PINGPONG,
        COS,
        NONE
    }

    public enum AudioType
    {
        Music,
        SFX
    }

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }

    public enum VehicleAudioType
    {
        ENGINE_FIRE,
        ENGINE_RUNNING,
        ENGINE_HIT,
        NONE
    }

    public enum WantedLevel
    {
        FIRST,
        SECOND,
        THIRD,
        FOURTH,
        NONE
    }

    public enum PoolObjectType
    {
        PLAYER,
        COP_SEDAN,
        COP_MUSCLE,
        EXPLOSION,
        GROUND,
        ITEM,
        NONE
    }
}
