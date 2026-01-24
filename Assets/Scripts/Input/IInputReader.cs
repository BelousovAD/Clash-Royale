using Behaviour;
using ChangeableValue;

namespace Input
{
    public interface IInputReader : IEnable, IDisable
    {
        public ChangeableValue<bool> AttackInput { get; }

        public ChangeableValue<bool> MoveInput { get; }
    }
}