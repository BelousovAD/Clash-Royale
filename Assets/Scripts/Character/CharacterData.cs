using Item;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = nameof(CharacterData), menuName = nameof(Character) + "/" + nameof(CharacterData))]
    public class CharacterData : ItemData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField][Min(0f)] private float _radius = 0.5f;
        [SerializeField][Min(0)] private int _health;
        [SerializeField] private int _healthChangeAmount;
        [SerializeField][Min(0f)] private float _healthChangeDelay; 
        [SerializeField][Min(0f)] private float _attackSpeed = 1f;
        [SerializeField][Min(0f)] private float _moveSpeed = 1f;
        [SerializeField][Min(0f)] private float _attackRange = 1f;

        public int HealthChangeAmount => _healthChangeAmount;
        
        public int Health => _health;
        
        public float AttackSpeed => _attackSpeed;
        
        public float MoveSpeed => _moveSpeed;

        public float AttackRange => _attackRange;
        
        public GameObject Prefab => _prefab;
        
        public float Radius => _radius;
        
        public float HealthChangeDelay => _healthChangeDelay;
    }
}