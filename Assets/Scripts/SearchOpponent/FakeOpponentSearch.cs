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
        [SerializeField] private GameObject _cancelButton;

        private Coroutine _searchCoroutine;

        private void OnEnable() =>
            _searchCoroutine = StartCoroutine(SearchingOpponent());

        private void OnDisable() =>
            StopCoroutine(_searchCoroutine);

        private IEnumerator SearchingOpponent()
        {
            _cancelButton.SetActive(true);
            float randomTime = Random.Range(_minTime, _maxTime);

            yield return new WaitForSecondsRealtime(randomTime);

            _cancelButton.SetActive(false);
            _sceneLoader.Load();
        }
    }
}