using System;
using TMPro;
using UnityEngine;

namespace Item
{
    [RequireComponent(typeof(TMP_Text))]
    internal abstract class ItemTextView<T> : ItemView<T>
        where T : Enum
    {
        [SerializeField] private string _format = "{0}";
        
        protected TMP_Text TextField { get; private set; }

        protected string Format => _format;

        protected virtual void Awake() =>
            TextField = GetComponent<TMP_Text>();
    }
}