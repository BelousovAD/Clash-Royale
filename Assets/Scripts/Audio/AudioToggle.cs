namespace Audio
{
    using Common;

    internal class AudioToggle : AbstractToggle
    {
        private Audio _audio;

        protected void Initialize(Audio audioObject) =>
            _audio = audioObject;

        protected override void Awake()
        {
            base.Awake();
            Toggle.isOn = _audio.IsActive;
        }

        protected override void HandleValue(bool value) =>
            _audio.SetActive(value);
    }
}