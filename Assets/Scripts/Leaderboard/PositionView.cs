using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    internal class PositionView : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem _item;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private List<Sprite> _positionSprites = new ();

        private void OnEnable()
        {
            _item.Initialized += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _item.Initialized -= UpdateView;

        private void UpdateView()
        {
            _image.enabled = false;
            _textField.enabled = false;

            if (_item.Position <= _positionSprites.Count)
            {
                _image.sprite = _positionSprites[_item.Position - 1];
                _image.enabled = true;
            }
            else
            {
                _textField.text = _item.Position.ToString();
                _textField.enabled = true;
            }
        }
    }
}