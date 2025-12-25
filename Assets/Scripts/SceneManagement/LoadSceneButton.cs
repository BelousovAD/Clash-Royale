namespace SceneManagement
{
    using Common;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    internal class LoadSceneButton : AbstractButton
    {
        [SerializeField] private string _sceneToLoad;

        protected override void HandleClick() =>
            SceneManager.LoadScene(_sceneToLoad);
    }
}