using System;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    [RequireComponent(typeof(Image))]
    internal abstract class ItemImageView<T> : ItemView<T>
        where T : Enum
    {
        [SerializeField] private Sprite _defaultSprite;
        
        protected Image Image { get; private set; }

        protected Sprite DefaultSprite => _defaultSprite;

        protected virtual void Awake() =>
            Image = GetComponent<Image>();
    }
}