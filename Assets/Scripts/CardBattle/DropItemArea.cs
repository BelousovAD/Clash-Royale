using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CardBattle
{
    [RequireComponent(typeof(Image))]
    internal class DropItemArea : MonoBehaviour
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

        public void Receive() =>
            _container.RemoveAt(_container.Index);
    }
}