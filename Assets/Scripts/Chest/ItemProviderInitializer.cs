using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Chest
{
    internal class ItemProviderInitializer : MonoBehaviour
    {
        [SerializeField] private ContainerType _containerType;
        [SerializeField] private List<ItemProvider> _itemProviders;

        private Container _container;
        
        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _containerType)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _container.ContentChanged += Initialize;
            Initialize();
        }

        private void OnDisable() =>
            _container.ContentChanged -= Initialize;

        private void Initialize()
        {
            for (int i = 0; i < _itemProviders.Count; i++)
            {
                _itemProviders[i].Initialize(i < _container.Items.Count ? _container.Items[i] : null);
            }
        }
    }
}