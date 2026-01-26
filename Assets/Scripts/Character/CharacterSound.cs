using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Character
{
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private List<Track> _tracks;
        
        private AudioSource _source;

        private void OnEnable()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlayTrack(AudioClipKey key)
        {
            foreach (Track track in _tracks)
            {
                if (track.Key == key)
                {
                    _source.clip = track.Clip;
                }
            }
            
            _source.Play();
        }
    }
}