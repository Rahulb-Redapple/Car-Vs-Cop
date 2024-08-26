using UnityEngine;

namespace RacerVsCops
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInput))]

    internal class Player : VehicleBase, IHealth
    {
        [SerializeField] private VehicleInvincibility _vehicleInvincibility;
        [SerializeField] private VehicleAudioHelper _vehicleAudioHelper;
        [SerializeField] private PlayerInput _playerInput;

        private CameraHelper _cameraHelper;

        internal void ReadyToPlay(CameraHelper cameraHelper)
        {
            _cameraHelper = cameraHelper;
            _playerInput.Init(cameraHelper, VehicleConfig);
            IsVehicleReady = true;
            GameHelper.Instance.InvokeAction(GameConstants.UpdatePlayerHealth, CurrentHealth);

            ParticleTrail.Init();

            _vehicleAudioHelper.PlayAudio(VehicleAudioType.ENGINE_RUNNING);
        }

        internal override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out VehicleIdentifier vehicleIdentifier))
            {
                switch (vehicleIdentifier.vehicleType)
                {
                    case VehicleType.COP:
                        InitiateVehicleHealth(OnHitHealthDecrease);
                        break;
                }
            }
        }

        public void InitiateVehicleHealth(int damageCount)
        {
            if (CurrentHealth >= 1)
            {
                if (CurrentHealth > 1)
                    ParticleTrail.HandleTrails(TotalHealth - CurrentHealth);

                CurrentHealth -= damageCount;
                GameHelper.Instance.InvokeAction(GameConstants.UpdatePlayerHealth, CurrentHealth);
                _vehicleInvincibility.Init();

                if (CurrentHealth <= 0)
                {
                    ParticleTrail.Cleanup();
                    gameObject.SetActive(false);
                    ExplodeVehicle();
                    GameplayHelper.InitiateGameplayEnd(false, GameplayLoseStatus.PLAYERDIED);
                }
                _cameraHelper.ShakeCamera();
                _vehicleAudioHelper.PlayAudio(VehicleAudioType.ENGINE_HIT);
#if UNITY_ANDROID || UNITY_IOS
                Vibration.Vibrate(200);
#endif
            }
        }

        internal override void Cleanup()
        {
            _vehicleInvincibility.Cleanup();
            _vehicleAudioHelper.Cleanup();
            Destroy(gameObject);
        }
    }
}
