using System;
using Rarity;

namespace Card
{
    public class Card : Item.Item
    {
        private bool _isLocked = true;
        
        public Card(CardData data, int id)
            : base(data, id)
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

        private new CardData Data => base.Data as CardData;

        public void Unlock() =>
            IsLocked = false;

        public override void Load()
        {
            base.Load();
            _isLocked = Services.Preferences.LoadBool(Type + Subtype + Id, Data.IsLocked);
        }

        protected override void DeleteSaves()
        {
            base.DeleteSaves();
            Services.Preferences.DeleteKey(Type + Subtype + Id);
        }

        protected override void Save()
        {
            Services.Preferences.SaveBool(Type + Subtype + Id, IsLocked);
            base.Save();
        }
    }
}