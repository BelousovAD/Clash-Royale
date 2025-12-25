namespace Audio
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct Track
    {
        public AudioClipKey Key;
        public AudioClip Clip;
    }
}