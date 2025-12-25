namespace Audio
{
    using System.Collections.Generic;
    using UnityEngine.Audio;

    public class Music : Audio
    {
        public Music(AudioMixerGroup group, AudioSourceSpawner spawner, IEnumerable<Track> tracks)
            : base(AudioType.Music, group, spawner, tracks)
        { }
    }
}