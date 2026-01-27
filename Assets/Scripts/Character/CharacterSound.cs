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

        public AudioSource Source { get; private set; }

        private void Awake()
        {
            Source = GetComponent<AudioSource>();

            foreach (Track track in _tracks)
            {
                _audioClips.Add(track.Key, track.Clip);
            }
        }

        public void PlayTrack(AudioClipKey key)
        {
            Source.clip = _audioClips[key];
            Source.Play();
        }
    }
}