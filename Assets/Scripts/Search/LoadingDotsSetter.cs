using System.Collections;
using TMPro;
using UnityEngine;

namespace Search
{
    public class LoadingDotsSetter : MonoBehaviour
    {
        private static readonly char _dot = '.';

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _delay = 0.5f;

        private void Start()
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
            
            _text.text = _dot.ToString();
            StartCoroutine(ChangeDots());
        }

        private IEnumerator ChangeDots()
        {
            while (true)
            {
                _text.text += _dot;
                yield return new WaitForSeconds(_delay);
                _text.text += _dot;
                yield return new WaitForSeconds(_delay);         
                _text.text = _dot.ToString();
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}