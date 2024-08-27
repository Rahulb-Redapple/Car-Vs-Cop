using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class Rotator : MonoBehaviour
    {
        private EssentialConfigData _essentialConfigData;
        private PlayConfig _playConfig;
        private ShopConfig _shopConfig;

        private bool _canRotate = false;

        internal void Init(EssentialConfigData essentialConfigData)
        {
            _essentialConfigData = essentialConfigData;
            _playConfig = _essentialConfigData.AccessConfig<PlayConfig>();
            _shopConfig = _playConfig.shopConfig;
        }

        internal void ReadyToRotate(bool canRotate)
        {
            _canRotate = canRotate;
        }

        private void Update()
        {
            if (!_canRotate)
                return;
            transform.Rotate(Vector3.up * _shopConfig.RotationSpeed * Time.deltaTime);
        }

        internal void Cleanup()
        {
            ReadyToRotate(false);
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
    }
}
