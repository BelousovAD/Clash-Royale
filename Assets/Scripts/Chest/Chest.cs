using Rarity;

namespace Chest
{
    using System.Linq;
    using Item;
    using UnityEngine;

    internal class Chest : Item
    {
        private const float MinRandomValue = 0f;
        
        private readonly float _totalChance;

        public Chest(ChestData data)
            : base(data) =>
            _totalChance = Data.Chances.Sum(chance => chance.Percent);

        private new ChestData Data => base.Data as ChestData;

        public RarityType GetRandomRarity()
        {
            float take = Random.Range(MinRandomValue, _totalChance);
            float chanceSum = 0f;

            foreach (Chance chance in Data.Chances)
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