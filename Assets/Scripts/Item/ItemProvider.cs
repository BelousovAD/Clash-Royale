using System;
using UnityEngine;

namespace Item
{
    internal class ItemProvider : MonoBehaviour
    {
        private Item _item;
        
        public event Action Changed;

        public Item Item
        {
            get
            {
                return _item;
            }

            private set
            {
                _item = value;
                Changed?.Invoke();
            }
        }

        public void Initialize(Item item) =>
            Item = item;
    }
}