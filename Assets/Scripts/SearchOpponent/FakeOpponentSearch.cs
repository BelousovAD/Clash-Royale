using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SearchOpponent
{
    internal class FakeOpponentSearch : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField][Min(0f)] private float _minTime = 3f;
        [SerializeField][Min(0f)] private float _maxTime = 6f;
        
        private Coroutine _searchCoroutine;
        private bool _isFound;

        public event Action SearchStatusChanged;

        public bool IsFound
        {
            get
            {
                return _isFound;
            }

            private set
            {
                if (value != _isFound)
                {
                    _isFound = value;
                    SearchStatusChanged?.Invoke();
                }
            }
        }

        private void OnEnable() =>
            _searchCoroutine = StartCoroutine(SearchingOpponent());

        private void OnDisable() =>
            Cancel();

        public void Cancel()
        {
            if (IsFound == false && _searchCoroutine is not null)
            {
                StopCoroutine(_searchCoroutine);
                _searchCoroutine = null;
            }
        }

        private IEnumerator SearchingOpponent()
        {
            IsFound = false;
            float randomTime = Random.Range(_minTime, _maxTime);

            yield return new WaitForSecondsRealtime(randomTime);

            IsFound = true;
            _sceneLoader.Load();
        }
    }
}