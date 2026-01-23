using System;
using Castle;
using UnityEngine;

namespace Inputs
{
    internal class TowerFounder : MonoBehaviour
    {
        private float _range;
        private Vector2 _currentTarget;

        public Vector2 FindTarget()
        {
            float distance = 1000;
            Collider[] colliders = Physics.OverlapSphere(transform.position, _range);

            foreach (Collider collider in colliders)
            {
                if (Vector3.Distance(transform.position, collider.gameObject.transform.position) < distance)
                {
                    distance = (Vector3.Distance(transform.position, collider.transform.position));
                    _currentTarget = new Vector2(collider.transform.position.x, collider.transform.position.z);
                }
            }

            return _currentTarget;
        }
    }
}