using System.Collections.Generic;
using System.Linq;
using Chest;
using Item;
using Rarity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reward
{
    internal class ChestRewarder : Rewarder
    {
        private const float MinRandomValue = 0f;
        private const ContainerType ChestContainer = ContainerType.Chest;

        private readonly List<Chance> _chestChances;
        private readonly List<ChestData> _chestDatas;
        private readonly float _totalChance;
        private Container _container;

        public ChestRewarder(RewardData data, IEnumerable<Chance> chestChances, IEnumerable<ChestData> chestDatas)
            : base(data)
        {
            _chestChances = new List<Chance>(chestChances);
            _chestDatas = new List<ChestData>(chestDatas);
            _totalChance = _chestChances.Sum(chance => chance.Percent);
        }
        
        public void Initialize(Gameplay.Judge judge, IEnumerable<Container> containers)
        {
            Initialize(judge);
            
            foreach (Container container in containers)
            {
                if (container.Type == ChestContainer)
                {
                    _container = container;
                    break;
                }
            }
        }

        protected override void ApplyPenalty()
        { }

        protected override void ApplyReward()
        {
            RarityType rarity = GetRandomRarity();
            ChestData chestData = _chestDatas
                .First(chestData => chestData!.Rarity == rarity);
            _container.Add(new Chest.Chest(chestData));
            UpdateIcon(chestData.Icon);
        }

        private RarityType GetRandomRarity()
        {
            float take = Random.Range(MinRandomValue, _totalChance);
            float chanceSum = 0f;

            foreach (Chance chance in _chestChances)
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