using Item;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = nameof(CharacterData), menuName = nameof(Character) + "/" + nameof(CharacterData))]
    public class CharacterData : ItemData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField][Min(0f)] private float _radius;
        [SerializeField][Min(0)] private int _health;
        [SerializeField][Min(0)] private int _damage;
        [SerializeField][Min(0)] private int _attackSpeed;
        [SerializeField][Min(0)] private int _moveSpeed;
        [SerializeField][Min(1)] private int _attackRange;

        public int Damage => _damage;
        
        public int Health => _health;
        
        public int AttackSpeed => _attackSpeed;
        
        public int MoveSpeed => _moveSpeed;

        public int AttackRange => _attackRange;
        
        public GameObject Prefab => _prefab;
        
        public float Radius => _radius;
    }
}