using System;
using Rarity;

namespace Card
{
    internal class Card : Item.Item
    {
        private bool _isLocked = true;
        
        public Card(CardData data)
            : base(data)
        { }

        public event Action LockStatusChanged;

        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }

            private set
            {
                if (value != _isLocked)
                {
                    _isLocked = value;
                    Save();
                    LockStatusChanged?.Invoke();
                }
            }
        }

        public int Price => Data.Price;

        public RarityType Rarity => Data.Rarity;

        protected new CardData Data => base.Data as CardData;

        public void Unlock() =>
            IsLocked = false;

        public override void Load()
        {
            base.Load();
            IsLocked = Services.Preferences.LoadBool(Data.Type + Data.Subtype, Data.IsLocked);
        }

        protected override void Save()
        {
            Services.Preferences.SaveBool(Data.Type + Data.Subtype, IsLocked);
            base.Save();
        }
    }
}