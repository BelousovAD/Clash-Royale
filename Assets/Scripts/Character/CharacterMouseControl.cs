using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    internal class CharacterMouseControl : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }
    }
}