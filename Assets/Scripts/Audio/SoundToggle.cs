namespace Audio
{
    using Reflex.Attributes;

    internal class SoundToggle : AudioToggle
    {
        [Inject]
        private void Initialize(Sound sound) =>
            base.Initialize(sound);
    }
}