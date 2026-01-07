using System;
using UnityEngine;

namespace Item
{
    internal class ItemProvider<T> : MonoBehaviour
        where T : Enum
    {
        private Item<T> _item;
        
        public event Action Changed;

        public Item<T> Item
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

        public void Initialize(Item<T> item) =>
            Item = item;
    }
}