using Item;
using UnityEditor.Animations;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = nameof(CharacterData), menuName = nameof(Character) + "/" + nameof(CharacterData))]
    public class CharacterData : ItemData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private AnimatorController _animatorController;
        [SerializeField] private Avatar _avatar;
        [SerializeField][Min(0)] private int _health;
        [SerializeField][Min(0)] private int _damage;
        [SerializeField][Min(0)] private int _attackSpeed;
        [SerializeField][Min(0)] private int _moveSpeed;
        [SerializeField][Min(1)] private int _attackRange;

        public AnimatorController AnimatorController => _animatorController;

        public Avatar Avatar => _avatar;

        public int Damage => _damage;
        
        public int Health => _health;
        
        public int AttackSpeed => _attackSpeed;
        
        public int MoveSpeed => _moveSpeed;

        public int AttackRange => _attackRange;
        
        public GameObject Prefab => _prefab;
    }
}