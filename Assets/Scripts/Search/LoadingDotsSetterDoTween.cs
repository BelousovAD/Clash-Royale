using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Search
{
    public class LoadingDotsSetterDoTween : MonoBehaviour
    {
        private readonly float  _getDelayHigher = 1.5f;
        private readonly float  _getDelayLower = 0.1f;
        private readonly int _infineteLoops = -1;
        
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _animationDelay = 0.5f;
    
        private int _dotsCount = 0;
        private Sequence _sequence;

        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<TMP_Text>();
            }
        }

        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
            CreateDots();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }

        private void CreateDots()
        {
            _sequence.AppendCallback(() => UpdateText(++_dotsCount));
            _sequence.AppendInterval(_animationDelay);
            
            _sequence.AppendCallback(() => UpdateText(++_dotsCount));
            _sequence.AppendInterval(_animationDelay);
            
            _sequence.AppendCallback(() => UpdateText(++_dotsCount));
            _sequence.AppendInterval(_animationDelay * _getDelayHigher);

            _sequence.AppendCallback(() => UpdateText(_dotsCount = 0));
            _sequence.AppendInterval(_animationDelay * _getDelayLower);
            
            _sequence.SetLoops(_infineteLoops);
        }

        private void UpdateText(int dotsCount)
        {
            string dotsString = new string('.', dotsCount);  
            _text.text = dotsString;
        }
    }
}