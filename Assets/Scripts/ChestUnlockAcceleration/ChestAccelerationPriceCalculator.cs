using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace ChestUnlockAcceleration
{
    internal class ChestAccelerationPriceCalculator : MonoBehaviour
    {
        private const ContainerType ChestContainer = ContainerType.Chest;

        private Container _container;
        private List<Chest.Chest> _chests = new ();
        private int _price;
        
        public event Action PriceChanged;

        public int Price
        {
            get
            {
                return _price;
            }

            private set
            {
                if (value != _price)
                {
                    _price = value;
                    PriceChanged?.Invoke();
                }
            }
        }

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == ChestContainer)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _container.ContentChanged += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _container.ContentChanged -= UpdateSubscriptions;
            Unsubscribe();
        }

        private void CalculatePrice() =>
            Price = _chests.Where(chest => chest.IsUnlocking).Sum(chest => chest.Price);

        private void Subscribe() =>
            _chests.ForEach(chest => chest.UnlockingStatusChanged += CalculatePrice);

        private void Unsubscribe() =>
            _chests.ForEach(chest => chest.UnlockingStatusChanged -= CalculatePrice);

        private void UpdateSubscriptions()
        {
            Unsubscribe();
            _chests = new List<Chest.Chest>(_container.Items.Select(item => item as Chest.Chest));
            Subscribe();
            CalculatePrice();
        }
    }
}