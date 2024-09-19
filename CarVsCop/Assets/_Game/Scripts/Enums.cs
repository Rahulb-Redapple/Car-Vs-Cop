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
        STAR_UI,
        COLOR_ITEM
    }

    public enum CashType
    {
        NONE = 0,
        PLAYING,
        SPEND,
        HOURLY_BONUS,
        DAILY_REWARD,
        ACHIEVEMENT
    }

    public enum VehicleCategory
    {
        NONE = 0,
        TAXI_SEDAN = 101,
        TAXI_MUSCLE = 102,
        SPORTS_TYPE_A = 103,
        SPORTS_TYPE_B = 104,
        MUSCLE_TYPE_A = 105,
        MUSCLE_TYPE_B = 106,
        MUSCLE_TYPE_C = 107,
        SEDAN = 108,
        PICKUP = 109
    }

    public enum TweenType
    {
        NONE = 0,
        MOVE,
        SCALE
    }
}
