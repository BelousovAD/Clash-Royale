using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rarity
{
    [CreateAssetMenu(fileName = nameof(RarityData), menuName = nameof(Rarity) + "/" + nameof(RarityData))]
    internal class RarityData : ScriptableObject
    {
        [SerializeField] private List<RarityColorPair> _rarities;

        public Color GetColor(RarityType rarityType)
        {
            Color result = Color.white;
            int index = _rarities.FindIndex(rare => rare.Type == rarityType);

            if (index != -1)
            {
                result = _rarities[index].Color;
            }

            return result;
        }
        
        [Serializable]
        private struct RarityColorPair
        {
            public RarityType Type;
            public Color Color;
        }
    }
}