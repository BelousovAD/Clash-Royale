using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardDrop
{
    internal class CardReceiverSwitcher : MonoBehaviour
    {
        private const ContainerType HandCardContainerType = ContainerType.HandCard;

        [SerializeField] private CardReceiver _bottomArea;
        [SerializeField] private CardReceiver _topArea;

        private Container _container;
        private Item.Item _currentSelect;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == HandCardContainerType)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void OnEnable() =>
            _container.SelectChanged += ToggleCardArea;

        private void OnDisable() =>
            _container.SelectChanged -= ToggleCardArea;

        private void ToggleCardArea() =>
            _bottomArea.gameObject.SetActive(_container.Selected != null);
    }
}