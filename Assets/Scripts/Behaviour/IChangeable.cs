using System;

namespace Behaviour
{
    public interface IChangeable
    {
        public event Action Changed;
    }
}