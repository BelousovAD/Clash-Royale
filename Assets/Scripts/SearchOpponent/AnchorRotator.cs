using System;
using UnityEngine;

namespace SearchOpponent
{
    
    [RequireComponent(typeof(RectTransform))]
    internal class AnchorRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private RectTransform _anchor;
        [SerializeField] private float _radius;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _rectTransform.position = _anchor.position + new Vector3(_radius, 0f, 0f);
        }

        private void Update()
        {
            _rectTransform.RotateAround(_anchor.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}