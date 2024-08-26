using UnityEngine;
using UnityEngine.EventSystems;

namespace RacerVsCops
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(InputHandler))]

    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;

        private int _screenWidth;

        private CameraHelper _cameraHelper;
        private VehicleConfig _vehicleConfig;

        internal void Init(CameraHelper cameraHelper, VehicleConfig vehicleConfig)
        {
            _cameraHelper = cameraHelper;
            _vehicleConfig = vehicleConfig;
            _screenWidth = _cameraHelper.GetCam().pixelWidth;
        }

        private void Update()
        {
#if UNITY_EDITOR
            KeyboardInput();
#endif

#if UNITY_ANDROID
            MobileInput();
#endif
        }

        private void KeyboardInput()
        {
            switch (_inputHandler.Move.x)
            {
                case 1:
                    TurnRight();
                    break;

                case -1:
                    TurnLeft();
                    break;
            }
        }

        private void MobileInput()
        {
            //if (Input.touchCount > 0 && Input.touchCount < 2)
            //{
            //    Touch touch = Input.touches[0];

            //    if (touch.phase == TouchPhase.Stationary && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            //    {
            //        if (touch.position.x > (_screenWidth / 2))
            //            TurnRight();
            //        else
            //            TurnLeft();
            //    }
            //}

            if (_inputHandler.Touch.phase == TouchPhase.Stationary && !EventSystem.current.IsPointerOverGameObject(_inputHandler.Touch.fingerId))
            {
                if (_inputHandler.Touch.position.x > (_screenWidth / 2))
                    TurnRight();
                else
                    TurnLeft();
            }
        }

        public void TurnLeft()
        {
            transform.Rotate(Vector3.down * _vehicleConfig.vehicleSetting.TurnSpeed * Time.deltaTime);
        }

        public void TurnRight()
        {
            transform.Rotate(Vector3.up * _vehicleConfig.vehicleSetting.TurnSpeed * Time.deltaTime);
        }
    }
}
