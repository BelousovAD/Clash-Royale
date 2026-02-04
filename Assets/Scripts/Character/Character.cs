using UnityEngine;

namespace Character
{
    public class Character : Item.Item
    {
        public Character(CharacterData data, int id)
            : base(data, id)
        { }

        public int HealthChangeAmount => Data.HealthChangeAmount;
        
        public float MoveSpeed => Data.MoveSpeed;
        
        public float AttackSpeed => Data.AttackSpeed;
        
        public int Health => Data.Health;
        
        public float AttackRange => Data.AttackRange;

        public GameObject Prefab => Data.Prefab;

        public float Radius => Data.Radius;

        public float HealthChangeDelay => Data.HealthChangeDelay;

        public bool IsTargetOnlyTower => Data.IsTargetOnlyTower;
        
        private new CharacterData Data => base.Data as CharacterData;
    }
}