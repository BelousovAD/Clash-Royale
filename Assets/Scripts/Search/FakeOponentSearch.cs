using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Search
{
    public class FakeOponentSearch : MonoBehaviour
    {
        [SerializeField] private float _minTime = 2f;
        [SerializeField] private float _maxTime = 8f;
        [SerializeField] private string _sceneID;
        
        private Coroutine _searchCoroutine;

        private void OnEnable()
        {
            _searchCoroutine = StartCoroutine(SearchingOpponent());
        }

        private void OnDisable()
        {
            StopCoroutine(_searchCoroutine);
        }

        private IEnumerator SearchingOpponent()
        {
            float randomTime = UnityEngine.Random.Range(_minTime, _maxTime);
            Debug.Log($"Searching opponent in {randomTime} seconds");
            yield return new WaitForSeconds(randomTime);
            
            LoadTargetScene();
        }

        private void LoadTargetScene()
        {
            if (!string.IsNullOrEmpty(_sceneID))
                SceneManager.LoadScene(_sceneID);
        }
    }
}