using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class ToggleSelectionView : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        private void Awake() =>
            _toggle.onValueChanged.AddListener(UpdateView);

        private void OnDestroy() =>
            _toggle.onValueChanged.RemoveListener(UpdateView);

        private void UpdateView(bool isOn) =>
            gameObject.SetActive(isOn);
    }
}