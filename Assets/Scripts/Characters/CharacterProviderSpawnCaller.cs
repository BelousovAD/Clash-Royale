using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Characters
{
    public class CharacterProviderSpawnCaller : MonoBehaviour
    {
        [SerializeField] private ContainerType _characterContainerType;
        [SerializeField] private ContainerType _equippedCardContainerType;
        [SerializeField] private SingleItemProviderSpawner _providerSpawner;

        private Container _characterContainer;
        private Container _equippedCardContainer;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _characterContainerType)
                {
                    _characterContainer = container;
                    break;
                }
            }

            foreach (Container container in containers)
            {
                if (container.Type == _equippedCardContainerType)
                {
                    _equippedCardContainer = container;
                    _equippedCardContainer.SelectChanged += CallSpawn;
                    break;
                }
            }
        }

        private void OnDisable()
        {
            _equippedCardContainer.SelectChanged -= CallSpawn;
        }

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