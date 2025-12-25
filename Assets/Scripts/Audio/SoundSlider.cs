namespace Audio
{
    using Reflex.Attributes;

    internal class SoundSlider : AudioSlider
    {
        [Inject]
        private void Initialize(Sound sound) =>
            base.Initialize(sound);
    }
}