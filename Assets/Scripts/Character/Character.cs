using UnityEditor.Animations;
using UnityEngine;

namespace Character
{
    public class Character : Item.Item
    {
        public Character(CharacterData data, int id)
            : base(data, id)
        { }

        public AnimatorController AnimatorController => Data.AnimatorController;

        public Avatar Avatar => Data.Avatar;

        public int Damage => Data.Damage;
        
        public int MoveSpeed => Data.MoveSpeed;
        
        public int AttackSpeed => Data.AttackSpeed;
        
        public int Health => Data.Health;
        
        public int AttackRange => Data.AttackRange;

        public GameObject Prefab => Data.Prefab;
        
        private new CharacterData Data => base.Data as CharacterData;
    }
}