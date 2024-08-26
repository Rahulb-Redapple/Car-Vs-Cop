using DG.Tweening;
using UnityEngine;

namespace RacerVsCops
{
    public class CameraHelper : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.45f;
        [SerializeField] private Camera _followCam;

        private Transform _followCamTransform => _followCam.transform;

        private Transform _target;
        private EssentialConfigData _essentialConfigData;
        private CameraShakeSettings _cameraShakeSettings;

        Vector3 offset;
        Vector3 currentVelocity;
        Vector3 initialPosition;

        private bool isReadyToFollow = false;

        private Vector3 _shakeRot;
        private float _duration;
        private int _vibration;
        private int _elasticity;

        internal void Init(EssentialConfigData essentialConfigData)
        {
            _essentialConfigData = essentialConfigData;
            initialPosition = _essentialConfigData.AccessConfig<PlayConfig>().FollowCamInitialPos;
            _cameraShakeSettings = _essentialConfigData.AccessConfig<PlayConfig>().cameraShakeSettings;

            _shakeRot = _cameraShakeSettings.CamShakeRot;
            _duration = _cameraShakeSettings.Duration;
            _vibration = _cameraShakeSettings.Vibration;
            _elasticity = _cameraShakeSettings.Elasticity;
        }

        internal Camera GetCam()
        {
            return _followCam;
        }

        internal void ReadyToFollow(bool isReady)
        {
            isReadyToFollow = isReady;
        }

        internal void SetTarget(Transform target)
        {
            _target = target;
            offset = _followCamTransform.position - _target.position;
        }

        private void LateUpdate()
        {
            if (!isReadyToFollow)
                return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            if (!_target)
                return;

            Vector3 targetPos = _target.position + offset;
            _followCamTransform.position = Vector3.SmoothDamp(_followCamTransform.position, targetPos, ref currentVelocity, _smoothTime);
            //followCam.position = Vector3.Lerp(followCam.position, targetPos, 0.35f);
        }

        internal void ShakeCamera()
        {
            _followCamTransform.DOPunchRotation(_shakeRot, _duration, _vibration, _elasticity);
        }

        internal void Cleanup()
        {
            _followCamTransform.position = initialPosition;
        }
    }
}
