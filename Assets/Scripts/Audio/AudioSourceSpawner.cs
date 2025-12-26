using Spawn;

namespace Audio
{
    public class AudioSourceSpawner : SiblingsSpawner
    {
        public new PooledAudioSource Spawn() =>
            base.Spawn().GetComponent<PooledAudioSource>();
    }
}