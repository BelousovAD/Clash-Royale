using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private List<Track> _tracks;

        private readonly Dictionary<AudioClipKey, AudioClip> _audioClips = new ();  
        private AudioSource _source;

        public AudioSource Source => _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();

            foreach (Track track in _tracks)
            {
                _audioClips.Add(track.Key, track.Clip);
            }
        }

        public void PlayTrack(AudioClipKey key)
        {
            _source.clip = _audioClips[key];
            _source.Play();
        }
    }
}