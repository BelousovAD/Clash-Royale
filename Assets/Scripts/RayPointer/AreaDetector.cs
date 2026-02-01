using System.Collections.Generic;
using CardDrop;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace RayPointer
{
    public class AreaDetector : MonoBehaviour
    {
        private const ContainerType ContainerType = Item.ContainerType.HandCard;

        private Container _container;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == ContainerType)
                {
                    _container = container;
                    break;
                }
            }
        }

        public void FindDropCardArea(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity)
                && hitInfo.collider.TryGetComponent(out DropCardArea area))
            {
                area.Receive();
            }
            else
            {
                _container.Deselect();
            }
        }
    }
}