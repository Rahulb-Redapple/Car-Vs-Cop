using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class ParticleTrail : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _exhaustTrailList = new List<ParticleSystem>();
        [SerializeField] private VehicleAudioHelper _vehicleAudioHelper;
        [SerializeField] private bool _isPlayer = false;

        private AudioSource _source;

        internal void Init()
        {
            _source = _vehicleAudioHelper.GetAudioSourceByType(VehicleAudioType.ENGINE_FIRE);
        }

        internal void HandleTrails(int index)
        {
            foreach (ParticleSystem trailEffect in _exhaustTrailList)
            {
                trailEffect.Stop();
            }
            _exhaustTrailList[index].Play();

            if (_isPlayer)
            {
                if (!_source.isPlaying)
                    _source.Play();

                switch (index)
                {
                    case 0:
                        _source.volume = 0.5f;
                        break;

                    case 1:
                        _source.volume = 1f;
                        break;

                }
            }
        }

        internal void Cleanup()
        {
            if (_isPlayer)
            {
                if (_source.isPlaying)
                {
                    _source.Stop();
                    _source.volume = 0f;
                }
            }

            foreach (ParticleSystem trailEffect in _exhaustTrailList)
            {
                trailEffect.Stop();
            }
        }
    }
}
