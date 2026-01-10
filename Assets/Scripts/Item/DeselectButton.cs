using System.Collections.Generic;
using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Item
{
    internal class DeselectButton : AbstractButton
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
        
        protected override void HandleClick() =>
            _container.Deselect();
    }
}