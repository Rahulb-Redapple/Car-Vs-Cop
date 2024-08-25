using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerVsCops
{
    public class VehicleAudioHelper : MonoBehaviour
    {
        [SerializeField] private List<AudioData> _audioDataList = new List<AudioData>();

        internal void PlayAudio(VehicleAudioType type)
        {
            AudioData selectedAudio = GetAudioByType(type);

            if (!Equals(selectedAudio, null))
            {
                selectedAudio.audio.Play();
            }
        }

        internal AudioSource GetAudioSourceByType(VehicleAudioType audioType)
        {
            AudioData selectedAudio = GetAudioByType(audioType);

            if (!Equals(selectedAudio, null))
            {
                return selectedAudio.audio;
            }
            return null;
        }

        private AudioData GetAudioByType(VehicleAudioType audioType)
        {
            for (int i = 0; i < _audioDataList.Count; i++)
            {
                if (audioType == _audioDataList[i].audioType)
                {
                    return _audioDataList[i];
                }
            }
            return null;
        }

        internal void Cleanup()
        {
            foreach (AudioData source in _audioDataList)
            {
                source.audio.Stop();
                source.audio.volume = 1f;
            }
        }

        [Serializable]
        public class AudioData
        {
            [SerializeField] private AudioSource _audioSource;
            [SerializeField] private VehicleAudioType _audioType;

            internal AudioSource audio => _audioSource;
            internal VehicleAudioType audioType => _audioType;
        }
    }
}

