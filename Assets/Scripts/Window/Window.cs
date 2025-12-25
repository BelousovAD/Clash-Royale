namespace Window
{
    using System;
    using UnityEngine;

    internal class Window : MonoBehaviour
    {
        [SerializeField] private string _id;
        
        private bool _isActive;
        private bool _isInteractable;

        public event Action ActivityChanged;
        public event Action InteractableChanged;

        public string Id => _id;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            private set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    ActivityChanged?.Invoke();
                }
            }
        }

        public bool IsInteractable
        {
            get
            {
                return _isInteractable;
            }

            private set
            {
                if (value != _isInteractable)
                {
                    _isInteractable = value;
                    InteractableChanged?.Invoke();
                }
            }
        }

        public void SetActive(bool value) =>
            IsActive = value;

        public void SetInteractable(bool value) =>
            IsInteractable = value;
    }
}