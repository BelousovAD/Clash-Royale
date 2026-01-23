using System.Collections.Generic;
using Castle;
using UnityEngine;

namespace Character
{
    public class CharacterTargetChooser : MonoBehaviour
    {
        [SerializeField] private float _visionRadius;

        private Tower _target;
        private Transform _transform;
        
        private void OnEnable()
        {
            _transform = transform;
        }

        public Tower SwitchTarget()
        {
            float distance = _visionRadius;
            List<Tower> towers = new List<Tower>();
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _visionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Tower tower))
                {
                    towers.Add(tower);
                }
            }

            if (towers.Count != 0)
            {
                foreach (Tower tower in towers)
                {
                    if (distance > Vector3.Distance(_transform.position, tower.transform.position))
                    {
                        distance = Vector3.Distance(_transform.position, tower.transform.position);
                        _target = tower;
                    }
                }
            }
            
            return _target;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _visionRadius);
        }
    }
}