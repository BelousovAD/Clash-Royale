using System;

namespace Common
{
    public interface IChangeableValue
    {
        public event Action Changed;
    }
}