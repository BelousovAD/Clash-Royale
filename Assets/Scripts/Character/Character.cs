using UnityEngine;

namespace Character
{
    internal class Character : Item.Item
    {
        public Character(CharacterData data, int id)
            : base(data, id)
        { }

        public int Damage => Data.Damage;
        
        public int Health => Data.Health;
        
        public int MoveSpeed => Data.MoveSpeed;
        
        public int AttackSpeed => Data.AttackSpeed;
        
        public GameObject Prefab => Data.Prefab;
        
        private new CharacterData Data => base.Data as CharacterData;
    }
}