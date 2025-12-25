namespace Audio
{
    using Spawn;

    public class AudioSourceSpawner : SiblingsSpawner
    {
        public new PooledAudioSource Spawn() =>
            base.Spawn().GetComponent<PooledAudioSource>();
    }
}