using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using UnityEngine;

namespace Audio
{
    internal class SoundPlayCaller : MonoBehaviour
    {
        private const AudioType SoundType = AudioType.Sound;

        [SerializeField] private AudioClipKey _clipKey;
        [SerializeField] private bool _isOnEnable;
        [SerializeField] private bool _isOnDisable;

        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

        private void OnEnable()
        {
            if (_isOnEnable)
            {
                _audio.Play(_clipKey);
            }
        }

        private void OnDisable()
        {
            if (_isOnDisable)
            {
                _audio.Play(_clipKey);
            }
        }
    }
}