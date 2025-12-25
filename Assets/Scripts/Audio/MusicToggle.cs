namespace Audio
{
    using Reflex.Attributes;

    internal class MusicToggle : AudioToggle
    {
        [Inject]
        private void Initialize(Music music) =>
            base.Initialize(music);
    }
}