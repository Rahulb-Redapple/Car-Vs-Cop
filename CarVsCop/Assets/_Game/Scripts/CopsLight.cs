using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class CopsLight : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _lights = new List<GameObject>();
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _flashInterval = 0.5f;

        private bool _switchLight;
        private bool _hasSiren = false;

        private Coroutine _copLightCoroutine;

        internal void Init()
        {
            _copLightCoroutine = StartCoroutine(FlashyLightEffect());

            if (!_hasSiren)
                return;
            _audioSource.Play();
        }

        private IEnumerator FlashyLightEffect()
        {
            while (true)
            {
                _switchLight = !_switchLight;

                _lights[0].SetActive(_switchLight);
                _lights[1].SetActive(!_switchLight);

                yield return Utility.GetWaitForSeconds(_flashInterval);
            }
        }

        internal void Cleanup()
        {
            _audioSource.Stop();

            if (!Equals(_copLightCoroutine, null))
            {
                StopCoroutine(_copLightCoroutine);
            }
        }
    }
}
