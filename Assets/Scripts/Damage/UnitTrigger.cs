using System;
using UnityEngine;

namespace Damage
{
    [RequireComponent(typeof(Collider))]
    internal class UnitTrigger : MonoBehaviour
    {
        private Unit.Unit _value;
        
        public event Action Changed;

        public Unit.Unit Value
        {
            get
            {
                return _value;
            }

            private set
            {
                if (value != _value)
                {
                    _value = value;
                    Changed?.Invoke();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Unit.Unit unit))
            {
                Value = unit;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Unit.Unit unit)
                && unit == Value)
            {
                Value = null;
            }
        }
    }
}