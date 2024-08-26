using System;
using UnityEngine;

namespace RacerVsCops
{
    [CreateAssetMenu(fileName = nameof(PlayConfig), menuName = "Scriptable Objects/" + nameof(PlayConfig))]
    public class PlayConfig : EssentialConfigScriptableObject
    {
        [SerializeField] private Vector3 _followCamInitialPos;
        [SerializeField] private float _splashScreenStayDuration;

        internal Vector3 FollowCamInitialPos => _followCamInitialPos;
        internal float SplashScreenStayDuration => _splashScreenStayDuration;

        [SerializeField] private CameraShakeSettings _cameraShakeSettings;
        internal CameraShakeSettings cameraShakeSettings => _cameraShakeSettings;

        [SerializeField] private GameplayConfig _gameplayConfig;
        internal GameplayConfig gameplayConfig => _gameplayConfig;
    }

    [Serializable]
    public class CameraShakeSettings
    {
        [SerializeField] private Vector3 _camShakeRot;
        [SerializeField] private float _duration;
        [SerializeField] private int _vibration;
        [SerializeField] private int _elasticity;

        internal Vector3 CamShakeRot => _camShakeRot;
        internal float Duration => _duration;
        internal int Vibration => _vibration;
        internal int Elasticity => _elasticity;
    }

    [Serializable]
    public class GameplayConfig
    {
        [SerializeField] private int _totalWantedLevel;
        internal int TotalWantedLevel => _totalWantedLevel;
    }
}
