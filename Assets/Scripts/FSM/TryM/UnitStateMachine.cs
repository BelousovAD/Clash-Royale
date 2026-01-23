using Castle;
using UnityEngine;

namespace FSM
{
   /* [RequireComponent(typeof(CharacterTargetChooser))]
    [RequireComponent(typeof(CharacterNavController))]
    [RequireComponent(typeof(AttackTrigger))]
    internal class UnitStateMachine : MonoBehaviour
    {
        private CharacterTargetChooser _targetChooser;
        private CharacterNavController _controller;
        private AttackTrigger _attackTrigger;
        private Tower _target;

        private void Start()
        {
            _targetChooser = GetComponent<CharacterTargetChooser>();
            _controller = GetComponent<CharacterNavController>();
            _attackTrigger = GetComponent<AttackTrigger>();

            _attackTrigger.FoundEnemy += AttackTarget;
            SwitchTarget();
        }

        private void AttackTarget(Tower tower)
        {
            _controller.AttackUnit(tower);
        }

        private void SwitchTarget()
        {
            _target = _targetChooser.SwitchTarget();
            _target.Destroyed += OnDestroyTarget;
            _controller.SendUnit(_target.transform.position);
        }

        private void OnDestroyTarget(Tower lastTarget)
        {
            _controller.StopAttack();
            _target.Destroyed -= OnDestroyTarget;
            _target = null;
            Destroy(lastTarget.gameObject);
            SwitchTarget();
        }
    }*/
}