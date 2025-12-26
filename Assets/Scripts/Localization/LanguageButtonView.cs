using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    [RequireComponent(typeof(Image))]
    public class LanguageButtonView : MonoBehaviour
    {
        [SerializeField] private SwitchLanguageButton _button;
        [SerializeField] private List<Sprite> _flags = new ();

        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        private void OnEnable()
        {
            _button.LanguageIndexChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _button.LanguageIndexChanged -= UpdateView;

        private void UpdateView() =>
            _image.sprite = _flags[_button.Index];
    }
}