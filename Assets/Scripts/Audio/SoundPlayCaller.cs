namespace Audio
{
    using Reflex.Attributes;
    using UnityEngine;

    internal class SoundPlayCaller : MonoBehaviour
    {
        [SerializeField] private AudioClipKey _clipKey;
        [SerializeField] private bool _isOnEnable;
        [SerializeField] private bool _isOnDisable;

        private Audio _audio;

        [Inject]
        private void Initialize(Sound sound) =>
            _audio = sound;

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