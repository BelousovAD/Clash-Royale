using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rarity
{
    [CreateAssetMenu(fileName = nameof(RarityData), menuName = nameof(Rarity) + "/" + nameof(RarityData))]
    public class RarityData : ScriptableObject
    {
        public static readonly Color DefaultColor = Color.white;
        
        [SerializeField] private List<RarityColorPair> _rarities;

        public Color GetColor(RarityType rarityType)
        {
            Color result = DefaultColor;
            int index = _rarities.FindIndex(rarity => rarity.Type == rarityType);

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