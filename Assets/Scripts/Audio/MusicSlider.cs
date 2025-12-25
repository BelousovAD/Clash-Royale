namespace Audio
{
    using Reflex.Attributes;
    
    internal class MusicSlider : AudioSlider
    {
        [Inject]
        private void Initialize(Music music) =>
            base.Initialize(music);
    }
}