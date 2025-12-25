namespace Common
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Toggle))]
    public abstract class AbstractToggle : MonoBehaviour
    {
        protected Toggle Toggle { get; private set; }

        protected virtual void Awake() =>
            Toggle = GetComponent<Toggle>();

        protected virtual void OnEnable() =>
            Toggle.onValueChanged.AddListener(HandleValue);

        protected virtual void OnDisable() =>
            Toggle.onValueChanged.RemoveListener(HandleValue);

        protected abstract void HandleValue(bool value);
    }
}