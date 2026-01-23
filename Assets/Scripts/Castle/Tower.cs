using System;
using UnityEngine;

namespace Castle
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _damage;

        public int Health => _health;

        public event Action<Tower> Destroyed;

        private void Update()
        {
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}