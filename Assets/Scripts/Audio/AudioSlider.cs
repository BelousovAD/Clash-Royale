namespace Audio
{
    using Common;

    internal class AudioSlider : AbstractSlider
    {
        private Audio _audio;

        protected void Initialize(Audio audioObject) =>
            _audio = audioObject;

        protected override void Awake()
        {
            base.Awake();
            Slider.value = _audio.Volume;
        }

        protected override void HandleValue(float value) =>
            _audio.SetVolume(value);
    }
}