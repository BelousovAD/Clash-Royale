using System.Collections.Generic;
using CardDrop;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace ArtificialOpponent
{
    internal class EnemyCardReceiveCaller : MonoBehaviour
    {
        private const ContainerType EnemyHandCardContainer = ContainerType.EnemyHandCard;
        
        [SerializeField] private CardReceiver _cardReceiver;

        private Container _container;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == EnemyHandCardContainer)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _container.SelectChanged += CallReceive;
            CallReceive();
        }

        private void OnDisable() =>
            _container.SelectChanged -= CallReceive;

        private void CallReceive()
        {
            if (_container.Selected is not null)
            {
                _cardReceiver.Receive();
            }
        }
    }
}