using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Audio
{
    internal class SoundPlayButton : AbstractButton
    {
        private const AudioType SoundType = AudioType.Sound;

        [SerializeField] private AudioClipKey _clipKey;
        [SerializeField] private float _delay;

        private Audio _audio;
        private WaitForSeconds _wait;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

        protected override void Awake()
        {
            base.Awake();
            _wait = new WaitForSeconds(_delay);
        }

        protected override void HandleClick()
        {
            if (_delay == 0)
            {
                _audio.Play(_clipKey);
            }
            else
            {
                StartCoroutine(PlayAfterDelay());
            }
        }

        private IEnumerator PlayAfterDelay()
        {
            yield return _wait;

            _audio.Play(_clipKey);
        }
    }
}