using UnityEngine;

namespace Character
{
    public class Character : Item.Item
    {
        public Character(CharacterData data, int id)
            : base(data, id)
        { }

        public int Damage => Data.Damage;
        
        public float MoveSpeed => Data.MoveSpeed;
        
        public float AttackSpeed => Data.AttackSpeed;
        
        public int Health => Data.Health;
        
        public float AttackRange => Data.AttackRange;

        public GameObject Prefab => Data.Prefab;

        public float Radius => Data.Radius;
        
        private new CharacterData Data => base.Data as CharacterData;
    }
}