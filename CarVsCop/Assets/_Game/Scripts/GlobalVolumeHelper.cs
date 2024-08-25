using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace RacerVsCops
{
    public class GlobalVolumeHelper : MonoBehaviour
    {
        [SerializeField] private Volume _globalVolume;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;

        [SerializeField] private float duration = 2;
        [SerializeField] private float startTime = 1;

        [SerializeField] private ColorInterpolationType _colorInterpolationType;

        private Vignette _vignette;

        private Color _color;
        private float _interpolation;

        private float t1;
        private float t2;

        private bool _isReadyToPlay = false;

        internal void Init()
        {
            if (_globalVolume.profile.TryGet(out Vignette vignette))
            {
                _vignette = vignette;
            }
        }

        internal void ReadyToPlayEffect()
        {
            _isReadyToPlay = true;
            _vignette.active = true;
        }

        private void Update()
        {
            if (!_isReadyToPlay)
                return;

            switch(_colorInterpolationType)
            {
                case ColorInterpolationType.PINGPONG:
                    _interpolation = Mathf.PingPong(Time.time - startTime, 1) / duration;
                    break;

                case ColorInterpolationType.COS:
                    _interpolation = (Mathf.Cos(((Time.time - startTime) + duration) * Mathf.PI / duration) + 1) * 0.5f;
                    break;
            }

            // Use t2 instead of t1 if you want smoother interpolation
            _color = Color.Lerp(startColor, endColor, _interpolation);
            _vignette.color.Override(_color);
        }

        internal void Cleanup()
        {
            _isReadyToPlay = false;
            _vignette.active = false;
        }
    }
}
