using System.Collections;
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
        [SerializeField][Min(0f)] private float _delay;

        private Container _container;
        private WaitForSeconds _wait;
        private Coroutine _waitRoutine;

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

        private void Awake() =>
            _wait = new WaitForSeconds(_delay);

        private void OnEnable()
        {
            _container.SelectChanged += Receive;
            Receive();
        }

        private void OnDisable() =>
            _container.SelectChanged -= Receive;

        private void Receive() =>
            _waitRoutine ??= StartCoroutine(ReceiveAfterDelay());

        private IEnumerator ReceiveAfterDelay()
        {
            yield return _wait;
            
            if (_container.Selected is not null)
            {
                _cardReceiver.Receive();
            }

            _waitRoutine = null;
        }
    }
}