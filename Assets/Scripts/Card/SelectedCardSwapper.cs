using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace Card
{
    public class SelectedCardSwapper : MonoBehaviour
    {
        [SerializeField] private ContainerType _firstContainerType;
        [SerializeField] private ContainerType _secondContainerType;
        
        private Container _firstContainer;
        private Container _secondContainer;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _firstContainerType)
                {
                    _firstContainer = container;
                }
                
                if (container.Type == _secondContainerType)
                {
                    _secondContainer = container;
                }
            }
        }

        private void OnEnable()
        {
            _firstContainer.SelectChanged += Swap;
            _secondContainer.SelectChanged += Swap;
            Swap();
        }

        private void OnDisable()
        {
            _firstContainer.SelectChanged -= Swap;
            _secondContainer.SelectChanged -= Swap;
        }

        private void Swap()
        {
            Item.Item firstItem = _firstContainer.Selected;
            Item.Item secondItem = _secondContainer.Selected;

            if (firstItem is null || secondItem is null)
            {
                return;
            }
            
            _firstContainer.ReplaceSelected(secondItem);
            _firstContainer.Deselect();
            _secondContainer.ReplaceSelected(firstItem);
            _secondContainer.Deselect();
        }
    }
}