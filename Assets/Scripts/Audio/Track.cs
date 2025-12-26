using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct Track
    {
        public AudioClipKey Key;
        public AudioClip Clip;
    }
}