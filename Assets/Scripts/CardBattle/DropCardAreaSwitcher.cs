using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardBattle
{
    internal class DropCardAreaSwitcher : MonoBehaviour
    {
        [SerializeField] private List<DropCardArea> _areas;
        [SerializeField] private CardIdentifier _identifier;

        private void OnEnable()
        {
            _identifier.Picked += TurnOnCardArea;
            _identifier.Droped += TurnOffCardArea;
        }

        private void OnDisable()
        {
            _identifier.Picked -= TurnOnCardArea;
            _identifier.Droped -= TurnOffCardArea;
        }

        private void TurnOnCardArea()
        {
            _areas[0].gameObject.SetActive(true);
        }

        private void TurnOffCardArea()
        {
            _areas[0].gameObject.SetActive(false);
        }
    }
}