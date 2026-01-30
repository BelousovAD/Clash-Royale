using System;
using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UnitHealth
{
    [RequireComponent(typeof(Image))]
    internal class UnitTypeColorImageView : MonoBehaviour
    {
        [SerializeField] private Unit.Unit _unit;
        [SerializeField] private List<UnitTypeColorPair> _colors;

        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        private void OnEnable()
        {
            _unit.TypeChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _unit.TypeChanged -= UpdateView;

        private void UpdateView() =>
            _image.color = _colors.First(color => color.Type == _unit.Type).Color;

        [Serializable]
        private struct UnitTypeColorPair
        {
            public UnitType Type;
            public Color Color;
        }
    }
}