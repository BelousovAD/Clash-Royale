using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardDrop
{
    public class AreaDetector : MonoBehaviour
    {
        private const ContainerType ContainerType = Item.ContainerType.HandCard;

        private Container _container;
        private RayPointer.RayPointer _rayPointer;

        [Inject]
        private void Initialize(IEnumerable<Container> containers, RayPointer.RayPointer rayPointer)
        {
            foreach (Container container in containers)
            {
                if (container.Type == ContainerType)
                {
                    _container = container;
                    break;
                }
            }

            _rayPointer = rayPointer;
        }

        private void OnEnable() =>
            _rayPointer.DragEnded += FindDropCardArea;

        private void OnDisable() =>
            _rayPointer.DragEnded -= FindDropCardArea;

        private void FindDropCardArea()
        {
            if (Physics.Raycast(_rayPointer.Ray, out RaycastHit hitInfo, Mathf.Infinity)
                && hitInfo.collider.TryGetComponent(out CardReceiver receiver))
            {
                receiver.Receive();
            }
            else
            {
                _container.Deselect();
            }
        }
    }
}