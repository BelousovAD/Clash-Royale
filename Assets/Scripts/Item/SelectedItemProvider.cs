using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Item
{
    public abstract class SelectedItemProvider : ItemProvider
    {
        [SerializeField] private ContainerType _type;
        
        private Container _container;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _type)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _container.SelectChanged += UpdateItem;
            UpdateItem();
        }

        private void OnDisable() =>
            _container.SelectChanged -= UpdateItem;

        private void UpdateItem() =>
            Initialize(_container.Selected);
    }
}