using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    [RequireComponent(typeof(Slider))]
    public abstract class AbstractSlider : MonoBehaviour
    {
        protected Slider Slider { get; private set; }

        protected virtual void Awake() =>
            Slider = GetComponent<Slider>();

        protected virtual void OnEnable() =>
            Slider.onValueChanged.AddListener(HandleValue);

        protected virtual void OnDisable() =>
            Slider.onValueChanged.RemoveListener(HandleValue);

        protected abstract void HandleValue(float value);
    }
}