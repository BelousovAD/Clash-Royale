using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Character
{
    internal class CharacterProviderSpawnCaller : MonoBehaviour
    {
        private const ContainerType CharacterContainerType = ContainerType.Character;
        private const ContainerType HandCardContainerType = ContainerType.HandCard;

        [SerializeField] private SingleItemProviderSpawner _providerSpawner;

        private Container _characterContainer;
        private Container _cardContainer;

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
                    case HandCardContainerType:
                        _cardContainer = container;
                        break;
                }
            }
            
            _cardContainer.SelectChanged += CallSpawn;
        }

        private void OnDisable() =>
            _cardContainer.SelectChanged -= CallSpawn;

        private void CallSpawn(Vector3 position)
        {
            foreach (Item.Item item in _characterContainer.Items)
            {
                if (item.Subtype == _cardContainer.Selected.Subtype)
                {
                    _providerSpawner.Spawn(item, position);
                }
            }
        }
    }
}