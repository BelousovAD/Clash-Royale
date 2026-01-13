using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [RequireComponent(typeof(Image))]
    public class CrownIconView : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private Sprite _activeIcon;
        [SerializeField] private CrownType _type;
        [SerializeField][Min(1)] private int _number = 1;
        
        private Image _image;
        private CrownCounter _counter;

        [Inject]
        private void Initialize(IEnumerable<CrownCounter> crownCounters)
        {
            foreach (CrownCounter counter in crownCounters)
            {
                if (counter.Type == _type)
                {
                    _counter = counter;
                    break;
                }
            }
        }

        private void Awake() =>
            _image = GetComponent<Image>();

        private void OnEnable() =>
            UpdateView();

        private void UpdateView() =>
            _image.sprite = _counter.Count >= _number ? _activeIcon : _defaultIcon;
    }
}