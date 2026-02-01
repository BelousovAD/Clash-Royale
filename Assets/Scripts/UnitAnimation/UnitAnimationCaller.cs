using System;
using FSM;
using UnityEngine;

namespace UnitAnimation
{
    internal class UnitAnimationCaller : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        
        private UnitAnimator _unitAnimator;
        private IStateSwitcher _stateSwitcher;

        public void Initialize(UnitAnimator unitAnimator)
        {
            _unitAnimator = unitAnimator;
            CallAnimation();
        }

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable() =>
            _unit.Initialized -= UpdateSubscriptions;

        private void Subscribe() =>
            _unit.StateSwitcher.StateSwitched += CallAnimation;

        private void Unsubscribe() =>
            _unit.StateSwitcher.StateSwitched -= CallAnimation;

        private void UpdateSubscriptions()
        {
            if (_stateSwitcher is not null)
            {
                Unsubscribe();
            }

            _stateSwitcher = _unit.StateSwitcher;

            if (_stateSwitcher is not null)
            {
                Subscribe();
            }
            
            CallAnimation();
        }

        private void CallAnimation()
        {
            if (_unitAnimator is null || _stateSwitcher is null)
            {
                return;
            }
            
            switch (_stateSwitcher.CurrentState.Type)
            {
                case StateType.Idle:
                    _unitAnimator.Play(AnimationKey.Idle);
                    break;
                case StateType.Attack:
                    _unitAnimator.Play(AnimationKey.Attack);
                    break;
                case StateType.Move:
                    _unitAnimator.Play(AnimationKey.Run);
                    break;
                case StateType.Die:
                    _unitAnimator.Play(AnimationKey.Die);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}