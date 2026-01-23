using System;
using Castle;
using UnityEngine;

namespace Character
{
    public class AttackTrigger : MonoBehaviour
    {
        public event Action<Tower> FoundEnemy;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Tower tower))
            {
                FoundEnemy?.Invoke(tower);
            }
        }
    }
}