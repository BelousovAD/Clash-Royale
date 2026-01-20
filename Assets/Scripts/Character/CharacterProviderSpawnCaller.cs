using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Character
{
    internal class CharacterProviderSpawnCaller : MonoBehaviour
    {
        private const ContainerType CharacterContainerType = ContainerType.Character;
        private const ContainerType EquippedCardContainerType = ContainerType.EquippedCard;

        [SerializeField] private SingleItemProviderSpawner _providerSpawner;

        private Container _characterContainer;
        private Container _equippedCardContainer;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                switch (container.Type)
                {
                    case CharacterContainerType:
                        _characterContainer = container;
                        break;
                    case EquippedCardContainerType:
                        _equippedCardContainer = container;
                        _equippedCardContainer.SelectChanged += CallSpawn;
                        break;
                }
            }
        }

        private void OnDisable() =>
            _equippedCardContainer.SelectChanged -= CallSpawn;

        //УДАЛИТЬ ПОСЛЕ ТЕСТОВ
        public void CallSpawn(string subtype)
        {
            foreach (Item.Item item in _characterContainer.Items)
            {
                if (item.Subtype == subtype)
                {
                    _providerSpawner.Spawn(item);
                }
            }
        }

        private void CallSpawn()
        {
            foreach (Item.Item item in _characterContainer.Items)
            {
                if (item.Subtype == _equippedCardContainer.Selected.Subtype)
                {
                    _providerSpawner.Spawn(item);
                }
            }
        }
    }
}