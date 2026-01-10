using System;
using Rarity;

namespace Chest
{
    [Serializable]
    internal struct Chance
    {
        public float Percent;
        public RarityType Rarity;
    }
}