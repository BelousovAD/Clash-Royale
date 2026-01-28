using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Item
{
    internal class SelectedItemProvider : ItemProvider
    {
        [SerializeField] private ContainerType _containerType;
        
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
            _container.SelectChanged += UpdateItem;
            _container.ContentChanged += UpdateItem;
            UpdateItem();
        }

        private void OnDisable()
        {
            _container.SelectChanged -= UpdateItem;
            _container.ContentChanged -= UpdateItem;
        }

        private void UpdateItem(Vector3 position) =>
            Initialize(_container.Selected);
        
        private void UpdateItem() =>
            Initialize(_container.Selected);
    }
}