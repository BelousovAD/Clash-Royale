using System.Collections.Generic;
using System.Linq;
using Chest;
using Currency;
using Item;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace Reward
{
    internal class RewardInstaller : MonoBehaviour, IInstaller
    {
        private const ItemType ChestItem = ItemType.Chest;

        [SerializeField] private ChestChanceData _chestChanceData;
        [SerializeField] private ItemDataList _fullChestList;
        [SerializeField] private List<RewardData> _rewardDatas = new ();
        
        private ContainerBuilder _builder;
        private ChestRewarder _chestRewarder;
        private List<CurrencyRewarder> _currencyRewarders;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _chestRewarder = new ChestRewarder(
                _rewardDatas.Find(data => data.Type == RewardType.Chest),
                _chestChanceData.Chances,
                _fullChestList.ItemDatas.Select(itemData => itemData as ChestData));
            _currencyRewarders = new List<CurrencyRewarder>
            {
                new (_rewardDatas.Find(data => data.Type == RewardType.Money), CurrencyType.Money),
                new (_rewardDatas.Find(data => data.Type == RewardType.Trophy), CurrencyType.Trophy),
            };
            
            _builder.AddSingleton(_chestRewarder, typeof(Rewarder));
            _currencyRewarders.ForEach(rewarder => _builder.AddSingleton(rewarder, typeof(Rewarder)));

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _chestRewarder.Initialize(
                container.Resolve<Gameplay.Judge>(),
                container.Resolve<IEnumerable<Container>>());
            _currencyRewarders.ForEach(rewarder => rewarder.Initialize(
                container.Resolve<Gameplay.Judge>(),
                container.Resolve<IEnumerable<Currency.Currency>>()));
        }
        
        private void OnValidate()
        {
            if (_fullChestList is not null && _fullChestList.Type != ChestItem)
            {
                Debug.LogError($"Require {nameof(_fullChestList)} with {nameof(_fullChestList.Type)}:{ChestItem}");
                _fullChestList = null;
            }
        }
    }
}