using UnityEngine;
using UnityEngine.EventSystems;

namespace RacerVsCops
{
    internal class Player : VehicleBase, IHealth
    {
        [SerializeField] private VehicleInvincibility _vehicleInvincibility;
        [SerializeField] private VehicleAudioHelper _vehicleAudioHelper;

        int _screenWidth;

        private CameraHelper _cameraHelper;

        internal void ReadyToPlay(CameraHelper cameraHelper)
        {
            _cameraHelper = cameraHelper;
            IsVehicleReady = true;
            GameHelper.Instance.InvokeAction(GameConstants.UpdatePlayerHealth, CurrentHealth);

            _screenWidth = Camera.main.pixelWidth;

            ParticleTrail.Init();

            _vehicleAudioHelper.PlayAudio(VehicleAudioType.ENGINE_RUNNING);
        }

        internal override void Update()
        {
            base.Update();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                TurnLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                TurnRight();
            }
#if UNITY_ANDROID
            MobileInput();
#endif

        }

        private void MobileInput()
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                Touch touch = Input.touches[0];

                if (touch.phase == TouchPhase.Stationary && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    if (touch.position.x > (_screenWidth / 2))
                        TurnRight();
                    else
                        TurnLeft();
                }
            }
        }

        public void TurnLeft()
        {
            transform.Rotate(Vector3.down * VehicleConfig.vehicleSetting.TurnSpeed * Time.deltaTime);

        }

        public void TurnRight()
        {
            transform.Rotate(Vector3.up * VehicleConfig.vehicleSetting.TurnSpeed * Time.deltaTime);

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
