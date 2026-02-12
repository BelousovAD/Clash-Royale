using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Character
{
    public class CharacterProviderSpawnCaller : MonoBehaviour
    {
        private const ContainerType CharacterContainerType = ContainerType.Character;

        [SerializeField] private SingleItemProviderSpawner _providerSpawner;

        private Container _characterContainer;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == CharacterContainerType)
                {
                    _characterContainer = container;
                    break;
                }
            }
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
    }
}