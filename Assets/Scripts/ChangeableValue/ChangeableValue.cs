using System;
using Behaviour;

namespace ChangeableValue
{
    public class ChangeableValue<T> : IChangeable
    {
        private T _value;
        
        public event Action Changed;

        public T Value
        {
            get
            {
                return _value;
            }

            protected set
            {
                if (_value.Equals(value) == false)
                {
                    _value = value;
                    Changed?.Invoke();
                }
            }
        }
    }
}