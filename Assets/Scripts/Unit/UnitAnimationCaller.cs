using System;
using FSM;
using UnityEngine;

namespace Unit
{
    internal class UnitAnimationCaller : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private UnitAnimator _unitAnimator;

        private void OnEnable()
        {
            _unit.Initialized += Subscribe;
            Subscribe();
        }

        private void OnDisable() =>
            Unsubscribe();
        
        private void Subscribe()
        {
            if (_unit.StateSwitcher is not null)
            {
                _unit.Initialized -= Subscribe;
                _unit.StateSwitcher.StateSwitched += CallAnimation;
                CallAnimation();
            }
        }

        private void Unsubscribe()
        {
            _unit.Initialized -= Subscribe;

            if (_unit.StateSwitcher is not null)
            {
                _unit.StateSwitcher.StateSwitched -= CallAnimation;
            }
        }

        private void CallAnimation()
        {
            switch (_unit.StateSwitcher.CurrentState.Type)
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