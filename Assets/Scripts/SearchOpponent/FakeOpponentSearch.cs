using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace SearchOpponent
{
    internal class FakeOpponentSearch : MonoBehaviour
    {
        [SerializeField][Min(0f)] private float _minTime = 2f;
        [SerializeField][Min(0f)] private float _maxTime = 8f;
        [SerializeField] private string _sceneToLoad;

        private Coroutine _searchCoroutine;

        private void OnEnable() =>
            _searchCoroutine = StartCoroutine(SearchingOpponent());

        private void OnDisable() =>
            StopCoroutine(_searchCoroutine);

        private IEnumerator SearchingOpponent()
        {
            float randomTime = Random.Range(_minTime, _maxTime);

            yield return new WaitForSecondsRealtime(randomTime);

            LoadTargetScene();
        }

        private void LoadTargetScene() =>
            SceneManager.LoadScene(_sceneToLoad);
    }
}