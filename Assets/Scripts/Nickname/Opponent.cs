using System;

namespace Nickname
{
    internal class Opponent
    {
        private string _name;

        public Opponent(OpponentType type) =>
            Type = type;

        public event Action Renamed;

        public string Name
        {
            get
            {
                return _name;
            }

            private set
            {
                if (value != _name)
                {
                    _name = value;
                    Renamed?.Invoke();
                }
            }
        }

        public OpponentType Type { get; }

        public void Rename(string name) =>
            Name = name;
    }
}