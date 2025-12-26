using System;
using System.Collections.Generic;
using Bootstrap;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class Audio
    {
        private readonly AudioMixerGroup _group;
        private readonly AudioSourceSpawner _spawner;
        private readonly Dictionary<AudioClipKey, AudioClip> _tracks = new ();
        private SavvyServicesProvider _services;
        private bool _isActive = true;
        private float _volume = 1f;

        public Audio(
            AudioType type,
            AudioMixerGroup group,
            AudioSourceSpawner spawner,
            IEnumerable<Track> tracks)
        {
            Type = type;
            _group = group;
            _spawner = spawner;

            foreach (Track track in tracks)
            {
                _tracks.Add(track.Key, track.Clip);
            }
        }

        public event Action ActivityChanged;
        public event Action VolumeChanged;

        public AudioType Type { get; }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            private set
            {
                _isActive = value;
                ActivityChanged?.Invoke();
            }
        }

        public float Volume
        {
            get
            {
                return _volume;
            }

            private set
            {
                _volume = value;
                VolumeChanged?.Invoke();
            }
        }

        public IReadOnlyCollection<AudioClipKey> TrackKeys => _tracks.Keys;

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void SetActive(bool value)
        {
            IsActive = value;
            Save();
        }

        public void SetVolume(float value)
        {
            Volume = Mathf.Clamp01(value);
            Save();
        }

        public void Load()
        {
            IsActive = _services.Preferences.LoadBool(Type + nameof(IsActive), true);
            Volume = Mathf.Clamp01(_services.Preferences.LoadFloat(Type + nameof(Volume), 0.5f));
        }

        public float Play(AudioClipKey key)
        {
            PooledAudioSource audioSource = _spawner.Spawn();
            audioSource.Initialize(_group, _tracks[key]);
            audioSource.Play();

            return _tracks[key].length;
        }

        private void Save()
        {
            _services.Preferences.SaveBool(Type + nameof(IsActive), IsActive);
            _services.Preferences.SaveFloat(Type + nameof(Volume), Volume);
        }
    }
}