using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacerVsCops
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;

        public AudioManager audioManager { get { return _audioManager; } }

        [SerializeField] private SwitchToggle _musicToggle;
        [SerializeField] private SwitchToggle _soundToggle;

        internal void Init()
        {
           audioManager.Init();
        }

        public void HandleMusicState(bool musicState)
        {
            _musicToggle.OnToggleValueChanged(musicState);
            _audioManager.MusicAudioMixerGroup.audioMixer.SetFloat("Music_Volume", musicState ? 0f : -80f);
            PlayerDataHandler.Player.UserSettingsPreferences.UpdateMusicStatus(musicState);
        }

        public void HandleSfxState(bool sfxState)
        {
            _soundToggle.OnToggleValueChanged(sfxState);
            _audioManager.SfxAudioMixerGroup.audioMixer.SetFloat("SFX_Volume", sfxState ? 0f : -80f);
            PlayerDataHandler.Player.UserSettingsPreferences.UpdateSFXStatus(sfxState);
        }

        public void Cleanup()
        {
            audioManager.Cleanup();
        }
    }
}
