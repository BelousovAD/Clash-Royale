using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Unit
{
    internal class StateObjectSwitcher : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private StateType _stateType;
        [SerializeField] private List<GameObject> _enableObjects;
        [SerializeField] private List<GameObject> _disableObjects;

        private IStateSwitcher _stateSwitcher;

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable() =>
            _unit.Initialized -= UpdateSubscriptions;

        private void Subscribe() =>
            _stateSwitcher.StateSwitched += SwitchObjects;

        private void Unsubscribe() =>
            _stateSwitcher.StateSwitched -= SwitchObjects;

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

            SwitchObjects();
        }

        private void SwitchObjects()
        {
            if (_stateSwitcher is null)
            {
                return;
            }
            
            if (_stateSwitcher.CurrentState.Type == _stateType)
            {
                _disableObjects.ForEach(obj => obj.SetActive(false));
                _enableObjects.ForEach(obj => obj.SetActive(true));
            }
            else
            {
                _enableObjects.ForEach(obj => obj.SetActive(false));
                _disableObjects.ForEach(obj => obj.SetActive(true));
            }
        }
    }
}