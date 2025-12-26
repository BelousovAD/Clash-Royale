using System.Collections.Generic;
using System.Linq;
using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Audio
{
    internal class AudioToggle : AbstractToggle
    {
        [SerializeField] private AudioType _type;

        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == _type);

        protected override void Awake()
        {
            base.Awake();
            Toggle.isOn = _audio.IsActive;
        }

        protected override void HandleValue(bool value) =>
            _audio.SetActive(value);
    }
}