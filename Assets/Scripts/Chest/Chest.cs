using System;
using System.Collections.Generic;
using System.Linq;
using Rarity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chest
{
    public class Chest : Item.Item
    {
        private const float MinRandomValue = 0f;
        
        private readonly float _totalChance;
        private bool _isLocked;

        public Chest(ChestData data)
            : base(data) =>
            _totalChance = Chances.Sum(chance => chance.Percent);

        public event Action LockStatusChanged;

        public IReadOnlyList<Chance> Chances => Data.Chances;

        public GameObject Prefab => Data.Prefab;

        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }

            private set
            {
                if (value != _isLocked)
                {
                    _isLocked = value;
                    Save();
                    LockStatusChanged?.Invoke();
                }
            }
        }

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