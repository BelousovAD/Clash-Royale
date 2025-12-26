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

        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);

        protected override void HandleClick() =>
            _audio.Play(_clipKey);
    }
}