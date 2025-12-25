namespace Audio
{
    using Common;
    using Reflex.Attributes;
    using UnityEngine;

    internal class SoundPlayButton : AbstractButton
    {
        [SerializeField] private AudioClipKey _clipKey;
        
        private Sound _sound;

        [Inject]
        private void Initialize(Sound sound) =>
            _sound = sound;
        
        protected override void HandleClick() =>
            _sound.Play(_clipKey);
    }
}