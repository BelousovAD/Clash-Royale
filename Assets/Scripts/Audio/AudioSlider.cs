using System.Collections.Generic;
using System.Linq;
using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Audio
{
    internal class AudioSlider : AbstractSlider
    {
        [SerializeField] private AudioType _type;

        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == _type);

        protected override void Awake()
        {
            base.Awake();
            Slider.value = _audio.Volume;
        }

        protected override void HandleValue(float value) =>
            _audio.SetVolume(value);
    }
}