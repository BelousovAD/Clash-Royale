using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    internal class AudioMixerController : IDisposable
    {
        private const string MusicVolume = nameof(MusicVolume);
        private const string SoundVolume = nameof(SoundVolume);
        private const float MinValue = 0.0625f;
        private const float MaxValue = 1f;
        private const float LogarithmBase = 2;
        private const float Multiplier = 20;

        private readonly AudioMixer _audioMixer;
        private readonly Audio _music;
        private readonly Audio _sound;

        public AudioMixerController(AudioMixer audioMixer, Audio music, Audio sound)
        {
            _audioMixer = audioMixer;
            _music = music;
            _sound = sound;

            _music.VolumeChanged += UpdateMusicVolume;
            _sound.VolumeChanged += UpdateSoundVolume;
        }

        public void Dispose()
        {
            _music.VolumeChanged -= UpdateMusicVolume;
            _sound.VolumeChanged -= UpdateSoundVolume;
        }

        private void UpdateMusicVolume()
        {
            float value = Mathf.Clamp(_music.Volume, MinValue, MaxValue);
            _audioMixer.SetFloat(MusicVolume, Mathf.Log(value, LogarithmBase) * Multiplier);
        }

        private void UpdateSoundVolume()
        {
            float value = Mathf.Clamp(_sound.Volume, MinValue, MaxValue);
            _audioMixer.SetFloat(SoundVolume, Mathf.Log(value, LogarithmBase) * Multiplier);
        }
    }
}