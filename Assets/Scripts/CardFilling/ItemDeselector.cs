using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardFilling
{
    internal class ItemDeselector : MonoBehaviour
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
        
        private void OnDisable() =>
            _container.Deselect();
    }
}