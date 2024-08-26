using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace RacerVsCops
{
	public class AudioManager : EssentialHelper
	{
		[SerializeField] private AudioConfig audioConfig;

		[Space]

		[SerializeField] private AudioMixerGroup sfxAudioMixerGroup;
		public AudioMixerGroup SfxAudioMixerGroup { get { return sfxAudioMixerGroup; } set { sfxAudioMixerGroup = value; } }
		[SerializeField] private AudioMixerGroup musicAudioMixerGroup;
		public AudioMixerGroup MusicAudioMixerGroup { get { return musicAudioMixerGroup; } set {  musicAudioMixerGroup = value; } }

		[Space]

		[SerializeField] private Transform sfxParent;
		[SerializeField] private Transform musicParent;

        private List<AudioDatum> audioSourceDatas = new List<AudioDatum>();

        internal void InitAudio()
		{
            for (int i= 0; i < audioConfig.AudioList.Count; i++)
            {
				AudioDatum audio = new AudioDatum(audioConfig.AudioList[i].name, audioConfig.AudioList[i].clip, 
					audioConfig.AudioList[i].volume, audioConfig.AudioList[i].pitch, audioConfig.AudioList[i].loop, 
					audioConfig.AudioList[i].playOnAwake, audioConfig.AudioList[i].audioType);

                switch(audio.type)
                {
                    case AudioType.MUSIC:
                        audio.source.transform.SetParent(musicParent);
                        audio.source.outputAudioMixerGroup = musicAudioMixerGroup;
                        break;

                    case AudioType.SFX:
                        audio.source.transform.SetParent(sfxParent);
                        audio.source.outputAudioMixerGroup = sfxAudioMixerGroup;
                        break;
                }
				audioSourceDatas.Add(audio);
            }
        }

        public void Play(object sound)
		{
            AudioDatum s = Array.Find(audioSourceDatas.ToArray(), item => item.name == (string)sound);
			s.source.Play();
		}
        internal void PlayOneShot(object sound) 
		{
            AudioDatum s = Array.Find(audioSourceDatas.ToArray(), item => item.name == (string)sound);
			s.source.PlayOneShot(s.source.clip);
        }
        internal void Stop(object sound)
		{
			AudioDatum s = Array.Find(audioSourceDatas.ToArray(), item => item.name == (string)sound);
			s.source.Stop();
		}
        internal void Pause(object sound)
		{
            AudioDatum s = Array.Find(audioSourceDatas.ToArray(), item => item.name == (string)sound);
			s.source.Pause();
        }

		internal void Cleanup()
		{
           
        }

        [System.Serializable]
        public class AudioDatum
        {
            public AudioDatum(string audioName, AudioClip audioClip, float volume, float pitch, bool loop, bool playAtAwake, AudioType audioType)
            {
                name = audioName;
                GameObject ob = new GameObject(name);
                source = ob.AddComponent<AudioSource>();
                source.clip = audioClip;
                type = audioType;
                source.volume = volume;
                source.pitch = pitch;
                source.loop = loop;
                source.playOnAwake = playAtAwake;
            }

            public string name;
            public AudioType type;
            public AudioSource source;
        }
    }
}