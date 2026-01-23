using System;

namespace Common
{
    public class ChangeableValue<T> : IChangeableValue
    {
        private T _value;
        
        public event Action Changed;

        public virtual T Value
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