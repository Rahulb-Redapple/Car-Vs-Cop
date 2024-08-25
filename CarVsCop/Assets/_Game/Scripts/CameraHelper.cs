using DG.Tweening;
using UnityEngine;

namespace RacerVsCops
{
    public class CameraHelper : MonoBehaviour
    {
        [SerializeField] private float smoothTime = 0.45f;
        [SerializeField] private Transform followCam;

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

        internal void ReadyToFollow(bool isReady)
        {
            isReadyToFollow = isReady;
        }

        internal void SetTarget(Transform target)
        {
            _target = target;
            offset = followCam.position - _target.position;
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
            followCam.position = Vector3.SmoothDamp(followCam.position, targetPos, ref currentVelocity, smoothTime);
            //followCam.position = Vector3.Lerp(followCam.position, targetPos, 0.35f);
        }

        internal void ShakeCamera()
        {
            followCam.DOPunchRotation(_shakeRot, _duration, _vibration, _elasticity);
        }

        internal void Cleanup()
        {
            followCam.transform.position = initialPosition;
        }
    }
}
