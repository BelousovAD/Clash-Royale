using System.Collections.Generic;
using System.Linq;
using Rarity;
using UnityEngine;

namespace Chest
{
    public class Chest : Item.Item
    {
        private const float MinRandomValue = 0f;
        
        private readonly float _totalChance;

        public Chest(ChestData data)
            : base(data) =>
            _totalChance = Chances.Sum(chance => chance.Percent);

        public IReadOnlyList<Chance> Chances => Data.Chances;

        public GameObject Prefab => Data.Prefab;

        private new ChestData Data => base.Data as ChestData;

        public RarityType GetRandomRarity()
        {
            float take = Random.Range(MinRandomValue, _totalChance);
            float chanceSum = 0f;

            foreach (Chance chance in Chances)
            {
                chanceSum += chance.Percent;

                if (take < chanceSum)
                {
                    return chance.Rarity;
                }
            }

            return RarityType.Legendary;
        }
    }
}